using DAL.AdoRepositories;
using DAL.Models;
using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests.AdoRepositoriesTests.ReadTests
{
    public class ProductRepositoryIntegrationReadTests : TestBase
    {
        [Test]
        public async Task ReadById_WheniProductExist_ShouldReturnExistingProduct()
        {
            // Arrange
            var productRepository = new ProductRepository(ConnectionString);
            var existProductId = "1ad2e869-5bfa-402a-ac26-672c68a89d57";

            // Act
            Product readedProduct = await productRepository.ReadById(existProductId);

            // Assert
            readedProduct.Should().BeEquivalentTo(new Product
            {
                Id = "1ad2e869-5bfa-402a-ac26-672c68a89d57",
                Name = "Product3",
                Description = "Product Description 3",
                Weight = 69.3m,
                Height = 84.1m,
                Width = 39.9m,
                Length = 66.4m
            });
        }

        [Test]
        public async Task ReadAll_WhenProductsExist_ShouldReturnProductsList()
        {
            // Arrange
            var productRepository = new ProductRepository(ConnectionString);

            // Act
            IEnumerable<Product> readedProducts = await productRepository.ReadAll();

            // Assert
            readedProducts.Should().BeEquivalentTo(new List<Product>
            {
                new Product
                {
                    Id = "541f2bc6-850a-4f5e-abaa-315ec24c3c15",
                    Name = "Product1",
                    Description = "Product Description 1",
                    Weight = 100.2m,
                    Height = 54.4m,
                    Width = 45m,
                    Length = 67.3m
                },
                new Product
                {
                    Id = "cf0f951f-c650-42cc-a735-5a90f349f218",
                    Name = "Product2",
                    Description = "Product Description 2",
                    Weight = 91.5m,
                    Height = 44.5m,
                    Width = 70.8m,
                    Length = 58.1m
                },
                new Product
                {
                    Id = "1ad2e869-5bfa-402a-ac26-672c68a89d57",
                    Name = "Product3",
                    Description = "Product Description 3",
                    Weight = 69.3m,
                    Height = 84.1m,
                    Width = 39.9m,
                    Length = 66.4m
                }
            });
        }
    }
}
