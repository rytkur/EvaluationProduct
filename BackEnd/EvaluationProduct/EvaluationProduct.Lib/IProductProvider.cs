using System.Collections.Generic;
using System.Threading.Tasks;
using EvaluationProduct.Domain.Model;

namespace EvaluationProduct.Lib
{
    public interface IProductProvider
    {
        Task<Product> GetById(int id);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetProducts(int count);
        Task<int> AddProduct(Product product);
        Task<int> UpdateProduct(Product product);
        Task<int> RemoveProductById(int id);
    }
}
