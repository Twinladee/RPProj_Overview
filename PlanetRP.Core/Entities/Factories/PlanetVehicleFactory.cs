using AltV.Net;
using AltV.Net.Elements.Entities;

namespace PlanetRP.Core.Entities.Factories
{
    public class PlanetVehicleFactory : IEntityFactory<PlanetVehicle>
    {
        public PlanetVehicle Create(ICore core, nint entityPointer, uint id)
        {
            return new PlanetVehicle(core, entityPointer, id);
        }
    }
}
