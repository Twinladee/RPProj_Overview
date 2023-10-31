using AltV.Net.Client;
using AltV.Net.Client.Async;
using AltV.Net.Client.Elements.Entities;
using AltV.Net.Client.Elements.Interfaces;
using Newtonsoft.Json;
using PlanetRP.DependencyInjectionsExtensions;
using PlanetRP.Shared;
using PlanetRP.Shared.Models.CharacterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Client.Services.CharacterService
{
    internal interface ICharacterService : IStartupSingletonScript
    {

    }

    internal class CharacterService : ICharacterService
    {
        public CharacterService()
        {
            Alt.OnConnectionComplete += InitializeService; 
        }

        private void InitializeService()
        {
            Alt.OnServer<string>(CharacterEvents.applyNewCharacter, OnApplyNewCharacter);
            Alt.Log($"Сервис персонажей инициализирован");
        }

        public void OnApplyNewCharacter(string characterCreationJson)
        {
            var player = Alt.LocalPlayer;

            try
            {
                
            }
            catch (Exception e)
            {
                Alt.Log($"Ошибка {e.Message}");
            }
            
        }
    }
}
