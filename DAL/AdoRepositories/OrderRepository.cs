using DAL.Enums;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.AdoRepositories
{
    internal class OrderRepository : IRepository<Order>
    {
        private readonly string _connectionString;

        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Order> Create(Order entity)
        {
            CheckNull(entity);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var textCommand = $"INSERT INTO [{nameof(Order)}] ([{nameof(Order.Id)}], [{nameof(Order.Status)}], [{nameof(Order.CreatedDate)}], [{nameof(Order.UpdatedDate)}], [{nameof(Order.ProductId)}]) " +
                    $"VALUES (@{nameof(Order.Id)}, @{nameof(Order.Status)}, @{nameof(Order.CreatedDate)}, @{nameof(Order.UpdatedDate)}, @{nameof(Order.ProductId)});";

                using (var sqlCommandInsert = new SqlCommand(textCommand, connection))
                {
                    var newGuid = Guid.NewGuid();
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Order.Id)}", newGuid.ToString());
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Order.Status)}", (int)entity.Status);
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Order.CreatedDate)}", entity.CreatedDate);
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Order.UpdatedDate)}", entity.UpdatedDate);
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Order.ProductId)}", entity.ProductId);

                    await sqlCommandInsert.ExecuteNonQueryAsync();

                    entity.Id = newGuid.ToString();

                    return entity;
                }
            }
        }

        public async Task Delete(string id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var textCommand = $"DELETE FROM [{nameof(Order)}] WHERE [{nameof(Order.Id)}] = @{nameof(Order.Id)};";

                using (var sqlCommandDelete = new SqlCommand(textCommand, connection))
                {
                    sqlCommandDelete.Parameters.AddWithValue($"@{nameof(Order.Id)}", id);

                    await sqlCommandDelete.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Order>> ReadAll()
        {
            var ordersList = new List<Order>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var textCommand = $"SELECT [{nameof(Order.Id)}], [{nameof(Order.Status)}], [{nameof(Order.CreatedDate)}], [{nameof(Order.UpdatedDate)}], [{nameof(Order.ProductId)}] FROM [{nameof(Order)}];";

                using (var sqlCommandRead = new SqlCommand(textCommand, connection))
                {
                    SqlDataReader reader = await sqlCommandRead.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var order = new Order
                        {
                            Id = reader.GetString(0),
                            Status = (OrderStatus)reader.GetInt32(1),
                            CreatedDate = reader.GetDateTime(2),
                            UpdatedDate = reader.GetDateTime(3),
                            ProductId = reader.GetString(4),
                        };

                        ordersList.Add(order);
                    }
                }
            }

            return ordersList;
        }

        public async Task<Order> ReadById(string id)
        {
            Order newOrder = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var textCommand = $"SELECT [{nameof(Order.Id)}], [{nameof(Order.Status)}], [{nameof(Order.CreatedDate)}], [{nameof(Order.UpdatedDate)}], [{nameof(Order.ProductId)}] " +
                    $"FROM [{nameof(Order)}] WHERE [{nameof(Order.Id)}] = @{nameof(Order.Id)};";

                using (var sqlCommandRead = new SqlCommand(textCommand, connection))
                {
                    sqlCommandRead.Parameters.AddWithValue($"@{nameof(Order.Id)}", id.ToString());

                    SqlDataReader reader = await sqlCommandRead.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        newOrder = new Order
                        {
                            Id = reader.GetString(0),
                            Status = (OrderStatus)reader.GetInt32(1),
                            CreatedDate = reader.GetDateTime(2),
                            UpdatedDate = reader.GetDateTime(3),
                            ProductId = reader.GetString(4),
                        };
                    }
                }
            }

            return newOrder;
        }

        public async Task<Order> Update(Order entity)
        {
            CheckNull(entity);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var textCommand = $"UPDATE [{nameof(Order)}] SET [{nameof(Order.Status)}] = @{nameof(Order.Status)}, " +
                    $"[{nameof(Order.CreatedDate)}] = @{nameof(Order.CreatedDate)}, " +
                    $"[{nameof(Order.UpdatedDate)}] = @{nameof(Order.UpdatedDate)}, " +
                    $"[{nameof(Order.ProductId)}] = @{nameof(Order.ProductId)} " +
                    $"WHERE [{nameof(Order.Id)}] = @{nameof(Order.Id)};";

                using (var sqlCommandUpdate = new SqlCommand(textCommand, connection))
                {
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Order.Id)}", entity.Id);
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Order.Status)}", (int)entity.Status);
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Order.CreatedDate)}", entity.CreatedDate);
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Order.UpdatedDate)}", entity.UpdatedDate);
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Order.ProductId)}", entity.ProductId);

                    await sqlCommandUpdate.ExecuteNonQueryAsync();
                }
            }

            return entity;
        }

        private void CheckNull(Order entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "The parameter can't be null.");
            }
        }
    }
}
