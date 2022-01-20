using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluationProduct.Domain.Interfaces;
using EvaluationProduct.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace EvaluationProduct.Context.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(ApplicationContext context):base(context)
        {
        }

        public async Task<IEnumerable<Product>> GetProducts(int count)
        {
            return await GetAll().OrderByDescending(d => d.Quantity).Take(count).ToListAsync();
        }
    }
}
