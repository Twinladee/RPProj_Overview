using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlanetRP.Core.Configuration;
using PlanetRP.DAL.Context;
using PlanetRP.DependencyInjectionsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.ServerJobs
{
    public class Database : IServerJob
    {
        private readonly IDbContextFactory<PlanetContext> _dbContextFactory;
        private readonly DevelopmentOptions _devOptions;
        private readonly ILogger<Server> _logger;

        public Database(IDbContextFactory<PlanetContext> dbContextFactory, IOptions<DevelopmentOptions> devOptions, ILogger<Server> logger)
        {
            _dbContextFactory = dbContextFactory;
            _devOptions = devOptions.Value;
            _logger = logger;
        }

        public Task OnSave()
        {
            return Task.CompletedTask;
        }

        public async Task OnShutdown()
        {
            await Task.CompletedTask;
        }

        public async Task OnStartup()
        {
            using var dbContext = _dbContextFactory.CreateDbContext();

            if (_devOptions.DropDatabaseAtStartup)
            {
                dbContext.Database.EnsureDeleted();
                _logger.LogInformation("База удалена");

                dbContext.Database.EnsureCreated();
                _logger.LogInformation("База создана");

                await dbContext.SaveChangesAsync();
            }

            await Task.CompletedTask;
        }
    }
}
