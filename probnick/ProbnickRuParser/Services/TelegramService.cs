using ProbnickRuParser.Models;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace ProbnickRuParser.Services;

public class TelegramService
{
    private readonly string ChannelUsername;
    private readonly string BotToken;
    private readonly TelegramBotClient _botClient;

    public TelegramService()
    {
        ChannelUsername = Config.TelegramChannelId;
        BotToken = Config.TelegramBotToken;
        
        _botClient = new TelegramBotClient(BotToken);
        _botClient.OnError += OnError;
        _botClient.OnMessage += OnMessage;
    }

    public async Task SendMessageToChannel(string postCard, string photoLink)
    {
        await _botClient.SendPhoto(ChannelUsername,
            photoLink, postCard,
            ParseMode.MarkdownV2);
    }

    //ловит только в polling или в моих OnMessage/OnUpdate
    async Task OnError(Exception e, HandleErrorSource source)
    {
        Console.WriteLine($"Ошибка {e.Message} в методе {e.TargetSite}");
        Console.WriteLine(e);
    }
    
    async Task OnMessage(Message msg, UpdateType type)
    {
        if (msg.Chat.Type == ChatType.Private)
            await _botClient.SendMessage(msg.Chat, "Работаю");
    }
}