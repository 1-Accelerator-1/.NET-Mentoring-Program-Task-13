using DAL.Interfaces;
using DAL.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AdoRepositories
{
    internal class ProductRepository : IRepository<Product>
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Product> Create(Product entity)
        {
            CheckNull(entity);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var textCommand = $"INSERT INTO [{nameof(Product)}] ([{nameof(Product.Id)}], " +
                    $"[{nameof(Product.Name)}], " +
                    $"[{nameof(Product.Description)}], " +
                    $"[{nameof(Product.Weight)}], " +
                    $"[{nameof(Product.Height)}], " +
                    $"[{nameof(Product.Width)}], " +
                    $"[{nameof(Product.Length)}]) " +
                    $"VALUES (@{nameof(Product.Id)}, " +
                    $"@{nameof(Product.Name)}, " +
                    $"@{nameof(Product.Description)}, " +
                    $"@{nameof(Product.Weight)}, " +
                    $"@{nameof(Product.Height)}, " +
                    $"@{nameof(Product.Width)}, " +
                    $"@{nameof(Product.Length)});";

                using (var sqlCommandInsert = new SqlCommand(textCommand, connection))
                {
                    var newGuid = Guid.NewGuid();
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Product.Id)}", newGuid.ToString());
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Product.Name)}", entity.Name);
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Product.Description)}", entity.Description);
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Product.Weight)}", entity.Weight);
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Product.Height)}", entity.Height);
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Product.Width)}", entity.Width);
                    sqlCommandInsert.Parameters.AddWithValue($"@{nameof(Product.Length)}", entity.Length);

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

                var textCommand = $"DELETE FROM [{nameof(Product)}] WHERE [{nameof(Product.Id)}] = @{nameof(Product.Id)};";

                using (var sqlCommandDelete = new SqlCommand(textCommand, connection))
                {
                    sqlCommandDelete.Parameters.AddWithValue($"@{nameof(Product.Id)}", id);

                    await sqlCommandDelete.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<List<Product>> ReadAll()
        {
            var productsList = new List<Product>();

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var textCommand = $"SELECT [{nameof(Product.Id)}], " +
                    $"[{nameof(Product.Name)}], " +
                    $"[{nameof(Product.Description)}], " +
                    $"[{nameof(Product.Weight)}], " +
                    $"[{nameof(Product.Height)}], " +
                    $"[{nameof(Product.Width)}], " +
                    $"[{nameof(Product.Length)}] " + 
                    $"FROM [{nameof(Product)}];";

                using (var sqlCommandRead = new SqlCommand(textCommand, connection))
                {
                    SqlDataReader reader = await sqlCommandRead.ExecuteReaderAsync();

                    while (reader.Read())
                    {
                        var product = new Product
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Weight = reader.GetDecimal(3),
                            Height = reader.GetDecimal(4),
                            Width = reader.GetDecimal(5),
                            Length = reader.GetDecimal(6),
                        };

                        productsList.Add(product);
                    }
                }
            }

            return productsList;
        }

        public async Task<Product> ReadById(string id)
        {
            Product newProduct = null;

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var textCommand = $"SELECT [{nameof(Product.Id)}], " +
                    $"[{nameof(Product.Name)}], " +
                    $"[{nameof(Product.Description)}], " +
                    $"[{nameof(Product.Weight)}], " +
                    $"[{nameof(Product.Height)}], " +
                    $"[{nameof(Product.Width)}], " +
                    $"[{nameof(Product.Length)}] " +
                    $"FROM [{nameof(Product)}] " +
                    $"WHERE [{nameof(Product.Id)}] = @{nameof(Product.Id)};";

                using (var sqlCommandRead = new SqlCommand(textCommand, connection))
                {
                    sqlCommandRead.Parameters.AddWithValue($"@{nameof(Product.Id)}", id);

                    SqlDataReader reader = await sqlCommandRead.ExecuteReaderAsync();

                    if (reader.HasRows)
                    {
                        reader.Read();

                        newProduct = new Product
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            Description = reader.GetString(2),
                            Weight = reader.GetDecimal(3),
                            Height = reader.GetDecimal(4),
                            Width = reader.GetDecimal(5),
                            Length = reader.GetDecimal(6),
                        };
                    }
                }
            }

            return newProduct;
        }

        public async Task<Product> Update(Product entity)
        {
            CheckNull(entity);

            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var textCommand = $"UPDATE [{nameof(Product)}] SET [{nameof(Product.Name)}] = @{nameof(Product.Name)}, " +
                    $"[{nameof(Product.Description)}] = @{nameof(Product.Description)}, " +
                    $"[{nameof(Product.Weight)}] = @{nameof(Product.Weight)}, " +
                    $"[{nameof(Product.Height)}] = @{nameof(Product.Height)}, " +
                    $"[{nameof(Product.Width)}] = @{nameof(Product.Width)}, " +
                    $"[{nameof(Product.Length)}] = @{nameof(Product.Length)} " +
                    $"WHERE [{nameof(Product.Id)}] = @{nameof(Product.Id)};";

                using (var sqlCommandUpdate = new SqlCommand(textCommand, connection))
                {
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Product.Id)}", entity.ToString());
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Product.Name)}", entity.Name);
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Product.Description)}", entity.Description);
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Product.Weight)}", entity.Weight);
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Product.Height)}", entity.Height);
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Product.Width)}", entity.Width);
                    sqlCommandUpdate.Parameters.AddWithValue($"@{nameof(Product.Length)}", entity.Length);

                    await sqlCommandUpdate.ExecuteNonQueryAsync();
                }
            }

            return entity;
        }

        private void CheckNull(Product entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity), "The parameter can't be null.");
            }
        }
    }
}
