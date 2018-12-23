using System;
using Newtonsoft.Json.Converters;

namespace ParserModel.Models
{
   public class Club
    {
        //[JsonConverter(typeof(ESDateTimeConverter))]
        public DateTime DateStart { get; set; }
        //[JsonConverter(typeof(ESDateTimeConverter))]
        public DateTime DateEnd { get; set; }
        public string Title { get; set; }
        public string Guests { get; set; }
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
