using AltV.Net.Client;
using AltV.Net.Client.Async;
using AltV.Net.Client.Elements.Data;
using AltV.Net.Client.Elements.Entities;
using AltV.Net.Client.Elements.Interfaces;
using AltV.Net.Client.Events;
using PlanetRP.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PlanetRP.Client.Services.ChatService
{
    internal class ChatServiceClient : IChatServiceClient
    {
        private IWebView? webView = null;

        public ChatServiceClient()
        {
            Alt.OnConnectionComplete += InitializeBrowser;
            Alt.OnKeyDown += OnKeyPress;
        }

        public void InitializeBrowser()
        {
            //Alt.Natives.SetPedComponentVariation(,)
            //

            webView = Alt.CreateWebView("http://assets/planetrp-webviews/webview-chat/index.html");

            Alt.OnServer("Chat:Message", (string playerName, string message) =>
            {
                webView?.Emit(ChatEvents.newMessage, playerName, message);
            });

            webView.On<string>(ChatEvents.onSendChatMessage, message =>
            {
                Console.WriteLine("[На сервер] Отправка сообщения");
                ToggleChatVisibility();

                if (message.StartsWith("/"))
                {
                    Alt.EmitServer(ChatEvents.sendCommand, message);
                    return;
                }

                Alt.EmitServer(ChatEvents.onSendChatMessage, message);
            });
        }

        public void ToggleChatVisibility()
        {
            if (webView is null)
                return;

            webView.Emit(ChatEvents.toggleChat, !webView.Focused);

            if (webView.Focused)
            {
                webView.Unfocus();
            }
            else
            {
                webView.Focus();
            }

            Alt.GameControlsEnabled = !webView.Focused; 
        }

        private void OnKeyPress(Key key)
        {
            if (webView is null)
                return;

            if (webView.Focused && key == Key.Escape)
            {
                Console.WriteLine("FOCUSED ESCAPE");
                ToggleChatVisibility();
                return;
            }
            else if (webView.Focused)
            {
                return;
            }

            if (key != Key.T)
            {
                return;
            }

            ToggleChatVisibility();
        }
    }
}
