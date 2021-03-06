﻿using Nest;
using log4net.ElasticSearch.Models;

namespace log4net.ElasticSearch.Tests
{
    public class ElasticSearchTestSetup
    {
        private readonly ConnectionSettings elasticSettings;
        public readonly ElasticClient client;

        public ElasticSearchTestSetup()
        {
            elasticSettings = new ConnectionSettings("127.0.0.1.", 9200)
                .SetDefaultIndex("log_test");
            
            client = new ElasticClient(elasticSettings);
        }

        public void SetupTestIndex()
        {
            client.CreateIndex("log_test", c => c
                                                    .NumberOfReplicas(0)
                                                    .NumberOfShards(1)
                                                    .AddMapping<LogEvent>(m => m.MapFromAttributes()));
        }

        public void DeleteTestIndex()
        {
            client.DeleteIndex("log_test");
        }
    }
}
