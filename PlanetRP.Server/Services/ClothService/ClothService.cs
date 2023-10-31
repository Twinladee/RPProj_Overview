using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Elements.Entities;
using Newtonsoft.Json;
using PlanetRP.Core.Entities;
using PlanetRP.DependencyInjectionsExtensions;
using PlanetRP.Server.Services.DataNodeProvider;
using PlanetRP.Shared;
using PlanetRP.Shared.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.Services.ClothService
{
    public interface IClothService : IStartupSingletonScript
    {
        public void OnPutOnClothes(PlanetPlayer player, string clothToPutOnJson);
        public void EquipNaked(PlanetPlayer player);
    }
    public class ClothService : IClothService
    {
        

        private readonly DataNodeService _dataNodeService;

        private static List<string> NakedMale = new()
        {
            "SP_M_BERD_0_0",
            "SP_M_HAIR_0_0",
            "SP_M_UPPR_15_0",
            "DLC_MP_VAL2_M_LEGS_1_0",
            "SP_M_HAND_0_0",
            "DLC_MP_APA_M_FEET_1_0",
            "SP_M_TEEF_0_0",
            "SP_M_ACCS_15_0",
            "SP_M_TASK_0_0",
            "SP_M_DECL_0_0",
            "SP_M_JBIB_15_0",
            "SP_M_HEAD_8_0_1",
            "SP_M_EYES_0_0",
            "DLC_MP_LUXE2_M_PEARS_12_0",
            "DLC_MP_VWD_M_PLEFT_WRIST_6_0",
            "DLC_MP_BIKER_M_PRIGHT_WRIST_0_0"
        };

        private static List<string> NakedFemale = new()
        {
            "SP_F_BERD_0_0",
            "SP_F_HAIR_0_0",
            "SP_F_UPPR_15_0",
            "SP_F_LOWR_15_0",
            "SP_F_HAND_0_0",
            "DLC_MP_APA_F_FEET_1_0",
            "SP_F_TEEF_0_0",
            "SP_F_ACCS_15_0",
            "SP_F_TASK_0_0",
            "SP_F_DECL_0_0",
            "SP_F_JBIB_15_0",
            "DLC_MP_X17_F_PHEAD_0_0",
            "SP_F_EYES_5_0",
            "DLC_MP_LUXE_F_PEARS_9_0",
            "SP_F_LEFT_WRIST_1_0",
            "DLC_MP_BIKER_F_PRIGHT_WRIST_0_0"
        };

        public ClothService(DataNodeService dataNodeService) 
        {
            _dataNodeService = dataNodeService;

            AltAsync.OnClient<PlanetPlayer, string>(ClothEvents.putOnClothes, OnPutOnClothes);
            AltAsync.OnClient<PlanetPlayer, string, string>("ChangeTorso", OnChangeTorso);
        }

        public async void OnPutOnClothes(PlanetPlayer player, string clothToPutOnJson)
        {

            var clothToPutOn = JsonConvert.DeserializeObject<ClothModel>(clothToPutOnJson);
            //player.SetClothes();
            if (clothToPutOn is null)
            {
                return;
            }
            //player.
            
            player.SetClothes(clothToPutOn.component, clothToPutOn.drawable, clothToPutOn.texture, 2); 
            

            var playerJacket = player.GetClothes(11);
            var playerUndershirt = player.GetClothes(8);

            if (clothToPutOn.component == 11)
            {
                player.Emit("AutoTorso", playerJacket.Drawable.ToString(), playerJacket.Texture.ToString(), playerUndershirt.Drawable.ToString(), playerUndershirt.Texture.ToString());

            }
            //player.ClearDecorations();
        }

        public void Equip(PlanetPlayer player, string nameHash)
        {
            nameHash.Trim();

            if (string.IsNullOrEmpty(nameHash))
            {
                return;
            }

            var clothModel = GetClothModelByNameHash(nameHash);

            if (clothModel is null)
            {
                return;
            }

            if (clothModel.IsCloth)
            {
                player.SetDlcClothes((byte)clothModel.ComponentId, (ushort)clothModel.RelativeCollectionDrawableId, (byte)clothModel.TextureId, 2, clothModel.DlcCollectionNameHash());
            }
            else
            {
                player.SetDlcProps((byte)clothModel.ComponentId, (ushort)clothModel.RelativeCollectionDrawableId, (byte)clothModel.TextureId, clothModel.DlcCollectionNameHash());
            }
        }

        public void EquipNaked(PlanetPlayer player, List<string> nameHashs)
        {
            foreach (string nameHash in nameHashs)
            {
                Equip(player, nameHash);
            }
        }

        public void EquipNaked(PlanetPlayer player)
        {
            if (player.Model == Alt.Hash(PlayerSexConstants.PedMale))
            {
                EquipNaked(player, NakedMale); 
            }
            else if (player.Model == Alt.Hash(PlayerSexConstants.PedFemale))
            {
                EquipNaked(player, NakedFemale);
            }

        }


        public PedComponentVariationModel? GetClothModelByNameHash(string nameHash)
        {
            return _dataNodeService.ComponentVariations.FirstOrDefault(x => x.NameHash == nameHash);
        }

        public async void OnChangeTorso(PlanetPlayer player, string texture, string drawable)
        {
            
            player.SetClothes(3, ushort.Parse(drawable), byte.Parse(texture), 2);
        }

    }
}
