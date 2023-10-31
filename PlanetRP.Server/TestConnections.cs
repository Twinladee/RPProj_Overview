
using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using PlanetRP.Core.Constants.Enums;
using PlanetRP.DependencyInjectionsExtensions;
using PlanetRP.Core.Entities;
using PlanetRP.Server.Extensions;
using PlanetRP.Core.Services.CommandService;
using AltV.Net.Async;
using PlanetRP.Server.Services.ClothService;
//using AltV.Net.Resources.Chat.Api;

namespace PlanetRP.Server
{
    internal class TestConnections : IStartupSingletonScript
    {
        private readonly IClothService _clothService;

        public TestConnections(IClothService clothService)
        {
            _clothService = clothService;
            AltAsync.OnPlayerConnect += PlayerConnect;
        }

        public Task PlayerConnect(IPlayer player, string reason)
        {
            player.SetDateTime(DateTime.Now);
            player.Model = (uint)PedModel.FreemodeFemale01;

            player.Spawn(new AltV.Net.Data.Position((float)-1184.159912109375, (float)-2909.5869140625, (float)17.086111068725586));

            _clothService.EquipNaked((PlanetPlayer)player);


            return Task.CompletedTask;
        }

        
    }
}
