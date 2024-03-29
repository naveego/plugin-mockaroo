using System;
using System.Collections.Generic;

namespace PluginMockaroo.Helper
{
    public class Settings
    {
        public string ApiKey { get; set; }
        public List<MockSchema> MockSchemas { get; set; }

        /// <summary>
        /// Validates the settings input object
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new Exception("The Api Key property must be set");
            }

            if (MockSchemas != null)
            {
                if (MockSchemas.Count > 0)
                {
                    foreach (var mockSchema in MockSchemas)
                    {
                        if (string.IsNullOrWhiteSpace(mockSchema.Name))
                        {
                            throw new Exception($"The Name property must be set on mock schema #{MockSchemas.IndexOf(mockSchema) + 1}");
                        }

                        // if (mockSchema.Count > 5000)
                        // {
                        //     throw new Exception($"The Count property must not exceed 5000 on mock schema #{MockSchemas.IndexOf(mockSchema) + 1}");
                        // }
                        
                        if (mockSchema.Count <= 0)
                        {
                            throw new Exception($"The Count property must be greater than 0 on mock schema #{MockSchemas.IndexOf(mockSchema) + 1}");
                        }
                    }
                }
            }
        }
    }

    public class MockSchema
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public string CustomName { get; set; }
    }
}