namespace ProbnickRuParser;

public static class Config
{
    private const string _telegramBotToken = "your_token";
    private const string _telegramChannelId = "your_channel_id";
    
    public static string TelegramBotToken
    {
        get { return _telegramBotToken; }
    }

    public static string TelegramChannelId
    {
        get { return _telegramChannelId; }
    }
}