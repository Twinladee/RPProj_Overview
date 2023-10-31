using AltV.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Core.Entities.Factories
{
    public class PlanetPedFactory : IEntityFactory<PlanetPed>
    {
        public PlanetPed Create(ICore core, nint entityPointer, uint id)
        {
            return new PlanetPed(core, entityPointer, id);
        }
    }
}
