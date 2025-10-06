using System;
using System.Windows;

namespace richcord
{
    /// <summary>
    /// Main window for the Custom Rich Presence app
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // Handles click event for "Update Presence" button
        private void btnSendPresence_Click(object sender, RoutedEventArgs e)
        {
            string discordAppId = txtDiscordAppId.Text.Trim();
            string statusDetails = txtStatusDetails.Text.Trim();
            string statusState = txtStatusState.Text.Trim();

            // Simple check: ensure all fields are filled
            if (string.IsNullOrEmpty(discordAppId) || string.IsNullOrEmpty(statusDetails) || string.IsNullOrEmpty(statusState))
            {
                MessageBox.Show("Please fill in Application ID, Details, and State.", "Missing Info", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Log information for development purposes
            Console.WriteLine($"AppID: {discordAppId}, Details: {statusDetails}, State: {statusState}");

            // Simulated update of Discord Rich Presence
            MessageBox.Show("Presence updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Example for future integration:
            // UpdateDiscordPresence(discordAppId, statusDetails, statusState);
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
