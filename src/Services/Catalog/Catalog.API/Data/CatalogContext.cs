using Catalog.API.Configuration;
using Catalog.API.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        private readonly DatabaseSettings _dbSettings;

        public CatalogContext(IOptions<DatabaseSettings> dbSettings)
        {
            this._dbSettings = dbSettings.Value;

            var client = new MongoClient(_dbSettings.ConnectionString);
            var database = client.GetDatabase(_dbSettings.DatabaseName);

            Products = database.GetCollection<Product>(_dbSettings.CollectionName);
            CatalogContextSeed.SeedData(Products);
        }
        public IMongoCollection<Product> Products { get;  }
    }
}
