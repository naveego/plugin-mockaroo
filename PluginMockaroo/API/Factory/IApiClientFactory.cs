using System.Net.Http;
using PluginMockaroo.Helper;

namespace PluginMockaroo.API.Factory
{
    public interface IApiClientFactory
    {
        IApiClient CreateApiClient(Settings settings);
    }
}