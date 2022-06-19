using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> Create(T entity);

        Task<T> ReadById(string id);

        Task<T> Update(T entity);

        Task Delete(string id);

        Task<List<T>> ReadAll();
    }
}
