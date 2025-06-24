using System.Text;
using PepperParser.Models;

namespace PepperParser.Helpers;

public class TextProcessing
{
    public string CreatePostCard(PepperProduct product)
    {
        StringBuilder stringBuilder = new();
        
        stringBuilder.Append($"*{product.Name}*");
        
        if (product.StoreTag is not null)
        {
            product.StoreTag = product.StoreTag.Replace(" ", "")
                .Replace(".", "")
                .Replace("\\", "");
            stringBuilder.Append($" \\| \\#{product.StoreTag}");
        }
        
        if (product.NewPrice is not null)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append($"\ud83d\udcb4 {product.NewPrice}");
            
            if (product.OldPrice is not null)
                stringBuilder.Append($" / ~{product.OldPrice}~");
            
            if (product.DiscountPercent is not null)
                stringBuilder.Append($" {product.DiscountPercent}");
        }

        if (product.Promo is not null)
        {
            stringBuilder.AppendLine();
            stringBuilder.Append($"\ud83c\udf40 Промокод: `{product.Promo}`");
        }
        
        if (product.ThreadLink is not null)
        {
            stringBuilder.AppendLine();
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"\u27a1\ufe0f [Ссылка на тему pepper](https://www.pepper.ru{product.ThreadLink})");
        }
        
        if (product.StraightLink is not null)
        {
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"\ud83d\udc49 [Прямая ссылка на товар]({product.StraightLink})");
        }

        return stringBuilder.ToString();
    }

    public PepperProduct ProductEscaping(PepperProduct product)
    {
        return new PepperProduct()
        {
            Name = TextEscaping(product.Name),
            NewPrice = TextEscaping(product.NewPrice),
            OldPrice = TextEscaping(product.OldPrice),
            DiscountPercent = TextEscaping(product.DiscountPercent),
            StoreTag = TextEscaping(product.StoreTag),
            Promo = TextEscaping(product.Promo),
            PhotoLink = TextEscaping(product.PhotoLink),
            ThreadLink = TextEscaping(product.ThreadLink),
            RedirectLink = TextEscaping(product.RedirectLink),
            StraightLink = TextEscaping(product.StraightLink),
            Description = TextEscaping(product.Description)
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