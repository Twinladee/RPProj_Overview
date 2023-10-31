using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Client.Models
{
    internal class ZoomDataLevel
    {
        public string? level { get; set; }
        public float zoomScale { get; set; }
        public float zoomSpeed { get; set; }
        public float scrollSpeed { get; set; }
        public float tilesX { get; set; }
        public float tilesY { get; set; }
    }
}
