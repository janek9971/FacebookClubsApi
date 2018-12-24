using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using ParserModel;
using ParserModel.Entities;

namespace API.Configurations
{
    public  class ConfigureConnections
    {
        private readonly IMongoCollection<Events> _clubs;

        private readonly IMongoDatabase database;

        public ConfigureConnections(IConfiguration config)
        {
            var client = new MongoClient(config.GetConnectionString("ClubDb"));

             database = client.GetDatabase("Clubs");
            _clubs = database.GetCollection<Events>("Events");
        }

        public List<Events> Get()
        {
            return _clubs.Find(club => true).ToList();
        }

         public void CreateCollection(string name)
        {

            database.CreateCollection(name);

        }

        //public Events Get(string id)
        //{
        //    var docId = new ObjectId(id);

        //    return _clubs.Find<Events>(events => events.Id == docId).FirstOrDefault();
        //}

        public Events Create(Events events)
        {
            _clubs.InsertOne(events);
            return events;
        }
        public IEnumerable<Events> Create(IEnumerable<Events> club,string name)
        {
            database.CreateCollection(name);
          var newCollection = database.GetCollection<Events>(name);

            var enumerable = club as Events[] ?? club.ToArray();

            newCollection.InsertMany(enumerable);
      
            return enumerable;
        }
        //public void Update(string id, Events clubIn)
        //{
        //    var docId = new ObjectId(id);

        //    _clubs.ReplaceOne(events => events.Id == docId, clubIn);
        //}

        //public void Remove(Events clubIn)
        //{
        //    _clubs.DeleteOne(events => events.Id == clubIn.Id);
        //}

        //public void Remove(ObjectId id)
        //{
        //    _clubs.DeleteOne(events => events.Id == id);
        //}
    }
}
    