using EShop.CommonTestComponents;
using EShop.DataAccess.Models;
using EShop.Infrastructure.Repositories;
using EShop.MainApplication.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EShop.MainApplication.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public async void CanShowListOfProducts()
        {
            var products = new Product[] {
                new Product {Id =3, Title="Football", Description="Nothing", Price=20},
                new Product {Id=2, Title="Jacket", Description="Jacket Jeans", Price=30}
            };
            var mockRepo = GetMockProductRepo(products);
            var controller = new HomeController(mockRepo.Object);

            var actual = (IEnumerable<Product>?)(await controller.Index() as ViewResult)?.Model;

            Assert.Equal(products, actual, new EnumerableComparer<Product>((a, b) => (a?.Id == b?.Id) && (a?.Title == b?.Title) && (a?.Description == b?.Description) && (a?.Price == b?.Price)));
        }

        public Mock<IProductsRepository> GetMockProductRepo(IEnumerable<Product> products)
        {
            var mock = new Mock<IProductsRepository>();
            mock.Setup((repo) => (repo.GetAllRowsAsync())).ReturnsAsync(products);
            return mock;
        }
    }
}
