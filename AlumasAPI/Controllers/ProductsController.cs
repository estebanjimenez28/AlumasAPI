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
    public class ProductsController : ControllerBase
    {
        private readonly AlumasContext _context;

        public ProductsController(AlumasContext context)
        {
            _context = context;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            return await _context.Products.ToListAsync();
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
          if (_context.Products == null)
          {
              return NotFound();
          }
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }
       

        [HttpGet("GetInfoByProductName")]
        public ActionResult<IEnumerable<ProductDTO>> GetInfoByProductName(string PproductName)
        {
            //acá creamos un linq que combina información de dos entidades 
            //(user inner join userrole) y la agreaga en el objeto dto de usuario 

            var query = (from u in _context.Products
                         join ur in _context.ProductCategories on
                         u.ProductCategoryProductCategoryId equals ur.ProductCategoryId
                         where u.ProductName == PproductName && u.Active == true 
                      
                         select new
                         {
                             idproducto = u.ProductId,
                             nombreproducto = u.ProductName,
                             cantidad = u.Quantity,
                             precio = u.Price,
                             activo = u.Active,
                             idsucursal = u.BranchBranchId,
                             idcliente = u.ClientsClientId,
                             idcategoria = ur.ProductCategoryId,
                             nombrecategoria = ur.ProductCategoryName
                         }).ToList();

            //creamos un objeto del tipo que retorna la función 
            List<ProductDTO> list = new List<ProductDTO>();

            foreach (var item in query)
            {
                ProductDTO NewItem = new ProductDTO()
                {
                    IdProducto = item.idproducto,
                    NombreProducto = item.nombreproducto,
                    Cantidad = item.cantidad,
                    Precio = item.precio,
                    Activo = item.activo,
                    IdSucursal = item.idsucursal,
                    IdCliente = item.idcliente,
                    IdProductoCategoria = item.idcategoria,
                    NombreCategoria = item.nombrecategoria
                };

                list.Add(NewItem);
            }

            if (list == null) { return NotFound(); }

            return list;

        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO product)
        {
            if (id != product.IdProducto)
            {
                return BadRequest();
            }

            Product? NewEFProduct = GetProductByID(id);

            if (NewEFProduct != null)
            {
                NewEFProduct.ProductName = product.NombreProducto;
                NewEFProduct.Quantity = product.Cantidad;
                NewEFProduct.Price = product.Precio;
                NewEFProduct.BranchBranchId = product.IdSucursal;
                NewEFProduct.ClientsClientId = product.IdCliente;

                _context.Entry(NewEFProduct).State = EntityState.Modified;

            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
          if (_context.Products == null)
          {
              return Problem("Entity set 'AlumasContext.Products'  is null.");
          }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.ProductId }, product);
        }



        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.ProductId == id)).GetValueOrDefault();
        }
        private Product? GetProductByID(int id)
        {
            var product = _context.Products.Find(id);

            //var user = _context.Users?.Any(e => e.UserId == id);

            return product;
        }
    }
}
