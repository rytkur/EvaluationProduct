using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using EvaluationProduct.Domain.Model;
using EvaluationProduct.Lib;
using EvaluationProduct.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using Xunit;

namespace EvaluationProduct.WebApi.Tests.Controllers
{
    public class ProductControllerTest
    {
        private readonly IProductProvider _productProvider;
        private readonly ProductController _productController;

        private Product _product;
        private IEnumerable<Product> _products;

        public ProductControllerTest()
        {
            _productProvider = Substitute.For<IProductProvider>();
            _productController = Substitute.For<ProductController>(_productProvider);

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
        public async Task GetAllProducts_must_return_all_product()
        {
            _productProvider.GetAllProducts().ReturnsForAnyArgs(Task.FromResult(_products));

            var response = await _productController.GetAllProducts();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetProducts_input_number_more_than_0_must_return_Products()
        {
            int count = 10;

            _productProvider.GetProducts(count).ReturnsForAnyArgs(Task.FromResult(_products));

            var response = await _productController.GetProducts(count);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetProducts_input_number_less_than_0_must_return_EmptyProducts()
        {
            int count = -1;
            IEnumerable<Product> products = new List<Product>();

            _productProvider.GetProducts(count).ReturnsForAnyArgs(Task.FromResult(products));

            var response = await _productController.GetProducts(count);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetProducts_input_number_equal_0_must_return_EmptyProducts()
        {
            int count = 0;
            IEnumerable<Product> products = new List<Product>();

            _productProvider.GetProducts(count).ReturnsForAnyArgs(Task.FromResult(products));

            var response = await _productController.GetProducts(count);

            Assert.NotNull(response);
        }

        [Fact]
        public async Task AddProduct_add_Product_return_1()
        {
            _productProvider.AddProduct(_product).ReturnsForAnyArgs(Task.FromResult(1));

            var response = await _productController.AddProduct(_product) as ObjectResult;

            var resultCode = response.StatusCode;

            Assert.Equal(Convert.ToInt32(HttpStatusCode.OK), resultCode);
        }

        [Fact]
        public async Task UpdateProduct_success_update()
        {
            _productProvider.UpdateProduct(_product).ReturnsForAnyArgs(Task.FromResult(1));

            var response = await _productController.UpdateProduct(_product);

            Assert.Null(response);
        }

        [Fact]
        public async Task UpdateProduct_failed_update()
        {
            _productProvider.UpdateProduct(_product).ReturnsForAnyArgs(Task.FromResult(0));

            var response = await _productController.UpdateProduct(_product);

            Assert.Null(response);
        }

        [Fact]
        public async Task DeleteProduct_success_update()
        {
            int id = 1;

            _productProvider.RemoveProductById(id).ReturnsForAnyArgs(Task.FromResult(1));

            var response = await _productController.DeleteProductById(id);

            Assert.Null(response);
        }

        [Fact]
        public async Task DeleteProduct_failed_update()
        {
            int id = 2;

            _productProvider.RemoveProductById(id).ReturnsForAnyArgs(Task.FromResult(0));

            var response = await _productController.DeleteProductById(id);

            Assert.Null(response);
        }

        [Fact]
        public async Task AddDemoProduct_add_Product_return_1()
        {
            _productProvider.AddProduct(Arg.Any<Product>()).ReturnsForAnyArgs(Task.FromResult(1));

            var response = await _productController.AddDemoProduct() as ObjectResult;

            var resultCode = response.StatusCode;

            Assert.Equal(Convert.ToInt32(HttpStatusCode.OK), resultCode);
        }
    }
}
