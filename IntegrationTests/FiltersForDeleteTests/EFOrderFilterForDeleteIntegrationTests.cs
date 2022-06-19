using DAL;
using DAL.EFRepositories;
using DAL.Enums;
using DAL.FiltersForDelete;
using DAL.Models;
using FluentAssertions;
using IntegrationTests.ConnectionHelpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests.FiltersForDeleteTests
{
    internal class EFOrderFilterForDeleteIntegrationTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            using var sqlCommand = new SqlCommand(@"EXEC [dbo].[AddTestDataToOrderTable]");

            using var sqlConnection = new SqlConnection(ConnectionHelper.GetConnnectionString());

            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.ExecuteNonQuery();
        }

        [TearDown]
        public void TearDown()
        {
            using var sqlCommand = new SqlCommand(@"EXEC [dbo].[DeleteTestDataFromOrderTable]");

            using var sqlConnection = new SqlConnection(ConnectionHelper.GetConnnectionString());

            sqlConnection.Open();
            sqlCommand.Connection = sqlConnection;
            sqlCommand.ExecuteNonQuery();
        }

        [Test]
        public async Task DeleteByYear_WhenYearExist_ShouldReturnOrdersListWithoutDeletedOrder()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<OrderManagmentDbContext>();
            builder.UseSqlServer(ConnectionString);

            var orderManagmentDbContext = new OrderManagmentDbContext(builder.Options);
            var orderFilterForDelete = new EFOrderFilterForDelete(orderManagmentDbContext);
            var orderRepository = new EFOrderRepository(orderManagmentDbContext);

            var year = 2021;

            IEnumerable<Order> ordersBeforeDelete = await orderRepository.ReadAll();

            // Act
            await orderFilterForDelete.DeleteByYear(year);

            // Assert
            IEnumerable<Order> ordersAfterDelete = await orderRepository.ReadAll();

            ordersBeforeDelete.Should().BeEquivalentTo(new List<Order>
            {
                new Order
                {
                    Status = OrderStatus.NotStarted,
                    CreatedDate = new DateTime(2022, 3, 21),
                    UpdatedDate = new DateTime(2022, 4, 21),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 3, 24),
                    UpdatedDate = new DateTime(2022, 4, 30),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 4, 11),
                    UpdatedDate = new DateTime(2022, 5, 16),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.InProgress,
                    CreatedDate = new DateTime(2022, 4, 9),
                    UpdatedDate = new DateTime(2022, 4, 20),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.Arrived,
                    CreatedDate = new DateTime(2021, 6, 21),
                    UpdatedDate = new DateTime(2022, 7, 21),
                    ProductId = "1ad2e869-5bfa-402a-ac26-672c68a89d57"
                }
            }, options => options.Excluding(order => order.Id));

            ordersAfterDelete.Should().BeEquivalentTo(new List<Order>
            {
                new Order
                {
                    Status = OrderStatus.NotStarted,
                    CreatedDate = new DateTime(2022, 3, 21),
                    UpdatedDate = new DateTime(2022, 4, 21),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 3, 24),
                    UpdatedDate = new DateTime(2022, 4, 30),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 4, 11),
                    UpdatedDate = new DateTime(2022, 5, 16),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.InProgress,
                    CreatedDate = new DateTime(2022, 4, 9),
                    UpdatedDate = new DateTime(2022, 4, 20),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
            }, options => options.Excluding(order => order.Id));
        }

        [Test]
        public async Task DeleteByStatus_WhenStatusExist_ShouldReturnOrdersListWithoutDeletedOrder()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<OrderManagmentDbContext>();
            builder.UseSqlServer(ConnectionString);

            var orderManagmentDbContext = new OrderManagmentDbContext(builder.Options);
            var orderFilterForDelete = new EFOrderFilterForDelete(orderManagmentDbContext);
            var orderRepository = new EFOrderRepository(orderManagmentDbContext);

            var status = OrderStatus.Arrived;

            IEnumerable<Order> ordersBeforeDelete = await orderRepository.ReadAll();

            // Act
            await orderFilterForDelete.DeleteByStatus(status);

            // Assert
            IEnumerable<Order> ordersAfterDelete = await orderRepository.ReadAll();

            ordersBeforeDelete.Should().BeEquivalentTo(new List<Order>
            {
                new Order
                {
                    Status = OrderStatus.NotStarted,
                    CreatedDate = new DateTime(2022, 3, 21),
                    UpdatedDate = new DateTime(2022, 4, 21),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 3, 24),
                    UpdatedDate = new DateTime(2022, 4, 30),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 4, 11),
                    UpdatedDate = new DateTime(2022, 5, 16),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.InProgress,
                    CreatedDate = new DateTime(2022, 4, 9),
                    UpdatedDate = new DateTime(2022, 4, 20),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.Arrived,
                    CreatedDate = new DateTime(2021, 6, 21),
                    UpdatedDate = new DateTime(2022, 7, 21),
                    ProductId = "1ad2e869-5bfa-402a-ac26-672c68a89d57"
                }
            }, options => options.Excluding(order => order.Id));

            ordersAfterDelete.Should().BeEquivalentTo(new List<Order>
            {
                new Order
                {
                    Status = OrderStatus.NotStarted,
                    CreatedDate = new DateTime(2022, 3, 21),
                    UpdatedDate = new DateTime(2022, 4, 21),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 3, 24),
                    UpdatedDate = new DateTime(2022, 4, 30),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 4, 11),
                    UpdatedDate = new DateTime(2022, 5, 16),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.InProgress,
                    CreatedDate = new DateTime(2022, 4, 9),
                    UpdatedDate = new DateTime(2022, 4, 20),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
            }, options => options.Excluding(order => order.Id));
        }

        [Test]
        public async Task DeleteByMonth_WhenMonthExist_ShouldReturnOrdersListWithoutDeletedOrder()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<OrderManagmentDbContext>();
            builder.UseSqlServer(ConnectionString);

            var orderManagmentDbContext = new OrderManagmentDbContext(builder.Options);
            var orderFilterForDelete = new EFOrderFilterForDelete(orderManagmentDbContext);
            var orderRepository = new EFOrderRepository(orderManagmentDbContext);

            var month = 6;

            IEnumerable<Order> ordersBeforeDelete = await orderRepository.ReadAll();

            // Act
            await orderFilterForDelete.DeleteByMonth(month);

            // Assert
            IEnumerable<Order> ordersAfterDelete = await orderRepository.ReadAll();

            ordersBeforeDelete.Should().BeEquivalentTo(new List<Order>
            {
                new Order
                {
                    Status = OrderStatus.NotStarted,
                    CreatedDate = new DateTime(2022, 3, 21),
                    UpdatedDate = new DateTime(2022, 4, 21),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 3, 24),
                    UpdatedDate = new DateTime(2022, 4, 30),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 4, 11),
                    UpdatedDate = new DateTime(2022, 5, 16),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.InProgress,
                    CreatedDate = new DateTime(2022, 4, 9),
                    UpdatedDate = new DateTime(2022, 4, 20),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.Arrived,
                    CreatedDate = new DateTime(2021, 6, 21),
                    UpdatedDate = new DateTime(2022, 7, 21),
                    ProductId = "1ad2e869-5bfa-402a-ac26-672c68a89d57"
                }
            }, options => options.Excluding(order => order.Id));

            ordersAfterDelete.Should().BeEquivalentTo(new List<Order>
            {
                new Order
                {
                    Status = OrderStatus.NotStarted,
                    CreatedDate = new DateTime(2022, 3, 21),
                    UpdatedDate = new DateTime(2022, 4, 21),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 3, 24),
                    UpdatedDate = new DateTime(2022, 4, 30),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 4, 11),
                    UpdatedDate = new DateTime(2022, 5, 16),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.InProgress,
                    CreatedDate = new DateTime(2022, 4, 9),
                    UpdatedDate = new DateTime(2022, 4, 20),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
            }, options => options.Excluding(order => order.Id));
        }

        [Test]
        public async Task DeleteByProductId_WhenProductIdExist_ShouldReturnOrdersListWithoutDeletedOrder()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<OrderManagmentDbContext>();
            builder.UseSqlServer(ConnectionString);

            var orderManagmentDbContext = new OrderManagmentDbContext(builder.Options);
            var orderFilterForDelete = new EFOrderFilterForDelete(orderManagmentDbContext);
            var orderRepository = new EFOrderRepository(orderManagmentDbContext);

            var productId = "1ad2e869-5bfa-402a-ac26-672c68a89d57";

            IEnumerable<Order> ordersBeforeDelete = await orderRepository.ReadAll();

            // Act
            await orderFilterForDelete.DeleteByProductId(productId);

            // Assert
            IEnumerable<Order> ordersAfterDelete = await orderRepository.ReadAll();

            ordersBeforeDelete.Should().BeEquivalentTo(new List<Order>
            {
                new Order
                {
                    Status = OrderStatus.NotStarted,
                    CreatedDate = new DateTime(2022, 3, 21),
                    UpdatedDate = new DateTime(2022, 4, 21),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 3, 24),
                    UpdatedDate = new DateTime(2022, 4, 30),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 4, 11),
                    UpdatedDate = new DateTime(2022, 5, 16),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.InProgress,
                    CreatedDate = new DateTime(2022, 4, 9),
                    UpdatedDate = new DateTime(2022, 4, 20),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.Arrived,
                    CreatedDate = new DateTime(2021, 6, 21),
                    UpdatedDate = new DateTime(2022, 7, 21),
                    ProductId = "1ad2e869-5bfa-402a-ac26-672c68a89d57"
                }
            }, options => options.Excluding(order => order.Id));

            ordersAfterDelete.Should().BeEquivalentTo(new List<Order>
            {
                new Order
                {
                    Status = OrderStatus.NotStarted,
                    CreatedDate = new DateTime(2022, 3, 21),
                    UpdatedDate = new DateTime(2022, 4, 21),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 3, 24),
                    UpdatedDate = new DateTime(2022, 4, 30),
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
                },
                new Order
                {
                    Status = OrderStatus.Loading,
                    CreatedDate = new DateTime(2022, 4, 11),
                    UpdatedDate = new DateTime(2022, 5, 16),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
                new Order
                {
                    Status = OrderStatus.InProgress,
                    CreatedDate = new DateTime(2022, 4, 9),
                    UpdatedDate = new DateTime(2022, 4, 20),
                    ProductId = "cf0f951f-c650-42cc-a735-5a90f349f218"
                },
            }, options => options.Excluding(order => order.Id));
        }
    }
}
