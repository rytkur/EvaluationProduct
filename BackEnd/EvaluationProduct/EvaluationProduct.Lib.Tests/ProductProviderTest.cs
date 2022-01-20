using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EvaluationProduct.Data;
using EvaluationProduct.Domain.Model;
using NSubstitute;
using Xunit;

namespace EvaluationProduct.Lib.Tests
{
    public class ProductProviderTest
    {
        private readonly IProductLoader _productLoader;
        private readonly IProductProvider _productProvider;

        private Product _product;
        private IEnumerable<Product> _products;

        public ProductProviderTest()
        {
            _productLoader = Substitute.For<IProductLoader>();
            _productProvider = Substitute.For<ProductProvider>(_productLoader);

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
        public async Task GetById_must_return_product()
        {
            _productLoader.GetById(1).ReturnsForAnyArgs(Task.FromResult(_product));

            var actual = await _productProvider.GetById(1);

            Assert.NotNull(actual);
            Assert.Equal(_product.Id, actual.Id);
        }

        [Fact]
        public async Task GetAllProducts_must_return_all_product()
        {
            _productLoader.GetAllProducts().ReturnsForAnyArgs(Task.FromResult(_products));

            var actual = await _productProvider.GetAllProducts();

            Assert.NotNull(actual);
            Assert.True(actual.Count() == 2);
        }

        [Fact]
        public async Task GetProducts_input_number_more_than_0_must_return_Products()
        {
            int count = 10;
            
            _productLoader.GetProducts(count).ReturnsForAnyArgs(Task.FromResult(_products));
            
            var actual = await _productProvider.GetProducts(count);

            Assert.NotNull(actual);
            Assert.True(actual.Count() == 2);
        }

        [Fact]
        public async Task GetProducts_input_number_less_than_0_must_return_EmptyProducts()
        {
            int count = -1;

            IEnumerable<Product> products = new List<Product>();

            _productLoader.GetProducts(count).ReturnsForAnyArgs(Task.FromResult(products));

            var actual = await _productProvider.GetProducts(count);

            Assert.NotNull(actual);
            Assert.False(actual.Any());
        }

        [Fact]
        public async Task GetProducts_input_number_equal_0_must_return_EmptyProducts()
        {
            int count = 0;

            IEnumerable<Product> products = new List<Product>();

            _productLoader.GetProducts(count).ReturnsForAnyArgs(Task.FromResult(products));

            var actual = await _productProvider.GetProducts(count);

            Assert.NotNull(actual);
            Assert.False(actual.Any());
        }

        [Fact]
        public async Task AddProduct_add_Product_return_1()
        {
            _productLoader.AddProduct(_product).ReturnsForAnyArgs(Task.FromResult(1));

            var actual = await _productProvider.AddProduct(_product);

            Assert.True(actual > 0);
        }

        [Fact]
        public async Task UpdateProduct_success_to_find_product_must_return_1()
        {
            _productLoader.GetById(1).ReturnsForAnyArgs(Task.FromResult(_product));
            _productLoader.UpdateProduct(_product).ReturnsForAnyArgs(Task.FromResult(1));

            var actual = await _productProvider.UpdateProduct(_product);

            Assert.True(actual == 1);
        }

        [Fact] public async Task UpdateProduct_failed_to_find_product_must_return_0()
        {
            _productLoader.GetById(2).ReturnsForAnyArgs(Task.FromResult<Product>(null));

            var actual = await _productProvider.UpdateProduct(_product);

            Assert.True(actual == 0);
        }

        [Fact]
        public async Task RemoveProductById_success_to_find_product_must_return_1()
        {
            int id = 1;

            _productLoader.GetById(id).ReturnsForAnyArgs(Task.FromResult(_product));
            _productLoader.RemoveProduct(_product).ReturnsForAnyArgs(Task.FromResult(1));

            var actual = await _productProvider.RemoveProductById(id);

            Assert.True(actual == 1);
        }

        [Fact]
        public async Task RemoveProductById_failed_to_find_product_must_return_0()
        {
            int id = 2;

            _productLoader.GetById(id).ReturnsForAnyArgs(Task.FromResult<Product>(null));

            var actual = await _productProvider.RemoveProductById(id);

            Assert.True(actual == 0);
        }
    }
}
