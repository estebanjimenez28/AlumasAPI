using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlumasAPI.Models;
using AlumasAPI.ModelsDTOs;

namespace AlumasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class DeliveriesController : ControllerBase
    {
        private readonly AlumasContext _context;

        public DeliveriesController(AlumasContext context)
        {
            _context = context;
        }

        // GET: api/Deliveries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveries()
        {
          if (_context.Deliveries == null)
          {
              return NotFound();
          }
            return await _context.Deliveries.ToListAsync();
        }

        // GET: api/Deliveries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Delivery>> GetDelivery(int id)
        {
          if (_context.Deliveries == null)
          {
              return NotFound();
          }
            var delivery = await _context.Deliveries.FindAsync(id);

            if (delivery == null)
            {
                return NotFound();
            }

            return delivery;
        }
        [HttpGet("GetDeliveryListByClient")]
        public async Task<ActionResult<IEnumerable<Delivery>>> GetDeliveryListByClient(int id)
        {
            if (_context.Deliveries == null)
            {
                return NotFound();
            }
            var deliveryList = await _context.Deliveries.Where(p => p.ClientsClientId.Equals(id)).ToListAsync();

            if (deliveryList == null)
            {
                return NotFound();
            }

            return deliveryList;
        }
        [HttpGet("GetInfoByDescription")]
        public ActionResult<IEnumerable<DeliveryDTO>> GeGetInfoByDescriptiontUserInfoByEmail(string Pdescripcion)
        {
            //acá creamos un linq que combina información de dos entidades 
            //(user inner join userrole) y la agreaga en el objeto dto de usuario 

            var query = (from u in _context.Deliveries
                         join ur in _context.Clients on
                         u.ClientsClientId equals ur.ClientId
                         where u.Description == Pdescripcion && u.Active == true 
                         select new
                         {
                             identrega = u.DeliveryId,
                             direccion = u.Address,
                             descripcion = u.Description,
                             idcliente = ur.ClientId,
                             activo = u.Active,
                             nombrecliente = ur.ClientName
                         }).ToList();

            //creamos un objeto del tipo que retorna la función 
            List<DeliveryDTO> list = new List<DeliveryDTO>();

            foreach (var item in query)
            {
                DeliveryDTO NewItem = new DeliveryDTO()
                {
                    IdEntrega = item.identrega,
                    Direccion = item.direccion,
                    Activo = item.activo,
                    IdCliente = item.idcliente,
                    NombreCliente = item.nombrecliente
                };

                list.Add(NewItem);
            }

            if (list == null) { return NotFound(); }

            return list;

        }



        // PUT: api/Deliveries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDelivery(int id, DeliveryDTO delivery)
        {
            if (id != delivery.IdEntrega)
            {
                return BadRequest();
            }

            Delivery? NewEFDelivery = GetDeliveryByID(id);

            if (NewEFDelivery != null)
            {
                NewEFDelivery.Address = delivery.Direccion;
                NewEFDelivery.Description = delivery.Descripcion;

                _context.Entry(NewEFDelivery).State = EntityState.Modified;

            }
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeliveryExists(id))
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

        // POST: api/Deliveries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Delivery>> PostDelivery(Delivery delivery)
        {
          if (_context.Deliveries == null)
          {
              return Problem("Entity set 'AlumasContext.Deliveries'  is null.");
          }
            _context.Deliveries.Add(delivery);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDelivery", new { id = delivery.DeliveryId }, delivery);
        }

        

        private bool DeliveryExists(int id)
        {
            return (_context.Deliveries?.Any(e => e.DeliveryId == id)).GetValueOrDefault();
        }
        private Delivery? GetDeliveryByID(int id)
        {
            var delivery = _context.Deliveries.Find(id);

            //var user = _context.Users?.Any(e => e.UserId == id);

            return delivery;
        }
    }
}
