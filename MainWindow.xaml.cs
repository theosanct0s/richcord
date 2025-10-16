using System;
using System.Windows;
using richcord.Services;

namespace richcord
{
    /// <summary>
    /// Main window for the Custom Rich Presence application.
    /// Handles user interaction and communicates with the Discord Rich Presence service.
    /// </summary>
    public partial class MainWindow : Window
    {
        // Service responsible for managing Discord Rich Presence connection and updates
        private readonly DiscordPresenceService _discordService = new DiscordPresenceService();

        public MainWindow()
        {
            InitializeComponent();
            this.Closed += MainWindow_Closed;
        }

        // Connects to Discord and activates Rich Presence when the "Connect" button is clicked
        private void btnConnect_Click(object sender, RoutedEventArgs e)
        {
            var appId = txtAppId.Text.Trim();

            if (_discordService.Connect(appId))
            {
                // Immediately update Rich Presence after connecting
                var details = txtDetails.Text.Trim();
                var state = txtState.Text.Trim();
                var largeImageKey = txtLargeImageKey.Text.Trim();
                var smallImageKey = txtSmallImageKey.Text.Trim();

                _discordService.UpdatePresence(details, state, largeImageKey, smallImageKey);

                MessageBox.Show("Successfully connected to Discord and Rich Presence is now active on your profile!", "Connection Status", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Failed to connect. Please check your Application ID (numbers only) and make sure Discord is running.", "Connection Status", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        // Updates the Rich Presence information when the "Update Presence" button is clicked
        private void btnUpdatePresence_Click(object sender, RoutedEventArgs e)
        {
            var details = txtDetails.Text.Trim();
            var state = txtState.Text.Trim();
            var largeImageKey = txtLargeImageKey.Text.Trim();
            var smallImageKey = txtSmallImageKey.Text.Trim();

            try
            {
                _discordService.UpdatePresence(details, state, largeImageKey, smallImageKey);
                MessageBox.Show("Rich Presence updated on Discord!", "Presence Update", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating presence: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Disconnects from Discord and removes Rich Presence when the "Disconnect" button is clicked
        private void btnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            _discordService.Dispose();
            MessageBox.Show("Disconnected from Discord. Rich Presence has been removed from your profile.", "Disconnected", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Ensures resources are released when the window is closed
        private void MainWindow_Closed(object sender, EventArgs e)
        {
            _discordService.Dispose();
        }
    }
}
