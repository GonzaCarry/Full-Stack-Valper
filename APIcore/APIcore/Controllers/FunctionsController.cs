using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIcore.Models;

namespace APIcore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionsController : ControllerBase
    {
        private readonly APIcoreContext _context;

        public FunctionsController(APIcoreContext context)
        {
            _context = context;
        }

        // GET: api/Functions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Function>>> GetFunction()
        {
            return await _context.Function.ToListAsync();
        }

        // GET: api/Functions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Function>> GetFunction(string id)
        {
            var function = await _context.Function.FindAsync(id);

            if (function == null)
            {
                return NotFound();
            }

            return function;
        }

        // PUT: api/Functions/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFunction(string id, Function function)
        {
            if (id != function.Id)
            {
                return BadRequest();
            }

            _context.Entry(function).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FunctionExists(id))
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

        // POST: api/Functions
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Function>> PostFunction(Function function)
        {
            _context.Function.Add(function);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FunctionExists(function.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFunction", new { id = function.Id }, function);
        }

        // DELETE: api/Functions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Function>> DeleteFunction(string id)
        {
            var function = await _context.Function.FindAsync(id);
            if (function == null)
            {
                return NotFound();
            }

            _context.Function.Remove(function);
            await _context.SaveChangesAsync();

            return function;
        }

        private bool FunctionExists(string id)
        {
            return _context.Function.Any(e => e.Id == id);
        }
    }
}
