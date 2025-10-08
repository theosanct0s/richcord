using System;
using System.Windows;
using richcord.Services;

namespace richcord
{
    /// <summary>
    /// Main window for the Custom Rich Presence app
    /// </summary>
    public partial class MainWindow : Window
    {
        // Service to handle Discord Rich Presence logic
        private readonly DiscordPresenceService _discordService = new DiscordPresenceService();

        public MainWindow()
        {
            InitializeComponent();
        }

        // Handles the Discord connection test when the button is clicked
        private void btnTestConnection_Click(object sender, RoutedEventArgs e)
        {
            // Grab the App ID from the input field
            var appId = txtDiscordAppId.Text.Trim();

            // Try to connect to Discord using the service
            var connected = _discordService.TryConnect(appId);

            // Show a friendly message based on the result
            if (connected)
            {
                MessageBox.Show("Successfully connected to Discord!", "Connection Test", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to connect. Please check your Application ID and make sure Discord is running.", "Connection Test", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Handles click event for "Update Presence" button
        private void btnSendPresence_Click(object sender, RoutedEventArgs e)
        {
            // This will be implemented in the next feature!
            MessageBox.Show("Presence update not implemented yet.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Prepares the Discord RPC client (future use)
        // private void SetupDiscordRpcClient()
        // {
        //     discordClient = new DiscordRpcClient(discordAppId);
        //     discordClient.Initialize();
        // }

        // Updates Discord Rich Presence (future use)
        // private void UpdateDiscordPresence(string appId, string details, string state)
        // {
        //     discordClient.SetPresence(new RichPresence
        //     {
        //         Details = details,
        //         State = state,
        //         Timestamps = Timestamps.Now
        //     });
        // }
    }
}
