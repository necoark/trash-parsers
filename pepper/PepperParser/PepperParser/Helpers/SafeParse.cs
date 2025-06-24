using OpenQA.Selenium;

namespace PepperParser.Helpers;

public class SafeParse
{
    public IWebElement FindByCssSafe(IWebElement element, string cssSelector)
    {
        try
        {
            return element.FindElement(By.CssSelector(cssSelector));
        }
        catch (Exception)
        {
            return null;
        }
    }
    
    public IWebElement FindElementSafe(IWebElement element, By by)
    {
        try
        {
            return element.FindElement(by);
        }
        catch (Exception)
        {
            return null;
        }
    }
}