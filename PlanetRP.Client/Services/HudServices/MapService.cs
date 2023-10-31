using AltV.Net.Client;
using AltV.Net.Client.Elements.Interfaces;
using PlanetRP.Client.Constants;
using PlanetRP.Client.Models;
using PlanetRP.DependencyInjectionsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Client.Services.HudServices
{
    internal class MapService : IStartupSingletonScript
    {
        private int radarZoomLevel = 1100;
        private uint tickInterval;
        private IPlayer player;

        public MapService()
        {
            foreach (var zoomConfig in ZoomDataLevels.zoomDataLevels)
            {
                SetMapZoomDataLevel(zoomConfig);
            }

            tickInterval = Alt.SetInterval(() =>
            {
                UpdateRadarZoomLevel();
            }, 100);
        }

        private void UpdateRadarZoomLevel()
        {
            Alt.Natives.SetRadarZoom(radarZoomLevel);
        }

        private void SetMapZoomDataLevel(ZoomDataLevel zoomDataLevel)
        {
            var zoomData = Alt.GetMapZoomData(zoomDataLevel.level);
            zoomData.FZoomScale = zoomDataLevel.zoomScale;
            zoomData.FZoomSpeed = zoomDataLevel.zoomSpeed;
            zoomData.FScrollSpeed = zoomDataLevel.scrollSpeed;
            zoomData.VTilesX = zoomDataLevel.tilesX;
            zoomData.VTilesY = zoomDataLevel.tilesY;
        }
    }
}
