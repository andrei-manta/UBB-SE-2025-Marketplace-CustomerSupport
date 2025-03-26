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
    }
}
