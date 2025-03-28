using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System.Threading.Tasks;
using Marketplace_SE.HardwareSurvey;
using Marketplace_SE.Rating;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.
namespace Marketplace_SE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FinalizeOrderPage : Page
    {
        private RateAppComponent _rateAppComponent;
        private HardwareSurveyComponent _hardwareSurveyComponent;

        public FinalizeOrderPage()
        {
            this.InitializeComponent();

            // Create placeholder services (will be replaced with real implementations)
            var ratingService = new PlaceholderRatingService();
            var hardwareSurveyService = new PlaceholderHardwareSurveyService();
            var loggerService = new PlaceholderLoggerService();

            // Initialize components
            _rateAppComponent = new RateAppComponent(ratingService);
            _hardwareSurveyComponent = new HardwareSurveyComponent(hardwareSurveyService, loggerService);

            // Register for page loading event to ensure XamlRoot is set
            Loaded += FinalizeOrderPage_Loaded;
        }

        private void FinalizeOrderPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Set XamlRoot for dialog components
            _rateAppComponent.SetXamlRoot(this.XamlRoot);
            _hardwareSurveyComponent.SetXamlRoot(this.XamlRoot);
        }

        private void OnButtonClickNavigateFinalizeOrderPageAccountPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainMarketplacePage));
        }

        private async void OnButtonClickFinalizeOrder(object sender, RoutedEventArgs e)
        {
            try
            {
                // Set XamlRoot again just before showing dialogs
                //// System.Diagnostics.Debug.WriteLine("Setting XamlRoot");
                _rateAppComponent.SetXamlRoot(this.XamlRoot);

                // First show rating and survey dialogs
                //// System.Diagnostics.Debug.WriteLine("Starting rating and survey dialogs");

                // Debug the XamlRoot value
                //// System.Diagnostics.Debug.WriteLine($"XamlRoot is null: {this.XamlRoot == null}");

                // Show more detailed debug for each step
                //// System.Diagnostics.Debug.WriteLine("Calling OnTransactionCompleted");
                _rateAppComponent.OnTransactionCompleted();

                //// System.Diagnostics.Debug.WriteLine("Calling CheckAndShowRatingPromptAsync");
                bool ratingShown = await _rateAppComponent.CheckAndShowRatingPromptAsync();
                //// System.Diagnostics.Debug.WriteLine($"Rating dialog shown: {ratingShown}");

                // After rating dialog closes, show hardware survey using the explicit XamlRoot method
                //// System.Diagnostics.Debug.WriteLine("Calling ShowWithExplicitXamlRoot");
                bool surveyShown = await _hardwareSurveyComponent.ShowWithExplicitXamlRoot(this.XamlRoot);
                //// System.Diagnostics.Debug.WriteLine($"Hardware survey shown: {surveyShown}");

                // Then navigate back to marketplace
                //// System.Diagnostics.Debug.WriteLine("Navigating to marketplace");
                Frame.Navigate(typeof(MainMarketplacePage));
            }
            catch (Exception ex)
            {
                //// System.Diagnostics.Debug.WriteLine($"Error in FinalizeOrder: {ex.Message}");
                //// System.Diagnostics.Debug.WriteLine($"Stack trace: {ex.StackTrace}");
                // Still navigate to maintain functionality
                // Add a small delay before navigation
                await Task.Delay(500);
                //// System.Diagnostics.Debug.WriteLine("Navigating to marketplace");
                Frame.Navigate(typeof(MainMarketplacePage));
            }
        }

        // Placeholder services (will be replaced with actual implementations)
        private class PlaceholderRatingService : IRatingDatabaseService
        {
            public Task SaveRatingAsync(RatingData ratingData)
            {
                //// System.Diagnostics.Debug.WriteLine($"Rating saved: {ratingData.Rating}, Comment: {ratingData.Comment}");
                return Task.CompletedTask;
            }
        }

        private class PlaceholderHardwareSurveyService : IHardwareSurveyDatabaseService
        {
            public Task SaveHardwareDataAsync(HardwareData hardwareData)
            {
                //// System.Diagnostics.Debug.WriteLine("Hardware survey data saved");
                return Task.CompletedTask;
            }
        }

        private class PlaceholderLoggerService : ILoggerService
        {
            public void LogInfo(string message)
            {
                //// System.Diagnostics.Debug.WriteLine($"INFO: {message}");
            }

            public void LogError(string message)
            {
                //// System.Diagnostics.Debug.WriteLine($"ERROR: {message}");
            }
        }
    }
}