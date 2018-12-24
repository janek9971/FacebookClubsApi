using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson.Serialization.Attributes;

namespace ParserModel.Entities
{
   public class Club
    {
        [BsonElement("ClubName")]
        private Club Name { get; set; }
    }
}
