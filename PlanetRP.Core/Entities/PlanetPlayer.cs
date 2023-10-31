using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;
using PlanetRP.Core.Constants.Enums;

namespace PlanetRP.Core.Entities
{
    public interface IPlanetPlayer : IPlayer
    {

    }

    public class PlanetPlayer : AsyncPlayer, IPlanetPlayer
    {
        public PlanetPlayer(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
        {

        }



        public VoiceLevel VoiceLevel
        {
            get
            {
                if (!GetStreamSyncedMetaData("voiceLevel", out int result))
                {
                    return VoiceLevel.Mute;
                }

                return (VoiceLevel)result;
            }
            set
            {
                SetStreamSyncedMetaData("voiceLevel", (int)value);
            }
        }

        

    }
}
