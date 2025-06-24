namespace PepperParser;

public static class Config
{
    private static readonly string _telegramBotToken;
    private static readonly string _telegramChannelId;

    static Config()
    {
        _telegramBotToken = GetTelegramBotToken();
        _telegramChannelId = GetTelegramChannelId();
    }
    
    public static string TelegramBotToken
    {
        get { return _telegramBotToken; }
    }

    public static string TelegramChannelId
    {
        get { return _telegramChannelId; }
    }

    private static string GetTelegramBotToken()
    {
        var tgBotToken = Environment.GetEnvironmentVariable("TG_BOT_TOKEN");
        return tgBotToken is not null ? tgBotToken : throw new Exception("TG_BOT_TOKEN не был передан");
    }
    
    private static string GetTelegramChannelId()
    {
        var tgChannelId = Environment.GetEnvironmentVariable("TG_NOTIFY_CHANNEL_ID");
        return tgChannelId is not null ? tgChannelId : throw new Exception("TG_NOTIFY_CHANNEL_ID не был передан");
    }
}