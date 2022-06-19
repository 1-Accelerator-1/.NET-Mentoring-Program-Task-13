using DAL.Enums;
using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderFilter
    {
        Task<List<Order>> ReadAllByMonth(int month);

        Task<List<Order>> ReadAllByYear(int year);

        Task<List<Order>> ReadAllByStatus(OrderStatus status);

        Task<List<Order>> ReadAllByProductId(string productId);
    }
}
