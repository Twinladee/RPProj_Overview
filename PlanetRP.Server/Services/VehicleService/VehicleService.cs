using PlanetRP.DependencyInjectionsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.Services.VehicleService
{
    internal interface IVehicleService : IStartupSingletonScript
    {

    }

    internal class VehicleService : IVehicleService
    {

        public VehicleService()
        {
            
        }

    }
}
