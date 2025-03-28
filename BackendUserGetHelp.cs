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

        public static List<string> GetTicketIDsMatchingCriteria(string UserID)
        {
            List<string> returnList = new List<string>();

            //lookup in the DB and add to list

            return returnList;
        }

        public static bool DoesTicketIDExist()
        {
            //look for it in DB

            return true;
        }
    }

    public class HelpTicket
    {
        public string TicketID { get; }
        public string UserID { get; }
        public string UserName { get; }
        public string DateHour { get; }
        public string Description { get; }
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
            return TicketID.ToString() + "::" + UserID.ToString() + "::" + UserName.ToString() + "::" + DateHour.ToString();
        }
    }
}
