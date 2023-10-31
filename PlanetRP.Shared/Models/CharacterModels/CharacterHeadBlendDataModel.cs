namespace PlanetRP.Shared.Models.CharacterModels
{
    public class CharacterHeadBlendDataModel
    {
        public uint shapeFirstID { get; set; }
        public uint shapeSecondID { get; set; }
        public uint shapeThirdID { get; set; }
        public uint skinFirstID { get; set; }
        public uint skinSecondID { get; set; }
        public uint skinThirdID { get; set; }
        public float shapeMix { get; set; }
        public float skinMix { get; set; }
        public float thirdMix { get; set; }
        public bool isParent { get; set; } = false;

    }
}