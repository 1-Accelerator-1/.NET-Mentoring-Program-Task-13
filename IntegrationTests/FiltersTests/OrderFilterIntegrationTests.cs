using DAL.Enums;
using DAL.Filters;
using DAL.Models;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntegrationTests.FiltersTests
{
    public class OrderFilterIntegrationTests : TestBase
    {
        [Test]
        public async Task ReadAllByYear_WhenSelectedYearExist_ShouldReturnOrdersListWithSelectedYear()
        {
            // Arrange
            var orderFilter = new OrderFilter(ConnectionString);
            var year = 2022;

            // Act
            IEnumerable<Order> readedOrders = await orderFilter.ReadAllByYear(year);

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

        [Test]
        public async Task ReadAllByStatus_WhenStatusExist_ShouldReturnOrdersListWithSelectedStatus()
        {
            // Arrange
            var orderFilter = new OrderFilter(ConnectionString);
            var status = OrderStatus.Loading;

            // Act
            IEnumerable<Order> readedOrders = await orderFilter.ReadAllByStatus(status);

            // Assert
            readedOrders.Should().BeEquivalentTo(new List<Order>
            {
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
            }, options => options.Excluding(order => order.Id));
        }

        [Test]
        public async Task ReadAllByMonth_WhenMonthExist_ShouldReturnOrdersListWithSelectedMonth()
        {
            // Arrange
            var orderFilter = new OrderFilter(ConnectionString);
            var month = 4;

            // Act
            IEnumerable<Order> readedOrders = await orderFilter.ReadAllByMonth(month);

            // Assert
            readedOrders.Should().BeEquivalentTo(new List<Order>
            {
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
        public async Task ReadAllByProductId_WhenProductIdExist_ShouldReturnOrdersListWithSelectedProductId()
        {
            // Arrange
            var orderFilter = new OrderFilter(ConnectionString);
            var productId = "541f2bc6-850a-4f5e-abaa-315ec24c3c15";

            // Act
            IEnumerable<Order> readedOrders = await orderFilter.ReadAllByProductId(productId);

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
            }, options => options.Excluding(order => order.Id));
        }
    }
}
