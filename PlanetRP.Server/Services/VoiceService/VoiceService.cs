using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using PlanetRP.Core.Constants.Enums;
using PlanetRP.DependencyInjectionsExtensions;
using PlanetRP.Core.Entities;


namespace PlanetRP.Server.Services.VoiceService
{
    internal interface IVoiceService : IStartupSingletonScript
    {
    }

    internal class VoiceService : IVoiceService
    {

        public IVoiceChannel MainNormalVoiceChannel { get; }
        public IVoiceChannel MainWhisperVoiceChannel { get; }
        public IVoiceChannel MainShoutVoiceChannel { get; }
        public IVoiceChannel PoliceMegafoneVoiceChannel { get; }

        public VoiceService()
        {
            MainNormalVoiceChannel = Alt.CreateVoiceChannel(true, 8f);
            MainWhisperVoiceChannel = Alt.CreateVoiceChannel(true, 3f);
            MainShoutVoiceChannel = Alt.CreateVoiceChannel(true, 15f);
            PoliceMegafoneVoiceChannel = Alt.CreateVoiceChannel(true, 32f);

            AltAsync.OnPlayerConnect += (player, reason) => OnPlayerConnectAsync((PlanetPlayer)player, reason);
            AltAsync.OnPlayerDisconnect += (player, reason) => OnPlayerDisconnectAsync((PlanetPlayer)player, reason);
            AltAsync.OnClient<PlanetPlayer, int, Task>("Voice:ChangeVoiceLevel", OnChangeVoiceLevelAsync);

        }

        private Task OnPlayerConnectAsync(PlanetPlayer player, string reason)
        {

            MainNormalVoiceChannel.AddPlayer(player);

            MainWhisperVoiceChannel.AddPlayer(player);
            MainWhisperVoiceChannel.MutePlayer(player);

            MainShoutVoiceChannel.AddPlayer(player);
            MainShoutVoiceChannel.MutePlayer(player);

            PoliceMegafoneVoiceChannel.AddPlayer(player);
            PoliceMegafoneVoiceChannel.MutePlayer(player);

            player.VoiceLevel = VoiceLevel.Normal;

            return Task.CompletedTask;

        }

        private Task OnPlayerDisconnectAsync(PlanetPlayer player, string reason)
        {
            MainNormalVoiceChannel.RemovePlayer(player);
            MainWhisperVoiceChannel.RemovePlayer(player);
            MainShoutVoiceChannel.RemovePlayer(player);
            PoliceMegafoneVoiceChannel.RemovePlayer(player);

            return Task.CompletedTask;
        }

        private Task OnChangeVoiceLevelAsync(PlanetPlayer player, int voiceLevel)
        {
            //Добавить возможные проверки

            switch (player.VoiceLevel)
            {
                case VoiceLevel.Mute:
                    break;
                case VoiceLevel.Whisper:
                    MainWhisperVoiceChannel.MutePlayer(player);
                    break;
                case VoiceLevel.Normal:
                    MainNormalVoiceChannel.MutePlayer(player);
                    break;
                case VoiceLevel.Shout:
                    MainShoutVoiceChannel.MutePlayer(player);
                    break;
                case VoiceLevel.PoliceMegafone:
                    PoliceMegafoneVoiceChannel.MutePlayer(player);
                    break;
                default:
                    break;
            }

            switch ((VoiceLevel)voiceLevel)
            {
                case VoiceLevel.Whisper:
                    MainWhisperVoiceChannel.UnmutePlayer(player);
                    break;
                case VoiceLevel.Normal:
                    MainNormalVoiceChannel.UnmutePlayer(player);
                    break;
                case VoiceLevel.Shout:
                    MainShoutVoiceChannel.UnmutePlayer(player);
                    break;
                case VoiceLevel.PoliceMegafone:
                    PoliceMegafoneVoiceChannel.UnmutePlayer(player);
                    break;
                default:
                    break;
            }

            player.VoiceLevel = (VoiceLevel)voiceLevel;

            return Task.CompletedTask;
        }
    }
}
