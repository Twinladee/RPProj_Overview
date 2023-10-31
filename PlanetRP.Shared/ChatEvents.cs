namespace PlanetRP.Shared
{
    public static class ChatEvents
    {
        public const string toggleChat = "Chat:Toggle:Visibility";
        public const string newMessage = "Chat:NewMessage";
        public const string onSendChatMessage = "Chat:OnSendChatMessage";
        public const string sendCommand = "Commands:Execute";

    }
    public static class ChatStates
    {
        public const string RP = "RP";
        public const string NRP = "NRP";
        public const string ME = "ME";
        public const string TRY = "TRY";
        public const string DO = "DO";
    }
}