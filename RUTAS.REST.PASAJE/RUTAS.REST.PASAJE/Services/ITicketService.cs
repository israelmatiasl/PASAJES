using MongoDB.Driver;
using RUTAS.REST.PASAJE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUTAS.REST.PASAJE.Services
{
    public interface ITicketService
    {
        Task<IEnumerable<Ticket>> GetTickets();
        Task<Ticket> GetTicket(String id);
        Task AddTicket(Ticket ticket);
        Task<ReplaceOneResult> UpdateTicket(String id, Ticket ticket);
        Task<DeleteResult> DeleteTicket(String id);
        Task<IEnumerable<Ticket>> GetTickesForPromotion(String promotionid);
    }
}
