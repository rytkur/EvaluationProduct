using System.Collections.Generic;
using System.Threading.Tasks;
using EvaluationProduct.Data;
using EvaluationProduct.Domain.Model;

namespace EvaluationProduct.Lib
{
    public class ProductProvider : IProductProvider
    {
        private readonly IProductLoader _productLoader;

        public ProductProvider(IProductLoader productLoader)
        {
            _productLoader = productLoader;
        }

        public async Task<Product> GetById(int id)
        {
            return await _productLoader.GetById(id);
        }

        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _productLoader.GetAllProducts();
        }

        public async Task<IEnumerable<Product>> GetProducts(int count)
        {
            return await _productLoader.GetProducts(count);
        }

        public async Task<int> AddProduct(Product product)
        {
            return await _productLoader.AddProduct(product);
        }

        public async Task<int> UpdateProduct(Product product)
        {
            var productEntity = await GetById(product.Id);

            if (productEntity == null)
            {
                return 0;
            }

            productEntity.Name = product.Name;
            productEntity.Price = product.Price;
            productEntity.Quantity = product.Quantity;

            return await _productLoader.UpdateProduct(productEntity);
        }

        public async Task<int> RemoveProductById(int id)
        {
            var productEntity = await GetById(id);

            if (productEntity == null)
            {
                return 0;
            }
            
            return await _productLoader.RemoveProduct(productEntity);
        }
    }
}
