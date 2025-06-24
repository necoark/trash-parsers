using ProbnickRuParser.Helpers;
using ProbnickRuParser.Parsers;
using ProbnickRuParser.Services;

namespace ProbnickRuParser.Services;

public class ProbnickRuService
{
    private readonly TelegramService _telegramService;
    private readonly Cache _cache;
    private readonly TextProcessing _textProcessing;
    private readonly AngleSharpParser _angleSharpParser;
    
    public ProbnickRuService(TelegramService telegramService, Cache cache, 
        TextProcessing textProcessing, AngleSharpParser angleSharpParser)
    {
        _cache = cache;
        _textProcessing = textProcessing;
        _telegramService = telegramService;
        _angleSharpParser = angleSharpParser;
    }

    public async Task NotifyNewOffer()
    {
        try
        {
            var offer = await _angleSharpParser.GetLastOffer();
            
            if (!_cache.HasLink(offer.OfferLink))
            {
                var escapedOffer = _textProcessing.ProductEscaping(offer);
                var postCard = _textProcessing.CreatePostCard(escapedOffer);
                await _telegramService.SendMessageToChannel(postCard, offer.PhotoLink);
                _cache.AddLink(offer.OfferLink);
                _cache.ClearOver();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Ошибка {e.Message} в методе {e.TargetSite}");
            Console.WriteLine(e);
        }
    }
}