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
    public class ClientsController : ControllerBase
    {
        private readonly AlumasContext _context;

        public ClientsController(AlumasContext context)
        {
            _context = context;
        }

        // GET: api/Clients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Client>>> GetClients()
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            return await _context.Clients.ToListAsync();
        }

        // GET: api/Clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Client>> GetClient(int id)
        {
          if (_context.Clients == null)
          {
              return NotFound();
          }
            var client = await _context.Clients.FindAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return client;
        }
        [HttpGet("GetInfoByClientName")]
        public ActionResult<IEnumerable<ClientDTO>> GetInfoByClientName(string PclientName)
        {
            //acá creamos un linq que combina información de dos entidades 
            //(user inner join userrole) y la agreaga en el objeto dto de usuario 

            var query = (from u in _context.Clients
                         join ur in _context.Branches on
                         u.BranchBranchId equals ur.BranchId
                         where u.ClientName == PclientName && u.Active == true
                         select new
                         {
                             idcliente = u.ClientId,
                             nombrecliente = u.ClientName,
                             telefono = u.PhoneNumber,
                             correoRespaldo = u.BackUpEmail,
                             direccion = u.Address,
                             activo = u.Active,
                             idsucursal = ur.BranchId,
                             nombresucursal = ur.Name
                         }).ToList();

            //creamos un objeto del tipo que retorna la función 
            List<ClientDTO> list = new List<ClientDTO>();

            foreach (var item in query)
            {
                ClientDTO NewItem = new ClientDTO()
                {
                    IdCliente = item.idcliente,
                    NombreCliente = item.nombrecliente,
                    CorreoRespaldo = item.correoRespaldo,
                    NumeroTelefono = item.telefono,
                    Direccion = item.direccion,
                    Activo = item.activo,
                    IdSucursal = item.idsucursal,
                    NombreSucursal = item.nombresucursal
                };

                list.Add(NewItem);
            }

            if (list == null) { return NotFound(); }

            return list;

        }

        // PUT: api/Clients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutClient(int id, ClientDTO client)
        {
            if (id != client.IdCliente)
            {
                return BadRequest();
            }

            Client? NewEFClient = GetClientByID(id);

            if (NewEFClient != null)
            {
                NewEFClient.ClientName = client.NombreCliente;
                NewEFClient.BackUpEmail = client.CorreoRespaldo;
                NewEFClient.PhoneNumber = client.NumeroTelefono;
                NewEFClient.Address = client.Direccion;

                _context.Entry(NewEFClient).State = EntityState.Modified;

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // POST: api/Clients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Client>> PostClient(Client client)
        {
          if (_context.Clients == null)
          {
              return Problem("Entity set 'AlumasContext.Clients'  is null.");
          }
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetClient", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            if (_context.Clients == null)
            {
                return NotFound();
            }
            var client = await _context.Clients.FindAsync(id);
            if (client == null)
            {
                return NotFound();
            }

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ClientExists(int id)
        {
            return (_context.Clients?.Any(e => e.ClientId == id)).GetValueOrDefault();
        }
        private Client? GetClientByID(int id)
        {
            var client = _context.Clients.Find(id);

            //var user = _context.Users?.Any(e => e.UserId == id);

            return client;
        }
    }
}
