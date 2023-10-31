using AltV.Net;
using AltV.Net.Elements.Entities;

namespace PlanetRP.Core.Entities.Factories
{
    public class PlanetPlayerFactory : IEntityFactory<PlanetPlayer>
    {
        public PlanetPlayer Create(ICore core, nint entityPointer, uint id)
        {
            return new PlanetPlayer(core, entityPointer, id);
        }
    }
}
