using AltV.Net.Client;
using PlanetRP.DependencyInjectionsExtensions.PlanetRP.DependencyInjectionsExtensions;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Client.Services.ClothService
{
    internal interface IClothService : IClientStartupSigletonScript
    {

    }

    internal class ClothService : IClothService
    {
        public ClothService()
        {
            Alt.OnConnectionComplete += InitialiazeService;
        }

        public void InitialiazeService()
        {
            Alt.OnServer<string, string, string, string>("AutoTorso", OnAutoTorso);
        }

        internal void OnAutoTorso(string drawable, string texture, string drawableUnder, string textureUnder)
        {
            var _drawable = ushort.Parse(drawable);
            var _texture = byte.Parse(texture);

            var _drawableUnder = ushort.Parse(drawableUnder);
            var _textureUnder = byte.Parse(textureUnder);

            var player = Alt.LocalPlayer;
            var ped = Alt.CreateLocalPed(player.Model, player.Dimension, player.Position, player.Rotation, false, 10);

            Alt.Natives.ClonePedToTarget(player, ped.ScriptId);

            var hashOf11 = Alt.Natives.GetHashNameForComponent(ped, 11, _drawable, _texture);
            var hashOf8 = Alt.Natives.GetHashNameForComponent(ped, 8, _drawableUnder, _textureUnder);

            ped.Destroy();

            Alt.Log("Хэш компонента 11" + hashOf11.ToString());
            Alt.Log("Хэш компонента 8" + hashOf8.ToString());

            Alt.Log("Количество форсед компонентов 11 = " + Alt.Natives.GetShopPedApparelForcedComponentCount(hashOf11).ToString());

            Alt.Log("Количество форсед компонентов 8 = " + Alt.Natives.GetShopPedApparelForcedComponentCount(hashOf8).ToString());

            int fcTorsoDrawable = 1;
            int fcTorsoTexture = 0;

            for (int i = 0; i < Alt.Natives.GetShopPedApparelForcedComponentCount(hashOf11); i++)
            {
                uint fcNameHash = 0;
                int fcEnumValue = 0;
                int fcType = 0;

                Alt.Natives.GetForcedComponent(hashOf11, i, ref fcNameHash, ref fcEnumValue, ref fcType);

                Alt.Log("fcType = " + fcType.ToString());

                if (fcType == 3)
                {
                    if (fcNameHash == 0 || fcNameHash == Alt.Natives.GetHashKey("0"))
                    {
                        fcTorsoDrawable = fcEnumValue;
                        fcTorsoTexture = 0;
                    }
                    else
                    {
                        int OutComponent = 0;
                        Alt.Natives.GetShopPedComponent(fcNameHash, ref OutComponent);

                        //player.

                        Alt.Log("Out component = " + OutComponent.ToString());
                    }

                    Alt.Log("fcTorsoDrawable = " + fcTorsoDrawable.ToString());
                    Alt.Log("fcTorsoTexture = " + fcTorsoTexture.ToString());
                }
            }

            

            Alt.EmitServer("ChangeTorso", fcTorsoTexture.ToString(), fcTorsoDrawable.ToString());
            //Alt.Natives.MakePair

        }
    }
}
