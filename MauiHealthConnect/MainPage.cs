using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MauiHealthConnect
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {

            // Button to navigate to HeartRatePage
            var button = new Button
            {
                Text = "View Heart Rate Data"
            };

            button.Clicked += async (sender, e) =>
            {
                await Navigation.PushAsync(new HeartRatePage());
            };

            Content = new StackLayout
            {
                Children = { button }
            };
        }
    }
    }