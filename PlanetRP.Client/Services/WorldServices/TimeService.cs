using AltV.Net.Client;
using AltV.Net.Client.Async;
using PlanetRP.DependencyInjectionsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Client.Services.WorldService
{
    internal class TimeService : IStartupSingletonScript
    {
        public TimeService() 
        {
            Alt.OnTick += SetRealTime;
        }

        private void SetRealTime()
        {
            int networkTime = Alt.Natives.GetNetworkTime();
            var gameTime = convertNetToGameTime(networkTime);
            Alt.Natives.SetClockTime((int)gameTime[0], (int)gameTime[1], (int)gameTime[2]);
            
        }

        private float[] convertNetToGameTime(int networkTime)
        {
            float constanta = 28800000.0f;

            

            var current_part_of_day = networkTime % constanta;
            var normalized_hours = current_part_of_day / constanta;
            var hours = normalized_hours * 24.0f;

            var normalized_minutes = hours % 1.0f;
            var minute = normalized_minutes * 60.0f;

            var normalized_seconds = normalized_minutes * 1.0f;
            var second = normalized_seconds * 60.0f;

            return new float[] { hours, minute, second };
        }
    }
}
