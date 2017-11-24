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
    [Route("api/Tickets")]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public async Task<IEnumerable<Ticket>> GetTickets(String promotionid)
        {
            //return await _ticketService.GetTickets();
            return await _ticketService.GetTickesForPromotion(promotionid);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] String id)
        {
            var ticket = await _ticketService.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody]Ticket ticket)
        {
            await _ticketService.AddTicket(ticket);
            return CreatedAtAction("GetTicket", new { id = ticket.ticketid }, ticket);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket([FromRoute] String id, [FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.ticketid)
            {
                return BadRequest();
            }

            try
            {
                await _ticketService.UpdateTicket(id, ticket);
            }
            catch
            {
                if (!TicketExists(id))
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
        public async Task<IActionResult> DeleteTicket([FromRoute] String id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _ticketService.GetTicket(id);
            if (ticket == null)
            {
                return NotFound();
            }

            await _ticketService.DeleteTicket(id);

            return Ok(ticket);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        private bool TicketExists(String id)
        {
            return _ticketService.GetTicket(id) != null;
        }
    }
}