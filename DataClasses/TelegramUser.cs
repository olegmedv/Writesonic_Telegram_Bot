using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWritesonic.Classes
{
    public class TelegramUser
    {
        public int Id { get; set; }
        public long TelegramUserId { get; set; }
        public string Username { get; set; }   
        public bool EnableGoogleResults { get; set; }
        public bool EnableMemory { get; set; }
    }
}
