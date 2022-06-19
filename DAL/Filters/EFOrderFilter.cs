using DAL.Enums;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Filters
{
    internal class EFOrderFilter : IOrderFilter
    {
        private readonly OrderManagmentDbContext _dbContext;

        public EFOrderFilter(OrderManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> ReadAllByMonth(int month)
        {
            var sql = "OrderFilterByMonth @Month";

            var sqlParameter = new SqlParameter { ParameterName = "Month", Value = month };

            var listOrders = await Task.Run(() => _dbContext.Orders.FromSqlRaw(sql, sqlParameter).ToList());
            return listOrders;
        }

        public async Task<List<Order>> ReadAllByProductId(string productId)
        {
            var sql = "OrderFilterByProductId @ProductId";

            var sqlParameter = new SqlParameter { ParameterName = "ProductId", Value = productId };

            var listOrders = await Task.Run(() => _dbContext.Orders.FromSqlRaw(sql, sqlParameter).ToList());
            return listOrders;
        }

        public async Task<List<Order>> ReadAllByStatus(OrderStatus status)
        {
            var sql = "OrderFilterByStatus @Status";

            var sqlParameter = new SqlParameter { ParameterName = "Status", Value = status };

            var listOrders = await Task.Run(() => _dbContext.Orders.FromSqlRaw(sql, sqlParameter).ToList());
            return listOrders;
        }

        public async Task<List<Order>> ReadAllByYear(int year)
        {
            var sql = "OrderFilterByYear @Year";

            var sqlParameter = new SqlParameter { ParameterName = "Year", Value = year };

            var listOrders = await Task.Run(() => _dbContext.Orders.FromSqlRaw(sql, sqlParameter).ToList());
            return listOrders;
        }
    }
}
