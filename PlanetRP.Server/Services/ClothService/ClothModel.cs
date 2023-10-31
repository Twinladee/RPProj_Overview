using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.Services.ClothService
{
    public class ClothModel
    {
        public byte component { get; set; }
        public ushort drawable { get; set; }
        public byte texture { get; set; }
        public byte pallete { get; set; }
        public string dlc { get; set; } = null!;
    }
}
