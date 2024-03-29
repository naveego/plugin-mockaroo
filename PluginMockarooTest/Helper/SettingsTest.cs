using System;
using System.Collections.Generic;
using PluginMockaroo.Helper;
using Xunit;

namespace PluginMockarooTest.Helper
{
    public class SettingsTest
    {
        [Fact]
        public void ValidateValidTest()
        {
            // setup
            var settings = new Settings
            {
                ApiKey = "APIKEY",
                MockSchemas = new List<MockSchema>
                {
                    new MockSchema
                    {
                        Name = "Name",
                        Count = 10
                    }
                }
            };

            // act
            settings.Validate();

            // assert
        }

        [Fact]
        public void ValidateNoApiKeyTest()
        {
            // setup
            var settings = new Settings
            {
                ApiKey = null,
                MockSchemas = new List<MockSchema>
                {
                    new MockSchema
                    {
                        Name = "Name",
                        Count = 10
                    }
                }
            };

            // act
            Exception e = Assert.Throws<Exception>(() => settings.Validate());

            // assert
            Assert.Contains("The Api Key property must be set", e.Message);
        }
        
        [Fact]
        public void ValidateNoMockSchemas()
        {
            // setup
            var settings = new Settings
            {
                ApiKey = "APIKEY",
                MockSchemas = null
            };

            // act
            settings.Validate();

            // assert
        }
        
        [Fact]
        public void ValidateNoMockSchemaNameTest()
        {
            // setup
            var settings = new Settings
            {
                ApiKey = "APIKEY",
                MockSchemas = new List<MockSchema>
                {
                    new MockSchema
                    {
                        Name = null,
                        Count = 10
                    }
                }
            };

            // act
            Exception e = Assert.Throws<Exception>(() => settings.Validate());

            // assert
            Assert.Contains("The Name property must be set on mock schema #1", e.Message);
        }
        
        [Fact]
        public void ValidateMockSchemaCountLowTest()
        {
            // setup
            var settings = new Settings
            {
                ApiKey = "APIKEY",
                MockSchemas = new List<MockSchema>
                {
                    new MockSchema
                    {
                        Name = "Name",
                        Count = -1
                    }
                }
            };

            // act
            Exception e = Assert.Throws<Exception>(() => settings.Validate());

            // assert
            Assert.Contains("The Count property must be greater than 0 on mock schema #1", e.Message);
        }
        
        // [Fact]
        // public void ValidateMockSchemaCountHighTest()
        // {
        //     // setup
        //     var settings = new Settings
        //     {
        //         ApiKey = "APIKEY",
        //         MockSchemas = new List<MockSchema>
        //         {
        //             new MockSchema
        //             {
        //                 Name = "Name",
        //                 Count = 10000
        //             }
        //         }
        //     };
        //
        //     // act
        //     Exception e = Assert.Throws<Exception>(() => settings.Validate());
        //
        //     // assert
        //     Assert.Contains("The Count property must not exceed 5000 on mock schema #1", e.Message);
        // }
    }
}