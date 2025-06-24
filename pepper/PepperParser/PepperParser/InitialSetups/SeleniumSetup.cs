using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;

namespace PepperParser.InitialSetups;

public class SeleniumSetup
{
    private const string SeleniumUrl = "http://firefox:4444/wd/hub";
    private readonly FirefoxOptions _firefoxOptions = new();
    private readonly IWebDriver _driver;
    
    public SeleniumSetup()
    {
        _firefoxOptions.AddArguments(new List<string>()
        {
            "--headless"
        });
        //некоторые аргументы только для хрома
        
        _driver = new RemoteWebDriver(new Uri(SeleniumUrl), _firefoxOptions);
    }
    
    public IWebDriver Driver
    {
        get { return _driver; }
    }
}