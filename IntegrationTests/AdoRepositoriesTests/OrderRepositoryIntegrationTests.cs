using DAL.AdoRepositories;
using DAL.Enums;
using DAL.Models;
using FluentAssertions;
using IntegrationTests.ConnectionHelpers;
using Microsoft.Data.SqlClient;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests.AdoRepositoriesTests
{
    internal class OrderRepositoryIntegrationTests : TestBase
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
        public async Task Create_WhenOrderIsNotNull_ShouldReturnOrdersListWithNewOrder()
        {
            // Arrange
            var orderRepository = new OrderRepository(ConnectionString);
            var orderToCreate = new Order
            {
                Status = OrderStatus.Arrived,
                CreatedDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day),
                UpdatedDate = new DateTime(DateTime.Now.Year + 2, DateTime.Now.Month, DateTime.Now.Day),
                ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15",
            };

            IEnumerable<Order> ordersBeforeCreate = await orderRepository.ReadAll();

            // Act
            Order addedOrder = await orderRepository.Create(orderToCreate);

            // Assert
            IEnumerable<Order> ordersAfterCreate = await orderRepository.ReadAll();

            await orderRepository.Delete(addedOrder.Id);

            ordersBeforeCreate.Should().BeEquivalentTo(new List<Order>
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

            ordersAfterCreate.Should().BeEquivalentTo(new List<Order>
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
                },
                new Order
                {
                    Status = OrderStatus.Arrived, 
                    CreatedDate = new DateTime(DateTime.Now.Year + 1, DateTime.Now.Month, DateTime.Now.Day), 
                    UpdatedDate = new DateTime(DateTime.Now.Year + 2, DateTime.Now.Month, DateTime.Now.Day), 
                    ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15" 
                }
            }, options => options.Excluding(order => order.Id));
        }

        [Test]
        public async Task Update_WhenOrderxist_ShouldReturnOrdersListWithUpdatedOrder()
        {
            // Arrange
            var orderRepository = new OrderRepository(ConnectionString);

            var testOrderId = "r51b5bc6-650a-4fke-a6aa-315e444c3c15";
            Order testOrder = await orderRepository.ReadById(testOrderId);

            testOrder.Status = OrderStatus.Cancelled;

            // Act
            Order updatedArea = await orderRepository.Update(testOrder);

            // Assert
            updatedArea.Should().BeEquivalentTo(new Order
            {
                Id = "r51b5bc6-650a-4fke-a6aa-315e444c3c15",
                Status = OrderStatus.Cancelled,
                CreatedDate = new DateTime(2021, 6, 21),
                UpdatedDate = new DateTime(2022, 7, 21),
                ProductId = "1ad2e869-5bfa-402a-ac26-672c68a89d57"
            });
        }

        [Test]
        public async Task Delete_WhenOrderExist_ShouldReturnOrdersListWithoutDeletedOrder()
        {
            // Arrange
            var orderRepository = new OrderRepository(ConnectionString);

            var testOrderId = "r51b5bc6-650a-4fke-a6aa-315e444c3c15";

            IEnumerable<Order> ordersBeforeDelete = await orderRepository.ReadAll();

            // Act
            await orderRepository.Delete(testOrderId);

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
                },
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
                } 
            }, options => options.Excluding(order => order.Id));
        }
    }
}
