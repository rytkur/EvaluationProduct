using System.Collections.Generic;
using System.Threading.Tasks;
using EvaluationProduct.Domain.Model;

namespace EvaluationProduct.Domain.Interfaces
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProducts(int count);
    }
}
