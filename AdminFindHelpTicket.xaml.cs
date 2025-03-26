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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Marketplace_SE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminFindHelpTicket : Page
    {
        public AdminFindHelpTicket()
        {
            this.InitializeComponent();
        }
        private void CheckedCheckBoxAdminFindTicketSearchExactDate(object sender, RoutedEventArgs e)
        {
            CheckBoxAdminFindTicketSearchStartingDate.IsChecked = false;
            CheckBoxAdminFindTicketSearchEndingDate.IsChecked = false;
        }
        private void UncheckedCheckBoxAdminFindTicketSearchExactDate(object sender, RoutedEventArgs e)
        {

        }
        private void CheckedCheckBoxAdminFindTicketSearchStartingDate(object sender, RoutedEventArgs e)
        {
            CheckBoxAdminFindTicketSearchExactDate.IsChecked = false;
            CheckBoxAdminFindTicketSearchEndingDate.IsChecked = false;
        }
        private void UncheckedCheckBoxAdminFindTicketSearchStartingDate(object sender, RoutedEventArgs e)
        {

        }
        private void CheckedCheckBoxAdminFindTicketSearchEndingDate(object sender, RoutedEventArgs e)
        {
            CheckBoxAdminFindTicketSearchStartingDate.IsChecked = false;
            CheckBoxAdminFindTicketSearchExactDate.IsChecked = false;
        }
        private void UncheckedCheckBoxAdminFindTicketSearchEndingDate(object sender, RoutedEventArgs e)
        {

        }
        private void OnButtonClickAdminSearchHelpTicket(object sender, RoutedEventArgs e)
        {
            TextBlockAdminFindHelpTicketUserIDNotFound.Visibility = Visibility.Collapsed;
            TextBlockAdminFindHelpTicketWrongDate.Visibility = Visibility.Collapsed;
            TextBlockAdminFindHelpTicketChooseCheckbox.Visibility = Visibility.Collapsed;
            TextBlockAdminFindHelpTicketChosenDayCantBeInFuture.Visibility = Visibility.Collapsed;

            bool errorInInput = false;

            if (TextBoxLookupHelpTicketUserID.Text != "1000")
            {
                TextBlockAdminFindHelpTicketUserIDNotFound.Visibility = Visibility.Visible;
                errorInInput = true;
            }

            if(!(CheckBoxAdminFindTicketSearchStartingDate.IsChecked ?? false) && !(CheckBoxAdminFindTicketSearchExactDate.IsChecked ?? false) && !(CheckBoxAdminFindTicketSearchEndingDate.IsChecked ?? false))
            {
                TextBlockAdminFindHelpTicketChooseCheckbox.Visibility = Visibility.Visible;
                errorInInput = true;
            }

            DateTime parsedDate;
            bool isDateValid = DateTime.TryParseExact(TextBoxLookupHelpTicketCreationDate.Text, "dd-MM-yyyy",
                                                      System.Globalization.CultureInfo.InvariantCulture,
                                                      System.Globalization.DateTimeStyles.None,
                                                      out parsedDate);
            DateTime today = DateTime.Today;
            int compare = DateTime.Compare(today, parsedDate);
            if(compare < 0)
            {
                TextBlockAdminFindHelpTicketChosenDayCantBeInFuture.Visibility = Visibility.Visible;
                errorInInput = true;
            }

            if (!isDateValid)
            {
                TextBlockAdminFindHelpTicketWrongDate.Visibility = Visibility.Visible;
                errorInInput = true;
            }

            if(!errorInInput)
            {
                //call backend

                //TEMPORARY
                Frame.Navigate(typeof(ViewHelpTicket));
            }
        }
        private void OnButtonClickNavigateAdminSearchHelpTicketPageAdminAccountPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminAccountPage));
        }
    }
}
