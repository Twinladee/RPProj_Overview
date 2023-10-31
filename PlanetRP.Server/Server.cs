using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlanetRP.DependencyInjectionsExtensions;
using PlanetRP.Core.Entities.Factories;
using PlanetRP.Server.Services.Chat;
using PlanetRP.Server.Services.VoiceService;
using Microsoft.EntityFrameworkCore;
using PlanetRP.DAL.Context;
using PlanetRP.Core.Configuration;
using System.ComponentModel;
using System.Globalization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlanetRP.Server.Services.CharacterService;
using Quartz;
using PlanetRP.Server.SheduledJobs.VehicleJob;
using Quartz.Spi;
using Quartz.Core;
using Microsoft.Extensions.Hosting;
using PlanetRP.Server.Services.ClothService;
using PlanetRP.Server.Services.DataNodeProvider;

namespace PlanetRP.Server
{
    public class Server : AsyncResource
    {
        private readonly ServiceProvider _serviceProvider;
        private readonly ILogger<Server>? _logger;
        private readonly IHostBuilder _hostBuilder;
        public IConfiguration Configuration { get; }
        

        public Server() : base(new ActionTickSchedulerFactory())
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddJsonFile("appsettings.local.json", true, true)
                .Build();

            var services = new ServiceCollection();

            services.Configure<DevelopmentOptions>(Configuration.GetSection(nameof(DevelopmentOptions)));

            //+ Data set
            services.TryAddSingleton<DataNodeService>();
            //-

            //+ Server singletons
            services.TryAddSingleton<IChatService, ChatService>();
            services.TryAddSingleton<IVoiceService, VoiceService>();
            services.TryAddSingleton<ICharacterService, CharacterService>();
            services.TryAddSingleton<IClothService, ClothService>();
            //- Server singletons

            //+ test
            services.TryAddSingleton<TestConnections>();
            services.TryAddSingleton<ISingletonScript, TestCommads>();
            //- test

            //+ Jobs
            services.TryAddSingleton<VehicleFuelJob>();

            _hostBuilder = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddQuartz(q =>
                {
                    q.ScheduleJob<VehicleFuelJob>(j => j
                    .WithIdentity("VehicleFuelTrigger", "default")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                    .WithIntervalInMinutes(1)
                    .RepeatForever()));
                });

                services.AddQuartzHostedService(q => q.WaitForJobsToComplete = false);
            });
            //- Jobs

            services.AddDbContextFactory<PlanetContext>(options => options
            .UseMySql(Configuration.GetConnectionString("DatabaseConnection"), MariaDbServerVersion.LatestSupportedServerVersion));

            services.AddAllTypes<IServerJob>();

            services.AddLogging(config => config
                .AddConfiguration(Configuration.GetSection("Logging"))
                .AddDebug()
                .AddConsole());

            _serviceProvider = services.BuildServiceProvider();

            _logger = _serviceProvider.GetService<ILogger<Server>>();


            _logger?.LogInformation("DI сервера инициализировано успешно!");

            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

        }

        public override void OnTick()
        {
            base.OnTick();
        }

        public override void OnStart()
        {

            _serviceProvider.InstanciateStartupScripts();

            var serverJobs = _serviceProvider.GetServices<IServerJob>();
            
            var taskList = new List<Task>();
            Parallel.ForEach(serverJobs, job => taskList.Add(job.OnStartup()));
            Task.WaitAll(taskList.ToArray());

            _hostBuilder.Build().Start();

            _logger?.LogInformation("Server started");
        }

        public override void OnStop()
        {
            var serverJobs = _serviceProvider.GetServices<IServerJob>();

            var taskList = new List<Task>();
            Parallel.ForEach(serverJobs, job => taskList.Add(job.OnShutdown()));
            Task.WaitAll(taskList.ToArray());

            _logger?.LogInformation("Server stopped");
        }

        #region Entities

        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new PlanetPlayerFactory();
        }

        public override IEntityFactory<IVehicle> GetVehicleFactory()
        {
            return new PlanetVehicleFactory();
        }

        public override IEntityFactory<IPed> GetPedFactory()
        {
            return new PlanetPedFactory();
        }

        #endregion
    }
}