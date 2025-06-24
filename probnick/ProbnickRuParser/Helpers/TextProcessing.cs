using System.Text;
using ProbnickRuParser.Models;

namespace ProbnickRuParser.Helpers;

public class TextProcessing
{
    public string CreatePostCard(Offer offer)
    {
        StringBuilder stringBuilder = new();
        
        stringBuilder.Append($"*{offer.Text}*");
        stringBuilder.AppendLine();
        stringBuilder.Append($"_{offer.Description}_");
        stringBuilder.AppendLine();
        stringBuilder.Append($"[Подробнее]({offer.OfferLink})");
        
        return stringBuilder.ToString();
    }

    public Offer ProductEscaping(Offer offer)
    {
        return new Offer()
        {
            Text = TextEscaping(offer.Text),
            Description = TextEscaping(offer.Description),
            OfferLink = TextEscaping(offer.OfferLink),
            PhotoLink = offer.PhotoLink
        };
    }

    private string? TextEscaping(string? text)
    {
        if (text is null)
            return null;
        
        return text.Replace(".", "\\.")
            .Replace("*", "\\*")
            .Replace("[", "\\[")
            .Replace("]", "\\]")
            .Replace("(", "\\(")
            .Replace(")", "\\)")
            .Replace("+", "\\+")
            .Replace("#", "\\#")
            .Replace(">", "\\>")
            .Replace("`", "\\`")
            .Replace("~", "\\~")
            .Replace("_", "\\_")
            .Replace("-", "\\-")
            .Replace("=", "\\=")
            .Replace("|", "\\|")
            .Replace("{", "\\{")
            .Replace("}", "\\}")
            .Replace("/", "\\/")
            .Replace("!", "\\!");
    }
}