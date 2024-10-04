
namespace VCE.Parser.Helper;

public class ProxyHelper
{
    object syncHandler = new object();
    private int currentProxyIndex;
    private Random random;
    public List<Proxy> Proxies { get; set; }

    public ProxyHelper()
    {
        random = new Random();
        currentProxyIndex = 0;
        Proxies = GetAllProxy("C:\\Users\\User\\source\\repos\\VCE.Parser\\VCE.Parser\\Data\\proxy.txt").ToList();
    }

    public Proxy GetNextProxy()
    {
        lock (Proxies)
        {
            if (Proxies.Count == 0)
            {
                throw new InvalidOperationException("No proxies available.");
            }

            var proxy = Proxies[currentProxyIndex];
            currentProxyIndex = (currentProxyIndex + 1) % Proxies.Count;
            return proxy;
        }
    }


    public IEnumerable<Proxy> GetAllProxy(string filePath)
    {
        List<Proxy> proxies = new List<Proxy>();
        string[] proxyData = File.ReadAllLines(filePath);

        foreach (var data in proxyData)
        {
            string[] parts = data.Split(':');
            if (parts.Length == 4)
            {
                proxies.Add(new Proxy
                {
                    IpAddres = parts[0],
                    Port = parts[1],
                    Password = parts[2],
                    Username = parts[3]
                });
            }
            else
            {
                Console.WriteLine($"Invalid data format: {data}");
            }
        }


        return proxies;
    }
}



public class Proxy
{
    public required string IpAddres { get; set; }
    public required string Port { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
}