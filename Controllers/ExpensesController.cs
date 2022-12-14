using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Matei_Claudiu_Lab6.Models;

namespace Matei_Claudiu_Lab6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ExpenseContext _context;

        public ExpensesController(ExpenseContext context)
        {
            _context = context;
        }

        // GET: api/Expenses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ExpensesDTO>>> GetExpenses()
        {
            return await _context.ExpensesDTO.ToListAsync();
        }

        // GET: api/Expenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ExpensesDTO>> GetExpenses(int id)
        {
            var expenses = await _context.ExpensesDTO.FindAsync(id);

            if (expenses == null)
            {
                return NotFound();
            }

            return expenses;
        }

        // PUT: api/Expenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutExpenses(int id, ExpensesDTO expenses)
        {
            if (id != expenses.Id)
            {
                return BadRequest();
            }

            _context.Entry(expenses).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExpensesExists(id))
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

        // POST: api/Expenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ExpensesDTO>> PostExpenses(ExpensesDTO expenses)
        {
            _context.ExpensesDTO.Add(expenses);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetExpenses), new { id = expenses.Id }, expenses);
        }

        // DELETE: api/Expenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteExpenses(int id)
        {
            var expenses = await _context.ExpensesDTO.FindAsync(id);
            if (expenses == null)
            {
                return NotFound();
            }

            _context.ExpensesDTO.Remove(expenses);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ExpensesExists(int id)
        {
            return _context.ExpensesDTO.Any(e => e.Id == id);
        }
    }
}
