using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace PepperParser.Models;

public class PepperProduct
{
    public string? Name { get; set; }
    public string? NewPrice { get; set; }
    public string? OldPrice { get; set; }
    public string? DiscountPercent { get; set; }
    public string? StoreTag { get; set; }
    public string? Promo { get; set; }
    public string? PhotoLink { get; set; }
    public string? ThreadLink { get; set; }
    public string? RedirectLink { get; set; }
    public string? StraightLink { get; set; }
    public string? Description { get; set; }
    public ReadOnlyCollection<Cookie> Cookies { get; set; }
}