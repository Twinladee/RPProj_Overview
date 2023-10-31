using AltV.Net.Client.Async;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlanetRP.Client.Services.CharacterService;
using PlanetRP.Client.Services.ChatService;
using PlanetRP.Client.Services.ClothService;
using PlanetRP.Client.Services.HudServices;
using PlanetRP.Client.Services.WorldService;
using PlanetRP.DependencyInjectionsExtensions;

namespace PlanetRP.Client
{
    public class Client : AsyncResource
    {
        private readonly ServiceProvider _serviceProvider;
        //public IConfiguration Configuration { get; }

        public Client() : base()
        {
            var services = new ServiceCollection();

            services.TryAddSingleton<IChatServiceClient, ChatServiceClient>();
            services.TryAddSingleton<ICharacterService, CharacterService>();

            services.TryAddSingleton<TimeService>();
            services.TryAddSingleton<MapService>();

            services.TryAddSingleton<IClothService, ClothService>();

            _serviceProvider = services.BuildServiceProvider();

            //var _logger = _serviceProvider.GetService<ILogger<Client>>();



            //_logger?.LogInformation("DI клиента инициализировано успешно!");
        }

        public override void OnStart()
        {

            _serviceProvider.InstanciateStartupScripts();

            Console.WriteLine("Started");
        }

        public override void OnStop()
        {
            //var serverJobs = _serviceProvider.GetServices<IServerJob>();

            Console.WriteLine("Stopped");
        }

    }
}