using ConsoleAppWritesonic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppWritesonic.Service
{
    public class DataService
    {
        private ChatDBContext db = new ChatDBContext();

        public DataService() { }

        public IQueryable<ApiKey> GetApiKeys() { 
            var apiKeys = db.ApiKeys;
            return apiKeys;
        }

        public TelegramUser CreateTelegramUser(TelegramUser telegramUser)
        {
            var telegramUserDB = new TelegramUser()
            {
                EnableGoogleResults = telegramUser.EnableGoogleResults,
                EnableMemory = telegramUser.EnableMemory,
                Username = telegramUser.Username,
                TelegramUserId = telegramUser.TelegramUserId,
            };
            db.TelegramUsers.Add(telegramUserDB);
            db.SaveChanges();
            return telegramUserDB;
        }

        public TelegramUser GetCreateTelegramUser(TelegramUser telegramUser)
        {
            var telegramUserDB = db.TelegramUsers.FirstOrDefault(a => a.TelegramUserId == telegramUser.TelegramUserId);
            if (telegramUserDB == null)
            {
                telegramUserDB = CreateTelegramUser(telegramUser);
                return telegramUserDB;
            }
            else
            {
                return telegramUserDB;
            }
        }

        public TelegramUser GetUpdateTelegramUser(TelegramUser telegramUser)
        {
            var telegramUserDB = db.TelegramUsers.FirstOrDefault(a => a.TelegramUserId == telegramUser.TelegramUserId);
            if (telegramUserDB == null)
            {
                telegramUserDB = CreateTelegramUser(telegramUser);
                return telegramUserDB;
            }
            else
            {
                telegramUserDB.EnableGoogleResults = telegramUser.EnableGoogleResults;
                telegramUserDB.EnableMemory = telegramUser.EnableMemory;
                db.SaveChanges();
                return telegramUserDB;
            }
        }

        public void UpdateTelegramUser(TelegramUser telegramUser)
        {
            var telegramUserDB = db.TelegramUsers.FirstOrDefault(a => a.Id == telegramUser.Id);
            if (telegramUserDB != null)
            {
                telegramUserDB.Username = telegramUser.Username;
                telegramUserDB.EnableMemory = telegramUser.EnableMemory;
                telegramUserDB.EnableGoogleResults = telegramUser.EnableGoogleResults;
                db.SaveChanges();
            }
        }

        public void DeleteTelegramUser(long telegramUserId)
        {
            var telegramUserDB = db.TelegramUsers.FirstOrDefault(a => a.Id == telegramUserId);
            if (telegramUserDB != null)
            {
                db.TelegramUsers.Remove(telegramUserDB);
                db.SaveChanges();
            }
        }

        public IQueryable<TelegramMessage> GetMessages(long telegramUserId)
        {
            var messagesDB = db.TelegramMessages.Where(a => a.TelegramUserId == telegramUserId);
            return messagesDB;
        }

        public void CreateMessage(TelegramMessage message)
        {
            db.TelegramMessages.Add(message);
            db.SaveChanges();
        }

        public void DeleteMessages(int telegramUserId)
        {
            var messagesDB = db.TelegramMessages.Where(a => a.TelegramUserId == telegramUserId);
            if (messagesDB != null)
            {
                db.TelegramMessages.RemoveRange(messagesDB);
                db.SaveChanges();
            }
        }

    }
}
