using System.Collections.Generic;
using System.Threading.Tasks;
using EvaluationProduct.Domain.Interfaces;
using EvaluationProduct.Domain.Model;

namespace EvaluationProduct.Data
{
    public class ProductLoader : IProductLoader
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductLoader(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public async Task<Product> GetById(int id)
        {
            return await _unitOfWork.Products.GetById(id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _unitOfWork.Products.GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProducts(int count)
        {
            return await _unitOfWork.Products.GetProducts(count);
        }

        public async Task<int> AddProduct(Product product)
        {
            await _unitOfWork.Products.AddAsync(product);

            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> UpdateProduct(Product product)
        {
            _unitOfWork.Products.Update(product);

            return await _unitOfWork.SaveAsync();
        }

        public async Task<int> RemoveProduct(Product product)
        {
            _unitOfWork.Products.Remove(product);

            return await _unitOfWork.SaveAsync();
        }
    }
}
