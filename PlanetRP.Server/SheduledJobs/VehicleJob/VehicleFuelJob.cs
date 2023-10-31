using AltV.Net;
using AltV.Net.Elements.Entities;
using PlanetRP.Core.Callbacks;
using PlanetRP.Core.Entities;
using PlanetRP.DependencyInjectionsExtensions;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.SheduledJobs.VehicleJob
{
    internal class VehicleFuelJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Hello from fuel job");

            var callback = new AsyncFunctionCallback<IVehicle>(async (vehicle) =>
            {
                var planetVehicle = vehicle as PlanetVehicle;

                if (planetVehicle is not null)
                {
                    planetVehicle.FuelLevel = 0;

                    
                }

                await Task.CompletedTask;
            });

            await Alt.ForEachVehicles(callback);
        }
    }
}
