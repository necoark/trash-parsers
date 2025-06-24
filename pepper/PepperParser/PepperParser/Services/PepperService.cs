using PepperParser.Helpers;
using PepperParser.Parsers;

namespace PepperParser.Services;

public class PepperService
{
    private readonly SeleniumParser _seleniumParser;
    private readonly RestSharpParser _restSharpParser;
    private readonly TelegramService _telegramService;
    private readonly Cache _cache;
    private readonly TextProcessing _textProcessing;
    
    public PepperService(SeleniumParser seleniumParser, RestSharpParser restSharpParser, 
        TelegramService telegramService, Cache cache, TextProcessing textProcessing)
    {
        _seleniumParser = seleniumParser;
        _restSharpParser = restSharpParser;
        _telegramService = telegramService;
        _cache = cache;
        _textProcessing = textProcessing;
    }

    public async Task NotifyNewProduct()
    {
        try
        {
            var product = _seleniumParser.GetPartialProductInfo();
            if (product.ThreadLink is null) return;
            
            if (!_cache.HasLink(product.ThreadLink))
            {
                product.StraightLink = _restSharpParser.GetStraightLink(product.RedirectLink, product.Cookies);
                var escapedProduct = _textProcessing.ProductEscaping(product);
                var postCard = _textProcessing.CreatePostCard(escapedProduct);
                await _telegramService.SendMessageToChannel(postCard);
                _cache.AddLink(product.ThreadLink);
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