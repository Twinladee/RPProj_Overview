using AltV.Net;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Core.Entities
{
    public interface IPlanetPed : IPed
    {

    }

    public class PlanetPed : AsyncPed, IPlanetPed
    {
        public PlanetPed(ICore core, nint nativePointer, uint id) : base(core, nativePointer, id)
        {
        }
    }
}
