using DAL;
using DAL.AdoRepositories;
using DAL.EFRepositories;
using DAL.Enums;
using DAL.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests.EFRepositoriesTests.ReadTests
{
    public class EFOrderRepositoryIntegrationReadTests : TestBase
    {
        [Test]
        public async Task ReadById_WhenOrderExist_ShouldReturnExistingOrder()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<OrderManagmentDbContext>();
            builder.UseSqlServer(ConnectionString);

            var orderManagmentDbContext = new OrderManagmentDbContext(builder.Options);
            var orderRepository = new EFOrderRepository(orderManagmentDbContext);
            var existOrderId = "cf0fvv1f-c650-42cc-a735-5a90f349f218";

            // Act
            Order readedOrder = await orderRepository.ReadById(existOrderId);

            // Assert
            readedOrder.Should().BeEquivalentTo(new Order
            {
                Id = "cf0fvv1f-c650-42cc-a735-5a90f349f218",
                Status = OrderStatus.Loading,
                CreatedDate = new DateTime(2022, 3, 24),
                UpdatedDate = new DateTime(2022, 4, 30),
                ProductId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15"
            });
        }

        [Test]
        public async Task ReadAll_WhenOrdersExist_ShouldReturnOrdersList()
        {
            // Arrange
            var builder = new DbContextOptionsBuilder<OrderManagmentDbContext>();
            builder.UseSqlServer(ConnectionString);

            var orderManagmentDbContext = new OrderManagmentDbContext(builder.Options);
            var orderRepository = new EFOrderRepository(orderManagmentDbContext);

            // Act
            IEnumerable<Order> readedOrders = await orderRepository.ReadAll();

            // Assert
            readedOrders.Should().BeEquivalentTo(new List<Order>
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
