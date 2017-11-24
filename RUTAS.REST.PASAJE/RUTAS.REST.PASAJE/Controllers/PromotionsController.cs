using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RUTAS.REST.PASAJE.Services;
using RUTAS.REST.PASAJE.Models;

namespace RUTAS.REST.PASAJE.Controllers
{
    [Produces("application/json")]
    [Route("api/Promotions")]
    public class PromotionsController : Controller
    {
        //private readonly ITicketService _ticketService;
        private readonly IPromotionService _promotionService;

        public PromotionsController(IPromotionService promotionService)
        {
            _promotionService = promotionService;
        }

        [HttpGet]
        public async Task<IEnumerable<Promotion>> GetPromotions(String origin, String destination, String departuredate)
        {
            if(String.IsNullOrEmpty(origin) && String.IsNullOrEmpty(destination) && String.IsNullOrEmpty(departuredate))
            {
                return await _promotionService.GetPromotions();
            }
            else
            {
                return await _promotionService.GetPromotionsFiltered(origin, destination, departuredate);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPromotion([FromRoute] String id)
        {
            var promotion = await _promotionService.GetPromotion(id);
            if (promotion == null)
            {
                return NotFound();
            }

            return Ok(promotion);
        }

        [HttpPost]
        public async Task<IActionResult> PostPromotion([FromBody]Promotion promotion)
        {
            await _promotionService.AddPromotion(promotion);

            return CreatedAtAction("GetPromotion", new { id = promotion.promotionid }, promotion);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPromotion([FromRoute] String id, [FromBody] Promotion promotion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != promotion.promotionid)
            {
                return BadRequest();
            }

            try
            {
                await _promotionService.UpdatePromotion(id, promotion);
            }
            catch
            {
                if (!PromotionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePromotion([FromRoute] String id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var promotion = await _promotionService.GetPromotion(id);
            if (promotion == null)
            {
                return NotFound();
            }

            await _promotionService.DeletePromotion(id);

            return Ok(promotion);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool PromotionExists(String id)
        {
            return _promotionService.GetPromotion(id) != null;
        }
    }
}