using System.Net;
using System.Net.Http;
using VCE.Parser.Enum;

namespace VCE.Parser.Helper;

public class HttpClientHelper
{
    private ProxyHelper _proxyHelper;

    public HttpClientHelper()
    {
        _proxyHelper = new ProxyHelper();
    }

    public HttpClient CreateHttpClient(Chapter chapter)
    {
        HttpClient httpClient = new HttpClient();
        HttpHeadersConfig(chapter, httpClient);
        SetProxy(httpClient);

        return httpClient;
    }


    private void HttpHeadersConfig(Chapter chapter, HttpClient httpClient)
    {
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Linux; Android 6.0; Nexus 5 Build/MRA58N) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/129.0.0.0 Mobile Safari/537.36");
        if (chapter == Chapter.ChapterCars)
        {
            httpClient.DefaultRequestHeaders.Add("X-Requested-With", "XMLHttpRequest");
            httpClient.DefaultRequestHeaders.Add("Referer", "https://vce.com.ua/products/05p634-lpr-tormoznye-kolodki-zadnie");
        }
    }

    private void SetProxy(HttpClient httpClient)
    {
        try
        {
            var proxy = _proxyHelper.GetNextProxy();
            string proxyURL = $"http://{proxy.IpAddres}:{proxy.Port}";

            string proxyUsername = proxy.Username;
            string proxyPassword = proxy.Password;

            WebProxy webProxy = new WebProxy
            {
                Address = new Uri(proxyURL),
                Credentials = new NetworkCredential(
                    userName: proxyUsername,
                    password: proxyPassword
                )
            };

            HttpClientHandler httpClientHandler = new HttpClientHandler
            {
                Proxy = webProxy
            };

            httpClient = new HttpClient(httpClientHandler);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при настройке прокси: {ex.Message}");
        }
    }
}
