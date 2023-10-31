using AltV.Net.Async;
using PlanetRP.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetRP.Server.Extensions
{
    public static class PlayerChatExtensions
    {
        public static void SendChatMessage(this PlanetPlayer player, string message)
        {
            AltAsync.EmitAllClients("Chat:Message", message);
        }
    }
}
