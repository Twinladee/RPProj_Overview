using AltV.Net;
using AltV.Net.Data;
using PlanetRP.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.Services.DataNodeProvider
{
    

    public class PedComponentVariationModel
    {
        public required string DlcCollectionName { get; set; }
        public required string PedName { get; set; }

        public bool IsCloth { get; set; }

        public required string NameHash { get; set; }
        public required string ComponentType { get; set; }

        public required string AnchorPoint { get; set; }
        public int ComponentId { get; set; }
        public int DrawableId { get; set; }
        public int TextureId { get; set; }
        public int RelativeCollectionDrawableId { get; set; }
        public TranslatedLabel? TranslatedLabel { get; set; }
        public int Price { get; set; }
        public List<string>? RestrictionTags { get; set; }

        public uint DlcCollectionNameHash()
        {
            return (DlcCollectionName is PlayerSexConstants.PedMale or PlayerSexConstants.PedFemale) ? 0 : Alt.Hash(DlcCollectionName);
        }
    }

    public class TranslatedLabel
    {
        public long Hash { get; set; }
        public required string English { get; set; }
        public required string German { get; set; }
        public required string French { get; set; }
        public required string Italian { get; set; }
        public required string Russian { get; set; }
        public required string Polish { get; set; }
        public required string Name { get; set; }
        public required string TraditionalChinese { get; set; }
        public required string SimplifiedChinese { get; set; }
        public required string Spanish { get; set; }
        public required string Japanese { get; set; }
        public required string Korean { get; set; }
        public required string Portuguese { get; set; }
        public required string Mexican { get; set; }
    }
}
