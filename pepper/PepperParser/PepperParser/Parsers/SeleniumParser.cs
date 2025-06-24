using System.Collections.ObjectModel;
using OpenQA.Selenium;
using PepperParser.Helpers;
using PepperParser.InitialSetups;
using PepperParser.Models;

namespace PepperParser.Parsers;

public class SeleniumParser
{
    private const string Url = "https://www.pepper.ru";
    private readonly IWebDriver _driver;
    private readonly SafeParse _safeParse;
    
    public SeleniumParser(SeleniumSetup seleniumService, SafeParse safeParse)
    {
        _driver = seleniumService.Driver;
        _safeParse = safeParse;
    }

    public PepperProduct GetPartialProductInfo()
    {
        GoToNewSite();
        var lastProduct = ParseLastProduct();
        _driver.Quit();
        _driver.Dispose();
        return lastProduct;
    }
    
    private PepperProduct ParseLastProduct()
    {
        var article = GetArticle();
        var customCard = GetCustomCard(article);
        
        return new PepperProduct()
        {
            Name = customCard.Text,
            NewPrice = GetNewPrice(article),
            OldPrice = GetOldPrice(article),
            DiscountPercent = GetDiscountPercent(article),
            StoreTag = GetStoreTag(article),
            Promo = GetPromo(article),
            PhotoLink = GetPhotoLink(article),
            ThreadLink = GetThreadLink(customCard),
            RedirectLink = GetRedirectLink(article),
            Cookies = GetCookies()
        };
    }
    
    private void GoToNewSite()
    {
        _driver.Navigate().GoToUrl(Url + "/new");
    }
    
    private ReadOnlyCollection<Cookie> GetCookies()
    {
        return _driver.Manage().Cookies.AllCookies;
    }

    private IWebElement GetArticle()
    {
        return _driver.FindElement(By.TagName("article"));
    }
    
    private IWebElement GetCustomCard(IWebElement element)
    {
        return _safeParse.FindByCssSafe(element, "div.custom-card-title");
    }
    
    private string? GetThreadLink(IWebElement element)
    {
        return _safeParse.FindElementSafe(element, By.TagName("a")).GetDomAttribute("href");
    }
    
    private string? GetRedirectLink(IWebElement element)
    {
        return _safeParse.FindByCssSafe(element, "a.w-full.h-full.flex.justify-center.items-center")?.GetDomAttribute("href");
    }
    
    private string? GetStoreTag(IWebElement element)
    {
        return _safeParse.FindByCssSafe(element, "a.gtm_store_visit_homepage")?.Text;
    }
    
    private string? GetNewPrice(IWebElement element)
    {
        return _safeParse.FindByCssSafe(element, "div.text-lg.font-bold.text-primary.mr-2")?.Text;
    }
    
    private string? GetOldPrice(IWebElement element)
    {
        return _safeParse.FindByCssSafe(element, "div.line-through.text-secondary-text-light")?.Text;
    }
    
    private string? GetDiscountPercent(IWebElement element)
    {
        return _safeParse.FindByCssSafe(element, "div.text-sm.text-secondary-text-light")?.Text;
    }
    
    private string? GetPromo(IWebElement element)
    {
        var tempElement = _safeParse.FindByCssSafe(element, "div.absolute.w-full.h-full.flex.items-center.justify-between");
        return _safeParse.FindElementSafe(tempElement, By.TagName("span"))?.Text;
    }
    
    private string? GetPhotoLink(IWebElement element)
    {
        return _safeParse.FindByCssSafe(element, "img.transition")?.GetDomAttribute("src");
    }
}