using System.Threading.Tasks;

namespace PluginMockaroo.API.Factory
{
    public interface IApiAuthenticator
    {
        Task<string> GetToken();
    }
}