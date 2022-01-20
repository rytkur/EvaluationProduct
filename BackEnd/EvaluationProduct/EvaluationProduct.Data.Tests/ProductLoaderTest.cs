using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluationProduct.Domain.Interfaces;
using EvaluationProduct.Domain.Model;
using NSubstitute;
using Xunit;

namespace EvaluationProduct.Data.Tests
{
    public class ProductLoaderTest
    {
        private readonly IProductLoader _productLoader;
        private readonly IUnitOfWork _unitOfWork;

        private Product _product;
        private IEnumerable<Product> _products;

        public ProductLoaderTest()
        {
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _productLoader = Substitute.For<ProductLoader>(_unitOfWork);

            Initialize();
        }

        private void Initialize()
        {
            _product = new Product
            {
                Id = 1,
                Name = "Product 01",
                Price = 100,
                Quantity = 99
            };

            _products = new List<Product>
            {
                new Product
                {
                    Id = 1,
                    Name = "Product 01",
                    Price = 100,
                    Quantity = 99
                },
                new Product
                {
                    Id = 2,
                    Name = "Product 02",
                    Price = 200,
                    Quantity = 990
                },
            };
        }

        [Fact]
        public async Task GetById_accept_product_id_must_return_Products()
        {
            int productId = 1;
            
            _unitOfWork.Products.GetById(productId).ReturnsForAnyArgs(Task.FromResult(_product));

            var actual = await _productLoader.GetById(productId);

            Assert.NotNull(actual);
            Assert.True(actual.Id == _product.Id);
        }

        [Fact]
        public async Task GetAllProducts_must_return_all_Products()
        {
            _unitOfWork.Products.GetAllAsync().ReturnsForAnyArgs(_products);

            var actual = await _productLoader.GetAllProducts();

            Assert.NotNull(actual);
            Assert.True(actual.Count() == 2);
        }

        [Fact]
        public async Task GetProducts_input_number_more_than_0_must_return_Products()
        {
            int count = 10;
            
            _unitOfWork.Products.GetProducts(count).ReturnsForAnyArgs(Task.FromResult(_products));

            var actual = await _productLoader.GetProducts(count);

            Assert.NotNull(actual);
            Assert.True(actual.Count() == 2);
        }

        [Fact]
        public async Task GetProducts_input_number_less_than_0_must_return_EmptyProducts()
        {
            int count = -1;
            IEnumerable<Product> products = new List<Product>();

            _unitOfWork.Products.GetProducts(count).ReturnsForAnyArgs(Task.FromResult(products));

            var actual = await _productLoader.GetProducts(count);

            Assert.NotNull(actual);
            Assert.False(actual.Any());
        }

        [Fact]
        public async Task GetProducts_input_number_equal_0_must_return_EmptyProducts()
        {
            int count = 0;
            IEnumerable<Product> products = new List<Product>();

            _unitOfWork.Products.GetProducts(count).ReturnsForAnyArgs(Task.FromResult(products));

            var actual = await _productLoader.GetProducts(count);

            Assert.NotNull(actual);
            Assert.False(actual.Any());
        }

        [Fact]
        public async Task AddProduct_add_Product_return_1()
        {
            _unitOfWork.SaveAsync().ReturnsForAnyArgs(Task.FromResult(1));

            var actual = await _productLoader.AddProduct(_product);

            Assert.True(actual > 0);
        }

        [Fact]
        public async Task UpdateProduct_must_success()
        {
            _unitOfWork.Products.Update(_product);

            _unitOfWork.SaveAsync().ReturnsForAnyArgs(Task.FromResult(1));

            var actual = await _productLoader.UpdateProduct(_product);

            Assert.Equal(1, actual);
        }

        [Fact]
        public async Task RemoveProduct_must_success()
        {
            _unitOfWork.Products.Remove(_product);

            _unitOfWork.SaveAsync().ReturnsForAnyArgs(Task.FromResult(1));

            var actual = await _productLoader.RemoveProduct(_product);

            Assert.Equal(1, actual);
        }
    }
}
