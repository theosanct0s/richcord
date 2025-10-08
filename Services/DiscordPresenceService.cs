using DiscordRPC;

namespace richcord.Services
{
    // Handles Discord Rich Presence connection logic
    public class DiscordPresenceService
    {
        private DiscordRpcClient _client;

        // Attempts to connect to Discord using the provided App ID
        public bool TryConnect(string appId)
        {
            // Basic validation for App ID
            if (string.IsNullOrWhiteSpace(appId))
                return false;

            // Dispose previous client if exists
            _client?.Dispose();

            // Create a new Discord RPC client
            _client = new DiscordRpcClient(appId);

            try
            {
                // Try to initialize the connection
                return _client.Initialize();
            }
            catch
            {
                // If anything fails, just return false
                return false;
            }
        }
    }
}