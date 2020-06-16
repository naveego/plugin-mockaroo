using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Naveego.Sdk.Plugins;
using Newtonsoft.Json;
using PluginMockaroo.API.Factory;
using PluginMockaroo.Helper;

namespace PluginMockaroo.API.Discover
{
    public static partial class Discover
    {
        public static async IAsyncEnumerable<Schema> GetAllSchemas(IApiClient apiClient, Settings settings, int sampleSize = 5)
        {
            foreach (var mockSchema in settings.MockSchemas)
            {
                // base schema to be added to
                var schema = new Schema
                {
                    Id = string.IsNullOrWhiteSpace(mockSchema.CustomName) ? mockSchema.Name : mockSchema.CustomName,
                    Name = string.IsNullOrWhiteSpace(mockSchema.CustomName) ? mockSchema.Name : mockSchema.CustomName,
                    Description = "",
                    PublisherMetaJson = JsonConvert.SerializeObject(mockSchema),
                    DataFlowDirection = Schema.Types.DataFlowDirection.Read
                };

                schema = await GetSchemaForMockSchema(apiClient, schema, mockSchema);

                // get sample and count
                yield return await AddSampleAndCount(apiClient, schema, sampleSize);
            }
        }

        private static async Task<Schema> AddSampleAndCount(IApiClient apiClient, Schema schema,
            int sampleSize)
        {
            // add sample and count
            var records = Read.Read.ReadRecordsAsync(apiClient, schema).Take(sampleSize);
            schema.Sample.AddRange(await records.ToListAsync());
            schema.Count = await GetCountOfRecords(apiClient, schema);

            return schema;
        }

        private static async Task<Schema> GetSchemaForMockSchema(IApiClient apiClient, Schema schema, MockSchema mockSchema)
        {
            var path = $"generate.json?array=true&count={10}&schema={mockSchema.Name}";
            var response = await apiClient.PostAsync(path, null);

            var recordsList = JsonConvert.DeserializeObject<List<Dictionary<string, object>>>(await response.Content.ReadAsStringAsync());
            var types = GetPropertyTypesFromRecords(recordsList);
            var record = recordsList.First();
                
            var properties = new List<Property>();
                
            foreach (var recordKey in record.Keys)
            {
                var property = new Property
                {
                    Id = recordKey,
                    Name = recordKey,
                    Type = types[recordKey],
                    IsKey = false,
                    IsCreateCounter = false,
                    IsUpdateCounter = false,
                    TypeAtSource = "",
                    IsNullable = true
                };

                properties.Add(property);
            }
                
            schema.Properties.Clear();
            schema.Properties.AddRange(properties);

            return schema;
        }
    }
}