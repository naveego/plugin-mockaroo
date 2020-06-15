using Newtonsoft.Json;

namespace PluginMockaroo.DataContracts
{
    public class MockarooError
    {
        [JsonProperty("error")]
        public string Error { get; set; }
    }
}