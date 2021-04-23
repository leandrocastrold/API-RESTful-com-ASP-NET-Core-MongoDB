using System;
using Microsoft.Extensions.Configuration;
using Mk_Api.Data.Collections;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Driver;

namespace Mk_Api.Data
{
    public class MongoDB
    {
        public IMongoDatabase DB { get; }
        public MongoDB(IConfiguration configuration)
        {
            try
            {
                var settings  =  MongoClientSettings.FromUrl(new MongoUrl (configuration["ConnectionString"]));
                var client = new MongoClient(settings);
                DB = client.GetDatabase(configuration["NomeBanco"]);
                MapClasses();
            
            }
            catch (Exception ex)
            {
                  throw new MongoException("Erro ao tentar conectar com o Mongo", ex);
            }
        }

        private void MapClasses()
        {
            var conventionPack = new ConventionPack { new CamelCaseElementNameConvention()};
            ConventionRegistry.Register("camelCase", conventionPack, t => true);

            if (BsonClassMap.IsClassMapRegistered(typeof(Fighter))){
                BsonClassMap.RegisterClassMap<Fighter>(i => {
                    i.AutoMap();
                    i.SetIgnoreExtraElements(true);
                });
            }
        }
    }
}