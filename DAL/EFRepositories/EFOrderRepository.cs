using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EFRepositories
{
    internal class EFOrderRepository : IRepository<Order>
    {
        private readonly OrderManagmentDbContext _dbContext;

        public EFOrderRepository(OrderManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> Create(Order entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            _dbContext.Set<Order>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(string id)
        {
            var entity = await _dbContext.Set<Order>().FindAsync(id);

            _dbContext.Set<Order>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Order>> ReadAll()
        {
            var result = await Task.Run(() => _dbContext.Set<Order>().AsNoTracking());

            return result.ToList();
        }

        public async Task<Order> ReadById(string id)
        {
            var result = await _dbContext.Set<Order>().FindAsync(id);

            return result;
        }

        public async Task<Order> Update(Order entity)
        {
            _dbContext.Set<Order>().Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
