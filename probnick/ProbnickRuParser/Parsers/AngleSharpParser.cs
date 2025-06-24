using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Io.Network;
using ProbnickRuParser.Models;

namespace ProbnickRuParser.Parsers;

public class AngleSharpParser
{
    private const string Url = "https://probnick.ru/";
    private readonly IBrowsingContext _context;
    
    public AngleSharpParser()
    {
        var handler = new HttpClientHandler
        {
            UseDefaultCredentials = false,
            AllowAutoRedirect = true
        };

        var client = new HttpClient(handler);
        client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:134.0) Gecko/20100101 Firefox/134.0");

        var config = Configuration.Default.WithDefaultLoader().WithTemporaryCookies().With(new HttpClientRequester(client));
        _context = BrowsingContext.New(config);
    }

    public async Task<Offer> GetLastOffer()
    {
        var probnickDocument = await _context.OpenAsync(Url);
        var firstArticle = GetArticleDoc(probnickDocument);

        return new Offer()
        {
            Text = GetTextContent(firstArticle),
            OfferLink = GetLinkToOffer(firstArticle),
            PhotoLink = GetPhotoLink(firstArticle),
            Description = GetDescription(firstArticle),
        };
    }
    
    private IElement? GetArticleDoc(IDocument document)
    {
        return document?.QuerySelector("article.postBox");
    }
    
    private string? GetPhotoLink(IElement firstArticle)
    {
        return firstArticle?.QuerySelector("img")?.GetAttribute("src");
    }
    
    private string? GetTextContent(IElement firstArticle)
    {
        return firstArticle?.QuerySelector("h2")?.TextContent;
    }
    
    private string? GetDescription(IElement firstArticle)
    {
        return firstArticle?.QuerySelector("div.textPreview")?.TextContent;
    }
    
    private string? GetLinkToOffer(IElement firstArticle)
    {
        return firstArticle?.QuerySelector("div.more-link")?.QuerySelector("a")?.GetAttribute("href");
    }
}