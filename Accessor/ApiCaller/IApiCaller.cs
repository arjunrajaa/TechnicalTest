using System.Threading.Tasks;

namespace Accessor.ApiCaller;

public interface IApiCaller
{
    void InitClient(string httpClientName ="",string baseUri = "");
    Task<string> GetApiResult(string endpoint);
}