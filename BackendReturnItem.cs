using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Notifications;

namespace Marketplace_SE
{
    static class BackendReturnItem
    {

        public enum BackendReturnItemCodes : int
        {
            PushNewReturnTicketToDBSuccess,
            PushNewReturnTicketToDBFailure,
            UpdateReturnTicketInDBSuccess,
            UpdateReturnTicketInDBFailure,
            ClosedReturnTicketInDBSuccess,
            ClosedReturnTicketInDBFailure
        }

        public static int PushNewReturnTicketToDB(string ReturnID, string ReturnName, string Price, string Description, string Approach)
        {

            Database.database = new Database(@"");
            bool status = Database.database.Connect();

            if (!status)
            {
                //database connection failed
                //ShowDialog("Database connection error", "Error connecting to database");

                Notification notification = new Notification("Database connection error", "Error connecting to database");
                notification.OkButton.Click += (s, e) =>
                {
                    notification.GetWindow().Close();
                    Database.database.Close();
                };
                notification.GetWindow().Activate();
                return (int)BackendReturnItemCodes.PushNewReturnTicketToDBFailure;
            }

            Database.database.Execute("INSERT INTO dbo.UserGetHelpTickets (ReturnID, ReturnName, Price, Description, Approach) VALUES (@RID, @RN, @P, @D, @A)",
                    new string[]
                    {
                        "@RID",
                        "@RN",
                        "@P",
                        "@D",
                        "@A"
                    }, new object[]
                    {
                        ReturnID,
                        ReturnName,
                        Price,
                        Description,
                        Approach
                    }
                );

            Database.database.Close();

            return (int)BackendReturnItemCodes.PushNewReturnTicketToDBSuccess;
        }
    }
}
