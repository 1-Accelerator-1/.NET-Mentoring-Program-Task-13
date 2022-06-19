using DAL.Enums;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace DAL.Filters
{
    internal class OrderFilter : IOrderFilter
    {
        private readonly string _connectionString;

        public OrderFilter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<List<Order>> ReadAllByMonth(int month)
        {
            var readOrders = new List<Order>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var sqlCommandRead = new SqlCommand("OrderFilterByMonth", connection))
                {
                    sqlCommandRead.CommandType = CommandType.StoredProcedure;

                    sqlCommandRead.Parameters.AddWithValue("@Month", month.ToString());

                    SqlDataReader reader = await sqlCommandRead.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var readOrder = new Order();

                        SetFieldsValueOrder(reader, readOrder);

                        readOrders.Add(readOrder);
                    }
                }
            }

            return readOrders;
        }

        public async Task<List<Order>> ReadAllByProductId(string productId)
        {
            var readOrders = new List<Order>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var sqlCommandRead = new SqlCommand("OrderFilterByProductId", connection))
                {
                    sqlCommandRead.CommandType = CommandType.StoredProcedure;

                    sqlCommandRead.Parameters.AddWithValue("@ProductId", productId);

                    SqlDataReader reader = await sqlCommandRead.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var readOrder = new Order();

                        SetFieldsValueOrder(reader, readOrder);

                        readOrders.Add(readOrder);
                    }
                }
            }

            return readOrders;
        }

        public async Task<List<Order>> ReadAllByStatus(OrderStatus status)
        {
            var readOrders = new List<Order>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var sqlCommandRead = new SqlCommand("OrderFilterByStatus", connection))
                {
                    sqlCommandRead.CommandType = CommandType.StoredProcedure;

                    sqlCommandRead.Parameters.AddWithValue("@Status", (int)status);

                    SqlDataReader reader = await sqlCommandRead.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var readOrder = new Order();

                        SetFieldsValueOrder(reader, readOrder);

                        readOrders.Add(readOrder);
                    }
                }
            }

            return readOrders;
        }

        public async Task<List<Order>> ReadAllByYear(int year)
        {
            var readOrders = new List<Order>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var sqlCommandRead = new SqlCommand("OrderFilterByYear", connection))
                {
                    sqlCommandRead.CommandType = CommandType.StoredProcedure;

                    sqlCommandRead.Parameters.AddWithValue("@Year", year.ToString());

                    SqlDataReader reader = await sqlCommandRead.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var readOrder = new Order();

                        SetFieldsValueOrder(reader, readOrder);

                        readOrders.Add(readOrder);
                    }
                }
            }

            return readOrders;
        }

        private void SetFieldsValueOrder(SqlDataReader reader, Order order)
        {
            order.Id = reader.GetString(0);
            order.Status = (OrderStatus)reader.GetInt32(1);
            order.CreatedDate = reader.GetDateTime(2);
            order.UpdatedDate = reader.GetDateTime(3);
            order.ProductId = reader.GetString(4);
        }
    }
}
