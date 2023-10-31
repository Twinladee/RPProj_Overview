using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using PlanetRP.Core.Constants.Enums;
using PlanetRP.Core.Entities;
using PlanetRP.Core.Services.CommandService;
using PlanetRP.DependencyInjectionsExtensions;
using PlanetRP.Server.Extensions;
using PlanetRP.Server.Services.CharacterService;
using PlanetRP.Server.Services.ClothService;
using PlanetRP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server
{
    internal class TestCommads : ISingletonScript
    {
        private readonly ICharacterService _characterService;
        private readonly IClothService _clothService;
        public TestCommads(ICharacterService characterService, IClothService clothService)
        {
            _characterService = characterService;
            _clothService = clothService;
        }

        [Command("createvehicle", requiredAccessLevel: AccessLevel.Developer)]
        public void CreateVehicle(PlanetPlayer player, string VehicleName, int R = 0, int G = 0, int B = 0)
        {
            IVehicle veh = Alt.CreateVehicle(Alt.Hash(VehicleName), new Position(player.Position.X, player.Position.Y + 1.5f, player.Position.Z), player.Rotation);

            //If the Vehicle Creation was successfull, then it should notify you.
            if (veh != null) { player.SendChatMessage("Ты создал ТС" + VehicleName); }
        }

        [Command("createchar", requiredAccessLevel: AccessLevel.Developer)]
        public void CreateCharacter(PlanetPlayer player, string charJson)
        {
            _characterService.OnCreateNewCharacter(player, charJson);
        }

        [Command("putcloth", requiredAccessLevel: AccessLevel.Developer)]
        public void PutOnClothes(PlanetPlayer player, string charJson)
        {
            _clothService.OnPutOnClothes(player, charJson);
        }
    }
}
