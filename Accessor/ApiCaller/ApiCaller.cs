using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Accessor.ApiCaller;

public class ApiCaller: IApiCaller
{
    private HttpClient _httpClient;
    private readonly IHttpClientFactory _httpClientFactory;
    public ApiCaller(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public void InitClient(string httpClientName = "", string baseUri = "")
    {
        _httpClient = _httpClientFactory.CreateClient(httpClientName);
        if (!String.IsNullOrEmpty(baseUri))
        {
            _httpClient.BaseAddress = new Uri(baseUri);
        }
    }
    
    public  async Task<string> GetApiResult(string endpoint)
    {
        using var response = await _httpClient.GetAsync(endpoint);
        var stringResult = await response.Content.ReadAsStringAsync();
        return stringResult;
    }
}