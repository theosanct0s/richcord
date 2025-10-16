using System;
using System.Timers;
using DiscordRPC;

namespace richcord.Services
{
    /// <summary>
    /// Service that manages the Discord Rich Presence connection, updates, and lifecycle.
    /// </summary>
    public class DiscordPresenceService : IDisposable
    {
        private DiscordRpcClient _client;
        private Timer _updateTimer;
        private RichPresence _currentPresence;
        private bool _isConnected;

        /// <summary>
        /// Connects to Discord using the provided Application ID.
        /// Starts the update loop if successful.
        /// </summary>
        public bool Connect(string appId)
        {
            if (string.IsNullOrWhiteSpace(appId) || !ulong.TryParse(appId, out _))
                return false;

            Disconnect();

            _client = new DiscordRpcClient(appId);

            try
            {
                _isConnected = _client.Initialize();
                if (_isConnected)
                {
                    _updateTimer = new Timer(2000);
                    _updateTimer.Elapsed += (s, e) => UpdatePresenceLoop();
                    _updateTimer.Start();
                }
                return _isConnected;
            }
            catch
            {
                _isConnected = false;
                return false;
            }
        }

        /// <summary>
        /// Updates the Rich Presence information shown on the user's Discord profile.
        /// </summary>
        public void UpdatePresence(string details, string state, string largeImageKey, string smallImageKey)
        {
            if (!_isConnected) return;

            _currentPresence = new RichPresence
            {
                Details = details,
                State = state,
                Timestamps = Timestamps.Now,
                Assets = new Assets
                {
                    LargeImageKey = largeImageKey,
                    SmallImageKey = smallImageKey
                }
            };
            _client.SetPresence(_currentPresence);
        }

        // Internal loop to keep Rich Presence updated on Discord Desktop
        private void UpdatePresenceLoop()
        {
            if (_isConnected && _currentPresence != null)
            {
                _client.SetPresence(_currentPresence);
            }
        }

        /// <summary>
        /// Disconnects from Discord and releases all resources.
        /// </summary>
        public void Disconnect()
        {
            _updateTimer?.Stop();
            _updateTimer?.Dispose();
            _client?.Dispose();
            _isConnected = false;
        }

        /// <summary>
        /// Ensures all resources are released when the service is disposed.
        /// </summary>
        public void Dispose()
        {
            Disconnect();
        }
    }
}