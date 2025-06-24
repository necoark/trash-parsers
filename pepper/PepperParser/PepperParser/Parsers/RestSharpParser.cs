using System.Collections.ObjectModel;
using System.Net;
using PepperParser.InitialSetups;
using RestSharp;
using Cookie = OpenQA.Selenium.Cookie;

namespace PepperParser.Parsers;

public class RestSharpParser
{
    private readonly RestClientOptions _restClientOptions;
    
    public RestSharpParser(RestSharpSetup restSharpSetup)
    {
        _restClientOptions = restSharpSetup.RestClientOptions;
    }
    
    public string? GetStraightLink(string redirectLink, ReadOnlyCollection<Cookie> cookies)
    {
        if (redirectLink == "/new" || redirectLink is null)
            return null;
        
        var client = new RestClient(_restClientOptions);
        var request = new RestRequest(redirectLink + "?ts=direct&dduid=0&client=desktop");
        FillHeaders(request);
        AddCookies(request, cookies);
        
        var response = client.ExecuteGet(request);
        return response.StatusCode != HttpStatusCode.Found ? null : ParseUrl(response);
    }

    private void FillHeaders(RestRequest request)
    {
        request.AddHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:134.0) Gecko/20100101 Firefox/134.0");
        request.AddHeader("accept", "text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
        request.AddHeader("accept-language", "ru-RU,ru;q=0.8,en-US;q=0.5,en;q=0.3");
        request.AddHeader("accept-encoding", "gzip, deflate, br, zstd");
        request.AddHeader("upgrade-insecure-requests", "1");
        request.AddHeader("sec-fetch-dest", "document");
        request.AddHeader("sec-fetch-mode", "navigate");
        request.AddHeader("sec-fetch-site", "same-origin");
        request.AddHeader("sec-fetch-user", "?1");
        request.AddHeader("priority", "u=0, i");
        request.AddHeader("te", "trailers");
    }

    private void AddCookies(RestRequest request, ReadOnlyCollection<Cookie> cookies)
    {
        foreach (var cookie in cookies)
        {
            if (cookie.Name == "") continue;
            request.AddCookie(cookie.Name, cookie.Value, cookie.Path, cookie.Domain);
        }
    }
    
    private string ParseUrl(RestResponse response)
    {
        foreach (var header in response.Headers)
        {
            if (header.Name == "Location")
            {
                string substring = "url=";
                int indexOfSubstring = header.Value.IndexOf(substring);
                var url = header.Value.Substring(indexOfSubstring + 4, header.Value.Length - indexOfSubstring - 4);
                return url;
            }
        }

        return null;
    }
}