using DAL.Enums;
using DAL.Interfaces;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.FiltersForDelete
{
    internal class EFOrderFilterForDelete : IOrderFilterForDelete
    {
        private readonly OrderManagmentDbContext _dbContext;

        public EFOrderFilterForDelete(OrderManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task DeleteByMonth(int month)
        {
            var sql = "EXEC OrderDeleteByMonth @Month";

            var sqlParameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "Month", Value = month },
            };

            await _dbContext.Database.ExecuteSqlRawAsync(sql, sqlParameters);
        }

        public async Task DeleteByProductId(string productId)
        {
            var sql = "EXEC OrderDeleteByProductId @ProductId";

            var sqlParameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "ProductId", Value = productId },
            };

            await _dbContext.Database.ExecuteSqlRawAsync(sql, sqlParameters);
        }

        public async Task DeleteByStatus(OrderStatus status)
        {
            var sql = "EXEC OrderDeleteByStatus @Status";

            var sqlParameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "Status", Value = status },
            };

            await _dbContext.Database.ExecuteSqlRawAsync(sql, sqlParameters);
        }

        public async Task DeleteByYear(int year)
        {
            var sql = "EXEC OrderDeleteByYear @Year";

            var sqlParameters = new List<SqlParameter>
            {
                new SqlParameter { ParameterName = "Year", Value = year },
            };

            await _dbContext.Database.ExecuteSqlRawAsync(sql, sqlParameters);
        }
    }
}
