using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace RUTAS.REST.PASAJE.Models
{
    public class Ticket
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public String ticketid { get; set; }

        public String promotionid { get; set; }
        public Int32 seatnumber { get; set; }
        public Boolean seatstatus { get; set; }
        public Boolean ticketstatus { get; set; }

    }
}
