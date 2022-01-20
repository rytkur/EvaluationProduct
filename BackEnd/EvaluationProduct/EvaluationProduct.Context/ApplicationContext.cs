using EvaluationProduct.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace EvaluationProduct.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
    }
}
