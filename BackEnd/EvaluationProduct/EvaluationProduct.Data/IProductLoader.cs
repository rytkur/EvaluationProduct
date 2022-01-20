using System.Collections.Generic;
using System.Threading.Tasks;
using EvaluationProduct.Domain.Model;

namespace EvaluationProduct.Data
{
    public interface IProductLoader
    {
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetProducts(int count);
        Task<int> AddProduct(Product product);
        Task<int> UpdateProduct(Product product);
        Task<int> RemoveProduct(Product product);
    }
}
