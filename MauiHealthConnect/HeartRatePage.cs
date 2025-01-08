using MauiHealthConnect.Platforms.Android;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MauiHealthConnect
{
    public class HeartRatePage : ContentPage
    {
        public HeartRatePage()
        {
            // Create a ListView to display the heart rate records
            var heartRateListView = new ListView
            {
                IsPullToRefreshEnabled = true,
                ItemTemplate = new DataTemplate(() =>
                {
                    var label = new Label();
                    label.SetBinding(Label.TextProperty, ".");
                    return new ViewCell { View = label };
                })
            };

            // Create a Label for status
            var statusLabel = new Label
            {
                Text = "Loading...",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.Center
            };

            // Create a StackLayout to hold the ListView and StatusLabel
            var stackLayout = new StackLayout
            {
                Children = { statusLabel, heartRateListView }
            };

            // Set the content of the page
            Content = stackLayout;

            // Request permissions and fetch heart rate records when the page is loaded
            Task.Run(() => LoadHeartRateData(heartRateListView, statusLabel));
        }

        private async Task LoadHeartRateData(ListView heartRateListView, Label statusLabel)
        {
            // Request Health Connect permissions
            var healthConnectWrapper = new HealthConnectService(Platform.AppContext);
            try
            {
                var heartRateRecordss = healthConnectWrapper.GetHeartRateRecords();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var heartRateRecords = healthConnectWrapper.GetHeartRateRecords();

            // If there are records, bind them to the ListView
            if (heartRateRecords != null && heartRateRecords.Any())
            {
                heartRateListView.ItemsSource = heartRateRecords;
                statusLabel.Text = $"Found {heartRateRecords.Count} heart rate records.";
            }
            else
            {
                statusLabel.Text = "No heart rate data found.";
            }
            // Check if permissions are granted
            /*if (healthConnectWrapper.RequestPermissions(Platform.AppContext))
            {
                // Fetch heart rate records
                var heartRateRecords = healthConnectWrapper.GetHeartRateRecords();

                // If there are records, bind them to the ListView
                if (heartRateRecords != null && heartRateRecords.Any())
                {
                    heartRateListView.ItemsSource = heartRateRecords;
                    statusLabel.Text = $"Found {heartRateRecords.Count} heart rate records.";
                }
                else
                {
                    statusLabel.Text = "No heart rate data found.";
                }
            }
            else
            {
                statusLabel.Text = "Permission denied. Unable to access heart rate data.";
            }*/
        }
    }
}