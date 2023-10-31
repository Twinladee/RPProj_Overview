using PlanetRP.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Client.Constants
{
    internal class ZoomDataLevels
    {
        public static readonly List<ZoomDataLevel> zoomDataLevels = new List<ZoomDataLevel>
        {
            new ZoomDataLevel
            {
                level = "ZOOM_LEVEL_0",
                zoomScale=  0.96f,
                zoomSpeed= 0.9f,
                scrollSpeed= 0.08f,
                tilesX= 0.0f,
                tilesY= 0.0f,
            },
            new ZoomDataLevel
            {
                level = "ZOOM_LEVEL_1",
                zoomScale = 1.6f,
                zoomSpeed = 0.9f,
                scrollSpeed = 0.08f,
                tilesX = 0.0f,
                tilesY = 0.0f,
            },
            new ZoomDataLevel
            {
                level = "ZOOM_LEVEL_2",
                zoomScale = 8.6f,
                zoomSpeed = 0.9f,
                scrollSpeed = 0.08f,
                tilesX = 0.0f,
                tilesY = 0.0f,
            },
            new ZoomDataLevel
            {
                level = "ZOOM_LEVEL_3",
                zoomScale = 12.3f,
                zoomSpeed = 0.9f,
                scrollSpeed = 0.08f,
                tilesX = 0.0f,
                tilesY = 0.0f,
            },
            new ZoomDataLevel
            {
                level = "ZOOM_LEVEL_4",
                zoomScale = 22.3f,
                zoomSpeed = 0.9f,
                scrollSpeed = 0.08f,
                tilesX = 0.0f,
                tilesY = 0.0f,
            },
            new ZoomDataLevel
            {
                level = "ZOOM_LEVEL_GOLF_COURSE",
                zoomScale = 55.0f,
                zoomSpeed = 0.0f,
                scrollSpeed = 0.1f,
                tilesX = 2.0f,
                tilesY = 1.0f,
            },
            new ZoomDataLevel
            {
                level = "ZOOM_LEVEL_INTERIOR",
                zoomScale = 450.0f,
                zoomSpeed = 0.0f,
                scrollSpeed = 0.1f,
                tilesX = 1.0f,
                tilesY = 1.0f,
            },
            new ZoomDataLevel
            {
                level = "ZOOM_LEVEL_GALLERY",
                zoomScale = 4.5f,
                zoomSpeed = 0.0f,
                scrollSpeed = 0.0f,
                tilesX = 0.0f,
                tilesY = 0.0f,
            },
            new ZoomDataLevel
            {
                level = "ZOOM_LEVEL_GALLERY_MAXIMIZE",
                zoomScale = 11.0f,
                zoomSpeed = 0.0f,
                scrollSpeed = 0.0f,
                tilesX = 2.0f,
                tilesY = 3.0f,
            }
        };
    }
}
