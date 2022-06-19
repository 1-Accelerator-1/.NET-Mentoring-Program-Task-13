using DAL.Enums;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Threading.Tasks;

namespace DAL.FiltersForDelete
{
    internal class OrderFilterForDelete : IOrderFilterForDelete
    {
        private readonly string _connectionString;

        public OrderFilterForDelete(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task DeleteByMonth(int month)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var sqlCommandDelete = new SqlCommand("OrderDeleteByMonth", connection))
                {
                    sqlCommandDelete.CommandType = CommandType.StoredProcedure;

                    sqlCommandDelete.Parameters.AddWithValue("@Month", month);

                    await sqlCommandDelete.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteByProductId(string productId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var sqlCommandDelete = new SqlCommand("OrderDeleteByProductId", connection))
                {
                    sqlCommandDelete.CommandType = CommandType.StoredProcedure;

                    sqlCommandDelete.Parameters.AddWithValue("@ProductId", productId);

                    await sqlCommandDelete.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteByStatus(OrderStatus status)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var sqlCommandDelete = new SqlCommand("OrderDeleteByStatus", connection))
                {
                    sqlCommandDelete.CommandType = CommandType.StoredProcedure;

                    sqlCommandDelete.Parameters.AddWithValue("@Status", (int)status);

                    await sqlCommandDelete.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteByYear(int year)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                using (var sqlCommandDelete = new SqlCommand("OrderDeleteByYear", connection))
                {
                    sqlCommandDelete.CommandType = CommandType.StoredProcedure;

                    sqlCommandDelete.Parameters.AddWithValue("@Year", year);

                    await sqlCommandDelete.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
