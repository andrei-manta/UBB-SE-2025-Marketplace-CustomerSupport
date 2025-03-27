using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Marketplace_SE
{
    static class BackendUserGetHelp
    {
        public enum BackendUserGetHelpStatusCodes : int
        {
            PushNewHelpTicketToDBSuccess,
            PushNewHelpTicketToDBFailure
        }
        public static int PushNewHelpTicketToDB(string UserID, string UserName, string Description)
        {
            //append current datetime, then push ticket in DB
            
            return (int)BackendUserGetHelpStatusCodes.PushNewHelpTicketToDBSuccess;
        }

        public static bool DoesUserIDExist(string UserID)
        {
            //look for it in DB

            return true;
        }

        public static List<HelpTicket> LoadTicketsFromDB(List<string> TicketIDs)
        {
            List<HelpTicket> returnList = new List<HelpTicket>();

            foreach(string each in TicketIDs)
            {
                //transform string to ID's format, call DB, construct HelpTicket, add to list
            }

            returnList.Add(new HelpTicket("0001", "1000", "User Name", "12-12-2022-09-22", "Lorem ipsum"));

            returnList.Add(new HelpTicket("0002", "1000", "User Name", "12-12-2022-10-22", "Lorem ipsum"));

            return returnList;
        }

        public static List<string> GetTicketIDsMatchingCriteria(string UserID, string DateAndTime, bool ExactDate, bool StartingDate, bool EndingDate)
        {
            List<string> returnList = new List<string>();

            //lookup in the DB and add to list

            return returnList;
        }
    }

    public class HelpTicket
    {
        private string TicketID { get; }
        private string UserID { get; }
        private string UserName { get; }
        private string DateHour { get; }
        private string Description { get; }
        public HelpTicket(string TicketID_, string UserID_, string UserName_, string DateHour_, string Description_)
        {
            TicketID = TicketID_;
            UserID = UserID_;
            UserName = UserName_;
            DateHour = DateHour_;
            Description = Description_;
        }

        public string toStringExceptDescription()
        {
            return "Ticket nr: " + TicketID.ToString() + "; User ID: " + UserID.ToString() + "; User name: " + UserName.ToString() + "; Date and time: " + DateHour.ToString();
        }
    }
}
