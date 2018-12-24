using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ParserModel.Entities

{
   public class Events
    {
        //[BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public ObjectId Id { get; set; }
        public int Id { get; set; }
        [BsonElement("DateStart")]
        [JsonConverter(typeof(ESDateTimeConverter))]
        public DateTime DateStart { get; set; }
        [BsonElement("DateEnd")]
        [JsonConverter(typeof(ESDateTimeConverter))]
        public DateTime DateEnd { get; set; }
        [BsonElement("Title")]
        public string Title { get; set; }
        [BsonElement("Guests")]
        public long Guests { get; set; }
        [BsonElement("Localization")]
        public string Localization { get; set; }

    }
   public class ESDateTimeConverter : IsoDateTimeConverter
   {
       public ESDateTimeConverter()
       {
           base.DateTimeFormat = "dd.MM.yyyy HH:mm:ss";

           // base.DateTimeFormat = "yyyy-MM-ddTHH:mm:ss";
       }
   }
}

