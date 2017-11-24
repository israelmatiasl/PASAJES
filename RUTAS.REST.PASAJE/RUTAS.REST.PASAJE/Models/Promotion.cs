using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace RUTAS.REST.PASAJE.Models
{
    public class Promotion
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String promotionid { get; set; }

        public String companyid { get; set; }
        public String promotionname { get; set; }
        public String description { get; set; }
        public String origin { get; set; }
        public String destination { get; set; }
        public DateTime departuredate { get; set; }
        public Int32 quantity { get; set; }
        public Double price { get; set; }
        public DateTime createdate { get; set; }
        public DateTime? editdate { get; set; }
        public Boolean isactive { get; set; }
        public Boolean allseats { get; set; }

        [BsonIgnore]
        public List<Object> company { get; set; }

        [BsonIgnore]
        public List<Int32> seatsnumber { get; set; }
    }
}
