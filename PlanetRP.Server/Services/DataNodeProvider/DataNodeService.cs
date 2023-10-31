using Newtonsoft.Json;
using PlanetRP.DependencyInjectionsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.Services.DataNodeProvider
{
    public class DataNodeService : IStartupSingletonScript
    {
        public List<PedComponentVariationModel> ComponentVariations = new();
       

        public DataNodeService()
        {
            LoadPedComponentVariations();

            //PedComponentVariations.
        }

        private void LoadPedComponentVariations()
        {
            ComponentVariations = LoadDataFromJsonFile<List<PedComponentVariationModel>>("PlanetRP-Data\\ComponentVariations.json");
        }

        private static TDumpType LoadDataFromJsonFile<TDumpType>(string dumpFileName)
        {
            TDumpType dumpResult = default;
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data", dumpFileName);

            if (!File.Exists(filePath))
            {
                //TODO Logger
            }

            try
            {
                dumpResult = JsonConvert.DeserializeObject<TDumpType>(File.ReadAllText(filePath));
            }
            catch (Exception e)
            {
                //TODO Logger
            }

            return dumpResult;
        }
    }
}
