using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.Common.ChatLogic
{
    public class Message
    {
        public string Content { get; set; }
        public string SenderName { get; set; }
        public bool IsMine { get; set; }
        public DateTime SendingTime { get; set; }
    }
}
