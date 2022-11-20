using Microsoft.EntityFrameworkCore;
namespace Matei_Claudiu_Lab6.Models
{
    public class ExpenseContext : DbContext
    {
        public ExpenseContext(DbContextOptions<ExpenseContext> options) :base(options)
        {

        }

        public DbSet<Expenses> Expenses { get; set; }
    }
}
