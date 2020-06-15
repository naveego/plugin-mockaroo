using System;
using System.Collections.Generic;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using PluginMockaroo.API.Factory;
using PluginMockaroo.Helper;

namespace PluginMockaroo.API.Read
{
    public static partial class Read
    {
        public static async IAsyncEnumerable<Record> ReadRecordsAsync(IApiClient apiClient, Schema schema)
        {
            var mockSchema = JsonConvert.DeserializeObject<MockSchema>(schema.PublisherMetaJson);
            var path = $"generate.json?array=true&count={mockSchema.Count}&schema={mockSchema.Name}";
            var response = await apiClient.PostAsync(path, null);

            var recordsList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(await response.Content.ReadAsStringAsync());

            foreach (var recordMap in recordsList)
            {
                yield return new Record
                {
                    Action = Record.Types.Action.Upsert,
                    DataJson = JsonConvert.SerializeObject(recordMap)
                };
            }
        }
    }
}