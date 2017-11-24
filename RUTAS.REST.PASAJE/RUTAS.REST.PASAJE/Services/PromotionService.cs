using Microsoft.Extensions.Options;
using RUTAS.REST.PASAJE.DbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using RUTAS.REST.PASAJE.Models;

namespace RUTAS.REST.PASAJE.Services
{
    public class PromotionService : IPromotionService
    {
        private readonly TravelPasajesContext _context = null;

        public PromotionService(IOptions<Settings> settings)
        {
            _context = new TravelPasajesContext(settings);
        }

        public async Task AddPromotion(Promotion promotion)
        {
            await _context.Promotions.InsertOneAsync(promotion);

            if(promotion.allseats)
            {
                for(Int32 i=0; i<promotion.quantity; i++)
                {
                    Ticket ticket = new Ticket();
                    ticket.promotionid = promotion.promotionid;
                    ticket.seatnumber = (i + 1);
                    ticket.seatstatus = true;
                    ticket.ticketstatus = true;

                    await _context.Tickets.InsertOneAsync(ticket);
                    //await _context.TicketsV.InsertOneAsync(ticket);
                }
            }
            else
            {
                foreach(var item in promotion.seatsnumber)
                {
                    Ticket ticket = new Ticket();
                    ticket.promotionid = promotion.promotionid;
                    ticket.seatnumber = item;
                    ticket.seatstatus = true;
                    ticket.ticketstatus = true;

                    await _context.Tickets.InsertOneAsync(ticket);
                    //await _context.TicketsV.InsertOneAsync(ticket);
                }
            }
        }

        public async Task<DeleteResult> DeletePromotion(string id)
        {
            return await _context.Promotions.DeleteOneAsync(Builders<Promotion>.Filter.Eq("promotionid", id));
        }

        public async Task<Promotion> GetPromotion(string id)
        {
            var promotion = Builders<Promotion>.Filter.Eq("promotionid", id);
            return await _context.Promotions.Find(promotion).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Promotion>> GetPromotions()
        {
            return await _context.Promotions.Find(x => true).ToListAsync();
        }

        public async Task<IEnumerable<Promotion>> GetPromotionsFiltered(String origin, String destination, String departuredate)
        {
            var promotions = Builders<Promotion>.Filter;
            var filter = promotions.Eq("isactive", true) & promotions.Eq("origin", origin) & promotions.Eq("destination", destination);
            
            if(!String.IsNullOrEmpty(departuredate))
            {
              var filterdate = promotions.Eq("departuredate", departuredate);
              var listfilter = filter & filterdate;
              return await _context.Promotions.Find(listfilter).ToListAsync();
            }

            return await _context.Promotions.Find(filter).ToListAsync();
        }

        public async Task<ReplaceOneResult> UpdatePromotion(string id, Promotion promotion)
        {
            return await _context.Promotions.ReplaceOneAsync(x => x.promotionid.Equals(id), promotion, new UpdateOptions { IsUpsert = true });
        }
    }
}
