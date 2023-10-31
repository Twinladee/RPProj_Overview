using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PlanetRP.Core.Entities;
using PlanetRP.DependencyInjectionsExtensions;
using PlanetRP.Shared;
using PlanetRP.Shared.Models.CharacterModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.Services.CharacterService
{
    internal interface ICharacterService : IStartupSingletonScript
    {
        public void OnCreateNewCharacter(PlanetPlayer player, string characterCreationJson);
    }

    internal class CharacterService : ICharacterService
    {
        private readonly ILogger<CharacterService> _logger;

        public CharacterService(ILogger<CharacterService> logger)
        {
            _logger = logger;

            AltAsync.OnClient<PlanetPlayer, string>(CharacterEvents.createNewCharacter, OnCreateNewCharacter);
        }

        public void OnCreateNewCharacter(PlanetPlayer player, string characterCreationJson)
        {
            CharacterModel? characterModelCreation = JsonConvert.DeserializeObject<CharacterModel>(characterCreationJson);

            if (characterModelCreation is null)
            {
                return;
            }

            var characterHeadBlendData = characterModelCreation.CharacterHeadBlendData;

            player.SetHeadBlendData(characterHeadBlendData.shapeFirstID, characterHeadBlendData.shapeSecondID, characterHeadBlendData.shapeThirdID,
                characterHeadBlendData.skinFirstID, characterHeadBlendData.skinSecondID, characterHeadBlendData.skinThirdID, characterHeadBlendData.shapeMix,
                characterHeadBlendData.skinMix, characterHeadBlendData.thirdMix);

            foreach (var headBlendPaletteColor in characterModelCreation.CharacterHeadBlendPaletteColor)
            {
                player.SetHeadBlendPaletteColor(headBlendPaletteColor.id, new Rgba(headBlendPaletteColor.red, headBlendPaletteColor.green, headBlendPaletteColor.blue, 0));
            }

            foreach (var microMorph in characterModelCreation.CharacterMicroMorph)
            {
                player.SetFaceFeature(microMorph.index, microMorph.scale);
            }

            foreach (var headOverlay in characterModelCreation.CharacterHeadOverlay)
            {
                player.SetHeadOverlay(headOverlay.overlayID, headOverlay.index, headOverlay.opacity);
            }

            foreach (var headOverlayColor in characterModelCreation.CharacterHeadOverlayTint)
            {
                player.SetHeadOverlayColor(headOverlayColor.overlayID, headOverlayColor.colorType, headOverlayColor.colorID, headOverlayColor.secondColorID);
            }

            player.SetEyeColor(characterModelCreation.CharacterHeadBlendEyeColor.index);

            player.HairColor = characterModelCreation.CharacterHairTint.colorID;
            player.HairHighlightColor = characterModelCreation.CharacterHairTint.highlightColorID;

            

            Alt.Log("Персонаж применен");
            //player.Emit(CharacterEvents.applyNewCharacter, characterCreationJson);
        }


    }
}
