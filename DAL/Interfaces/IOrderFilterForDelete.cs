using DAL.Enums;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IOrderFilterForDelete
    {
        Task DeleteByMonth(int month);

        Task DeleteByYear(int year);

        Task DeleteByStatus(OrderStatus status);

        Task DeleteByProductId(string productId);
    }
}
