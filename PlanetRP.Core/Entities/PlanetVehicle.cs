using AltV.Net;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;
using PlanetRP.Shared.MetaDataConstants;

namespace PlanetRP.Core.Entities
{
    public interface IPlanetVehicle : IVehicle
    {

    }

    public class PlanetVehicle : AsyncVehicle, IPlanetVehicle
    {
        public PlanetVehicle(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
        {
            
        }

        public float FuelLevel 
        { 
            get 
            {
                if (!GetStreamSyncedMetaData(VehicleMetaDataConstants.VehicleFuelLevel, out float result))
                {
                    return 0;
                }

                return result;
            }
            set 
            {
                SetStreamSyncedMetaData(VehicleMetaDataConstants.VehicleFuelLevel, value);
            } 
        }
    }

}
