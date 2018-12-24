using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ParserModel.Entities
{
   public class MongoDbContext
    {
        private readonly IMongoCollection<Events> _clubs;

        public MongoDbContext()
        {
            var client = new MongoClient("mongodb+srv://kay:lipsKO01298#@cluster0.mongodb.net/?ssl=true&authSource=admin");
            var database = client.GetDatabase("Clubs");
            _clubs = database.GetCollection<Events>("Events");
        }

        public List<Events> Get()
        {
            return _clubs.Find(club => true).ToList();
        }

        //public Events Get(string id)
        //{
        //    var docId = new ObjectId(id);

        //    return _clubs.Find<Events>(club => club.Id == docId).FirstOrDefault();
        //}

        public IEnumerable<Events> Create(IEnumerable<Events> club)
        {
            var enumerable = club as Events[] ?? club.ToArray();
            _clubs.InsertMany(enumerable);
            return enumerable;
        }

        //public void Update(string id, Events clubIn)
        //{
        //    var docId = new ObjectId(id);

        //    _clubs.ReplaceOne(club => club.Id == docId, clubIn);
        //}

        //public void Remove(Events clubIn)
        //{
        //    _clubs.DeleteOne(club => club.Id == clubIn.Id);
        //}

        //public void Remove(ObjectId id)
        //{
        //    _clubs.DeleteOne(club => club.Id == id);
        //}
    }
}
