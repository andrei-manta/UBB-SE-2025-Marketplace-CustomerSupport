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
using Windows.Security.Cryptography.Core;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Marketplace_SE
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ViewHelpTicket : Page
    {
        string data = "";

        public ViewHelpTicket()
        {
            this.InitializeComponent();
        }
        private void OnButtonClickNavigateViewHelpTicketPageAdminAccountPage(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(AdminAccountPage));
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            List<string> ticketID = new List<string>();
            ticketID.Add(e.Parameter as string);
            HelpTicket currentTicket = BackendUserGetHelp.LoadTicketsFromDB(ticketID)[0];

            TextBlockViewHelpTicketNumber.Text = "Ticket number: " + currentTicket.TicketID;
            TextBoxViewHelpTicketUserID.Text = currentTicket.UserID;
            TextBoxViewHelpTicketUserName.Text = currentTicket.UserName;
            TextBlockViewHelpTicketDateAndTime.Text = "Date and time: " + currentTicket.DateAndTime;
            TextBoxViewHelpTicketDescription.Text = currentTicket.Descript;
        }
    }
}
