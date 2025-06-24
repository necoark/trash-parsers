using RestSharp;

namespace PepperParser.InitialSetups;

public class RestSharpSetup
{
    private readonly RestClientOptions _restClientOptions;
    
    public RestSharpSetup()
    {
        _restClientOptions = new();
        _restClientOptions.FollowRedirects = false;
        _restClientOptions.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:134.0) Gecko/20100101 Firefox/134.0";
    }
    
    public RestClientOptions RestClientOptions
    {
        get { return _restClientOptions; }
    }
}