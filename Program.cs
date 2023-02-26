using RestSharp;
using Telegram.Bot.Types;
using Telegram.Bot;
using ConsoleAppWritesonic.Classes;
using ConsoleAppWritesonic.Service;

internal class Program
{
    private static DataService dataService = new DataService();
    private static string token { get; set; } = "";

    private class ResponseContent
    {
        public string Message { get; set; }
        public List<string> image_urls { get; set; }
    }

    private class JsonRequestParameter
    {
        public bool enable_google_results { get; set; }
        public bool enable_memory { get; set; }
        public string input_text { get; set; }
        public List<HistoryData>? history_data { get; set; }
    }

    private class HistoryData
    {
        public bool is_sent { get; set; }
        public string message { get; set; }
    }

    private static void Main(string[] args)
    {
        var client = new TelegramBotClient(token);
        client.StartReceiving(Update, Error);
        Console.ReadLine();
    }


    private async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken arg3)
    {
        var message = update.Message;
        if (message == null || message.Text == null || message.Text == String.Empty)
        {
            return;
        }
        var telegramUser = new TelegramUser()
        {
            Username = message.From.Username,
            TelegramUserId = message.From.Id,
            EnableGoogleResults = false,
            EnableMemory = false,
        };
        switch (message.Text)
        {
            case "/enable_google_results_on":
                telegramUser.EnableGoogleResults = true;
                dataService.GetUpdateTelegramUser(telegramUser);
                break;
            case "/enable_google_results_off":
                telegramUser.EnableGoogleResults = false;
                dataService.GetUpdateTelegramUser(telegramUser);
                break;
            case "/enable_memory_on":
                telegramUser.EnableMemory = true;
                dataService.GetUpdateTelegramUser(telegramUser);
                break;
            case "/enable_memory_off":
                telegramUser.EnableMemory = false;
                telegramUser = dataService.GetUpdateTelegramUser(telegramUser);
                dataService.DeleteMessages(telegramUser.Id);
                break;
            default:                
                var telegramUserBD = dataService.GetCreateTelegramUser(telegramUser);
                var responseString = await ProcessMessage(message, telegramUserBD, botClient);
                await botClient.SendTextMessageAsync(message.Chat.Id, responseString);
                break;
        }        
    }

    private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
    {
        throw new NotImplementedException();
    }

    private static async Task<string> ProcessMessage(Telegram.Bot.Types.Message message, TelegramUser telegramUser, ITelegramBotClient botClient)
    {
        var listMessages = new List<HistoryData>();
        if (telegramUser.EnableMemory)
        {
            listMessages = dataService.GetMessages(telegramUser.Id).Select(a => new HistoryData()
            {
                is_sent = a.IsSent,
                message = a.Text,
            }).ToList();
        }

        var answer = "answer";
        var apiKeyId = 1;
        var allApiKeysCount = dataService.GetApiKeys().Count();
        while (apiKeyId <= allApiKeysCount)
        {
            await botClient.SendChatActionAsync(message.Chat.Id, Telegram.Bot.Types.Enums.ChatAction.Typing);
            answer = await PostChatSonic(message.Text, telegramUser, apiKeyId, listMessages);
            if (answer != System.Net.HttpStatusCode.NotAcceptable.ToString()) {
                break;
            }
            apiKeyId++;
        }
        

        if (telegramUser.EnableMemory)
        {
            dataService.CreateMessage(new TelegramMessage()
            {
                TelegramUserId = telegramUser.Id,
                Text = message.Text,
                IsSent = true,
            });
            dataService.CreateMessage(new TelegramMessage()
            {
                TelegramUserId = telegramUser.Id,
                Text = answer,
                IsSent = false,
            });
        }


        return answer;
    }
    private static async Task<string> PostChatSonic(string inputText, TelegramUser telegramUser, int apiKeyId, List<HistoryData>? listMessages = null)
    {
        var apiKey = dataService.GetApiKeys().FirstOrDefault(a=> a.Id == apiKeyId).Key;
        var client = new RestClient("https://api.writesonic.com/v2/business/content/chatsonic?engine=premium");
        var request = new RestRequest("", Method.Post);
        request.AddHeader("accept", "application/json");
        request.AddHeader("content-type", "application/json");
        request.AddHeader("X-API-KEY", apiKey);

        var jsonRequestParameter = new JsonRequestParameter()
        {
            enable_google_results = telegramUser.EnableGoogleResults,
            enable_memory = telegramUser.EnableMemory,
            input_text = inputText,
            history_data = listMessages
        };
        var jsonRequest = Newtonsoft.Json.JsonConvert.SerializeObject(jsonRequestParameter);
        request.AddParameter("application/json", jsonRequest, ParameterType.RequestBody);
        var response = client.Execute(request);
        if (response.IsSuccessful && response.StatusCode == System.Net.HttpStatusCode.OK && response.Content != string.Empty)
        {
            var responseContent = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseContent>(response.Content);
            if (responseContent.Message != string.Empty)
            {
                return responseContent.Message;
            }
            else
            {
                return "empty";
            }
        }
        else
        {
            return response.StatusCode.ToString();
        }
    }
}