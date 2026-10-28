using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.MessagesFiles
{
    
    public class PrivateMessageRelay : TcpMessage
    {
        public PrivateMessageRelay() { Type = "PrivateMessage"; }
        public string FromUser { get; set; } 
        public string Message { get; set; }   
    }
}
