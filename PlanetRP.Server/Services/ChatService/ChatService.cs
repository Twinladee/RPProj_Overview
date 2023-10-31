using PlanetRP.DependencyInjectionsExtensions;
using AltV.Net;
using AltV.Net.Async;
using PlanetRP.Core.Entities;
using PlanetRP.Server.Constants;

namespace PlanetRP.Server.Services.Chat
{
    internal interface IChatService : IStartupSingletonScript
    {

    }

    internal class ChatService : IChatService
    {
        public ChatService()
        {
            AltAsync.OnClient<PlanetPlayer, string, string, Task>("Chat:OnSendChatMessage", OnSendChatMessage);
        }

        private Task OnSendChatMessage(PlanetPlayer player, string message, string chatType)
        {
            if (message.Length <= 0)
                return Task.CompletedTask;

            var playersInRange = Alt.GetAllPlayers().Where(p => p.Position.Distance(player.Position) <= ChatConstants.ChatMessageRange).ToList();

            foreach (var playerInRange in playersInRange)
            {
                playerInRange.Emit("Chat:Message", player.Name, message, chatType);
            }
            //Проверить на работоспособность и возможно переделать в асинхронное

            //AltAsync.EmitAllClients("Chat:Message", player.Name, message);
            return Task.CompletedTask;
        }

    }
}
