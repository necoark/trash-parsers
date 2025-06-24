namespace ProbnickRuParser.Helpers;

public class Cache
{
    private List<string> _links;

    public Cache()
    {
        _links = new();
    }

    public void AddLink(string link)
    {
        _links.Add(link);
    }
    
    public bool HasLink(string link)
    {
        return _links.Contains(link);
    }

    public void ClearOver()
    {
        if (_links.Count > 50)
        {
            _links = _links.TakeLast(10).ToList();
        }
    }
}