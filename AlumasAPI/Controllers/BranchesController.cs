using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AlumasAPI.Models;
using AlumasAPI.Attributes;

namespace AlumasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[ApiKey]
    public class BranchesController : ControllerBase
    {
        private readonly AlumasContext _context;

        public BranchesController(AlumasContext context)
        {
            _context = context;
        }

        // GET: api/Branches
        [HttpGet]
       // [ApiKey]
        public async Task<ActionResult<IEnumerable<Branch>>> GetBranches()
        {
          if (_context.Branches == null)
          {
              return NotFound();
          }
            return await _context.Branches.ToListAsync();
        }

        // GET: api/Branches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Branch>> GetBranch(int id)
        {
          if (_context.Branches == null)
          {
              return NotFound();
          }
            var branch = await _context.Branches.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            return branch;
        }

        // PUT: api/Branches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBranch(int id, Branch branch)
        {
            if (id != branch.BranchId)
            {
                return BadRequest();
            }

            _context.Entry(branch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BranchExists(id))
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

        // POST: api/Branches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Branch>> PostBranch(Branch branch)
        {
          if (_context.Branches == null)
          {
              return Problem("Entity set 'AlumasContext.Branches'  is null.");
          }
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBranch", new { id = branch.BranchId }, branch);
        }

       
        private bool BranchExists(int id)
        {
            return (_context.Branches?.Any(e => e.BranchId == id)).GetValueOrDefault();
        }
    }
}
