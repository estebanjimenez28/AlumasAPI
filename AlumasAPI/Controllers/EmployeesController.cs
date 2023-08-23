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
    public class EmployeesController : ControllerBase
    {
        private readonly AlumasContext _context;

        public EmployeesController(AlumasContext context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees()
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            return await _context.Employees.ToListAsync();
        }

        // GET: api/Employees/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
          if (_context.Employees == null)
          {
              return NotFound();
          }
            var employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            return employee;
        }


        // GET: api/Employees/5
        [HttpGet("GetEmployeeListByBranch")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeeListByBranch(int id)
        {
            if (_context.Employees == null)
            {
                return NotFound();
            }
            var employeeList = await _context.Employees.Where(p=>p.BranchBranchId.Equals(id)).ToListAsync();

            if (employeeList == null)
            {
                return NotFound();
            }

            return employeeList;
        }
        [HttpGet("GetInfoByEmployeeName")]
        public ActionResult<IEnumerable<EmployeeDTO>> GetInfoByEmployeeName(string PemployeeName)
        {
            //acá creamos un linq que combina información de dos entidades 
            //(user inner join userrole) y la agreaga en el objeto dto de usuario 

            var query = (from u in _context.Employees
                         join ur in _context.Branches on
                         u.BranchBranchId equals ur.BranchId
                         where u.EmployeeName == PemployeeName && u.Active == true
                        
                         select new
                         {
                             idEmpleado = u.EmployeeId,
                             nombreempleado = u.EmployeeName,
                             telefono = u.EmployeePhoneNumber,
                             correoRespaldo = u.BackUpEmail,
                             direccion = u.EmployeeAddress,
                             activo = u.Active,
                             idsucursal = ur.BranchId,
                             nombresucursal = ur.Name
                         }).ToList();

            //creamos un objeto del tipo que retorna la función 
            List<EmployeeDTO> list = new List<EmployeeDTO>();

            foreach (var item in query)
            {
                EmployeeDTO NewItem = new EmployeeDTO()
                {
                    
                    IdEmpelado = item.idEmpleado,
                    NombreEmpleado = item.nombreempleado,
                    Correo = item.correoRespaldo,
                    NumeroTelefonico = item.telefono,
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

        // PUT: api/Employees/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEmployee(int id, EmployeeDTO employee)
        {
            if (id != employee.IdEmpelado)
            {
                return BadRequest();
            }

            Employee? NewEFEmployee = GetEmployeeByID(id);

            if (NewEFEmployee != null)

            {
                NewEFEmployee.EmployeeName = employee.NombreEmpleado;
                NewEFEmployee.BackUpEmail = employee.Correo;
                NewEFEmployee.EmployeePhoneNumber = employee.NumeroTelefonico;
                NewEFEmployee.EmployeeAddress = employee.Direccion;
                _context.Entry(employee).State = EntityState.Modified;
            }


           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(id))
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
        

        // POST: api/Employees
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Employee>> PostEmployee(Employee employee)
        {
          if (_context.Employees == null)
          {
              return Problem("Entity set 'AlumasContext.Employees'  is null.");
          }
            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEmployee", new { id = employee.EmployeeId }, employee);
        }


        private bool EmployeeExists(int id)
        {
            return (_context.Employees?.Any(e => e.EmployeeId == id)).GetValueOrDefault();
        }
        private Employee? GetEmployeeByID(int id)
        {
            var employee = _context.Employees.Find(id);

            //var user = _context.Users?.Any(e => e.UserId == id);

            return employee;
        }
    }
}
