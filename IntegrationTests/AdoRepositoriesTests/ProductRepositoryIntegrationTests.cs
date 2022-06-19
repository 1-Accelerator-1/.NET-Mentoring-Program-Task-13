using DAL.AdoRepositories;
using DAL.Models;
using FluentAssertions;
using IntegrationTests.ConnectionHelpers;
using Microsoft.Data.SqlClient;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests.AdoRepositoriesTests
{
    public class ProductRepositoryIntegrationTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            using var sqlCommand = new SqlCommand(@"EXEC [dbo].[AddTestDataToProductTable]");

            using var sqlConnection = new SqlConnection(ConnectionHelper.GetConnnectionString());

            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.ExecuteNonQuery();
        }

        [TearDown]
        public void TearDown()
        {
            using var sqlCommand = new SqlCommand(@"EXEC [dbo].[DeleteTestDataFromProductTable]");

            using var sqlConnection = new SqlConnection(ConnectionHelper.GetConnnectionString());

            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.ExecuteNonQuery();
        }

        [Test]
        public async Task Create_WhenProductIsNotNull_ShouldReturnProductsListWithNewProduct()
        {
            // Arrange
            var productRepository = new ProductRepository(ConnectionString);
            var productToCreate = new Product
            {
                Name = "Product New",
                Description = "Product Description New",
                Weight = 69.3m,
                Height = 84.1m,
                Width = 39.9m,
                Length = 66.4m
            };

            IEnumerable<Product> productsBeforeCreate = await productRepository.ReadAll();

            // Act
            Product addedProduct = await productRepository.Create(productToCreate);

            // Assert
            IEnumerable<Product> productsAfterCreate = await productRepository.ReadAll();

            await productRepository.Delete(addedProduct.Id);

            productsBeforeCreate.Should().BeEquivalentTo(new List<Product>
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
                },
                new Product
                {
                    Id = "hy1f2bc6-850a-4f5e-abaa-315ec24c3c15",
                    Name = "Test Product",
                    Description = "Test Product Description",
                    Weight = 100.2m,
                    Height = 54.4m,
                    Width = 45m,
                    Length = 67.3m
                }
            });

            productsAfterCreate.Should().BeEquivalentTo(new List<Product>
            {
                new Product
                {
                    Name = "Product1",
                    Description = "Product Description 1",
                    Weight = 100.2m,
                    Height = 54.4m,
                    Width = 45m,
                    Length = 67.3m
                },
                new Product
                {
                    Name = "Product2",
                    Description = "Product Description 2",
                    Weight = 91.5m,
                    Height = 44.5m,
                    Width = 70.8m,
                    Length = 58.1m
                },
                new Product
                {
                    Name = "Product3",
                    Description = "Product Description 3",
                    Weight = 69.3m,
                    Height = 84.1m,
                    Width = 39.9m,
                    Length = 66.4m
                },
                new Product
                {
                    Name = "Test Product",
                    Description = "Test Product Description",
                    Weight = 100.2m,
                    Height = 54.4m,
                    Width = 45m,
                    Length = 67.3m
                },
                new Product
                {
                    Name = "Product New",
                    Description = "Product Description New",
                    Weight = 69.3m,
                    Height = 84.1m,
                    Width = 39.9m,
                    Length = 66.4m
                }
            }, options => options.Excluding(product => product.Id));
        }

        [Test]
        public async Task Update_WhenProductxist_ShouldReturnProductsListWithUpdatedProduct()
        {
            // Arrange
            var productRepository = new ProductRepository(ConnectionString);

            var testProductId = "hy1f2bc6-850a-4f5e-abaa-315ec24c3c15";
            Product testProduct = await productRepository.ReadById(testProductId);

            testProduct.Name = "Updated Product";

            // Act
            Product updatedProduct = await productRepository.Update(testProduct);

            // Assert
            updatedProduct.Should().BeEquivalentTo(new Product
            {
                Id = "hy1f2bc6-850a-4f5e-abaa-315ec24c3c15",
                Name = "Updated Product",
                Description = "Test Product Description",
                Weight = 100.2m,
                Height = 54.4m,
                Width = 45m,
                Length = 67.3m
            });
        }

        [Test]
        public async Task Delete_WhenProductExist_ShouldReturnProductsListWithoutDeletedProduct()
        {
            // Arrange
            var productRepository = new ProductRepository(ConnectionString);

            var testProductId = "hy1f2bc6-850a-4f5e-abaa-315ec24c3c15";

            IEnumerable<Product> productsBeforeDelete = await productRepository.ReadAll();

            // Act
            await productRepository.Delete(testProductId);

            // Assert
            IEnumerable<Product> productsAfterDelete = await productRepository.ReadAll();

            productsBeforeDelete.Should().BeEquivalentTo(new List<Product>
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
                },
                new Product
                {
                    Id = "hy1f2bc6-850a-4f5e-abaa-315ec24c3c15",
                    Name = "Test Product",
                    Description = "Test Product Description",
                    Weight = 100.2m,
                    Height = 54.4m,
                    Width = 45m,
                    Length = 67.3m
                }
            });

            productsAfterDelete.Should().BeEquivalentTo(new List<Product>
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
