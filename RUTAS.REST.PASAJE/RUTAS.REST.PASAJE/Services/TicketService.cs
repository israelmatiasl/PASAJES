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
    public class TicketService : ITicketService
    {
        private readonly TravelPasajesContext _context = null;

        public TicketService(IOptions<Settings> settings)
        {
            _context = new TravelPasajesContext(settings);
        }

        public async Task AddTicket(Ticket ticket)
        {
            await _context.Tickets.InsertOneAsync(ticket);
        }

        public async Task<DeleteResult> DeleteTicket(string id)
        {
            return await _context.Tickets.DeleteOneAsync(Builders<Ticket>.Filter.Eq("ticketid", id));
        }

        public async Task<IEnumerable<Ticket>> GetTickesForPromotion(String promotionid)
        {
            var tickets = Builders<Ticket>.Filter;
            var filter = tickets.Eq("promotionid", promotionid);

            return await _context.Tickets.Find(filter).ToListAsync();
        }

        public async Task<Ticket> GetTicket(string id)
        {
            var ticket = Builders<Ticket>.Filter.Eq("ticketid", id);
            return await _context.Tickets.Find(ticket).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Ticket>> GetTickets()
        {
            return await _context.Tickets.Find(x => true).ToListAsync();
        }

        public async Task<ReplaceOneResult> UpdateTicket(string id, Ticket ticket)
        {
            return await _context.Tickets.ReplaceOneAsync(x => x.ticketid.Equals(id), ticket, new UpdateOptions { IsUpsert = true });
        }
    }
}
