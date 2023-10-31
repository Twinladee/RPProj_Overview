using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Shared.Models.CharacterModels
{
    public class CharacterModel
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public CharacterHeadBlendDataModel CharacterHeadBlendData { get; set; } = null!;
        public List<CharacterHeadBlendPaletteColorModel> CharacterHeadBlendPaletteColor { get; set; } = null!;//4
        public List<CharacterMicroMorphModel> CharacterMicroMorph { get; set; } = null!;//20
        public List<CharacterHeadOverlayModel> CharacterHeadOverlay { get; set; } = null!;//13
        public List<CharacterHeadOverlayTintModel> CharacterHeadOverlayTint { get; set; } = null!;//0,1,2
        public CharacterHeadBlendEyeColorModel CharacterHeadBlendEyeColor { get; set; } = null!;
        public CharacterHairTintColorModel CharacterHairTint { get; set; } = null!;

        
    }
}
