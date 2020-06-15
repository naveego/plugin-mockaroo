using System;
using System.Threading.Tasks;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using PluginMockaroo.API.Factory;
using PluginMockaroo.Helper;

namespace PluginMockaroo.API.Discover
{
    public static partial class Discover
    {
        public static Task<Count> GetCountOfRecords(IApiClient apiClient, Schema schema)
        {
            var mockSchema = JsonConvert.DeserializeObject<MockSchema>(schema.PublisherMetaJson);
            return Task.FromResult(new Count
            {
                Kind = Count.Types.Kind.Exact,
                Value = mockSchema.Count
            });
        }
    }
}