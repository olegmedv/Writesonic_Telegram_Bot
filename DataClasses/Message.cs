using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWritesonic.Classes
{
    public class TelegramMessage
    {
        public int Id { get; set; }
        public TelegramUser TelegramUser { get; set; } 
        public int TelegramUserId { get; set; }
        public string Text { get; set; }
        public bool IsSent { get; set; }
    }
}
