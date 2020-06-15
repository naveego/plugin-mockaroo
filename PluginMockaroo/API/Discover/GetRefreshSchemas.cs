using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Google.Protobuf.Collections;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using PluginMockaroo.API.Factory;
using PluginMockaroo.Helper;

namespace PluginMockaroo.API.Discover
{
    public static partial class Discover
    {
        public static async IAsyncEnumerable<Schema> GetRefreshSchemas(IApiClient apiClient,
            RepeatedField<Schema> refreshSchemas, int sampleSize = 5)
        {
            foreach (var schema in refreshSchemas)
            {
                var mockSchema = JsonConvert.DeserializeObject<MockSchema>(schema.PublisherMetaJson);

                var refreshSchema = await GetSchemaForMockSchema(apiClient, schema, mockSchema);

                // get sample and count
                yield return await AddSampleAndCount(apiClient, refreshSchema, sampleSize);
            }
        }
    }
}