using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientChat.Objects
{
    public class Message
    {
        public int id;
        public int conversationId;
        public int creator;
        public int timestamp;
        public string contentType;
        public string content;
    }
}
