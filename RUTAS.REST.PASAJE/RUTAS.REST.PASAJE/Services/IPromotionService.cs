using MongoDB.Driver;
using RUTAS.REST.PASAJE.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RUTAS.REST.PASAJE.Services
{
    public interface IPromotionService
    {
        Task<IEnumerable<Promotion>> GetPromotions();
        Task<IEnumerable<Promotion>> GetPromotionsFiltered(String origin, String destination, String departuredate);
        Task<Promotion> GetPromotion(String id);
        Task AddPromotion(Promotion promotion);
        Task<ReplaceOneResult> UpdatePromotion(String id, Promotion promotion);
        Task<DeleteResult> DeletePromotion(String id);
    }
}
