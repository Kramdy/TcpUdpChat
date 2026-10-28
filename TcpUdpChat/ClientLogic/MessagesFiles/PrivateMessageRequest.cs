using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.MessagesFiles
{
    
    public class PrivateMessageRequest : TcpMessage
    {
        public PrivateMessageRequest() { Type = "PrivateMessage"; }
        public string ToUser { get; set; } 
        public string Message { get; set; }  
    }
}
