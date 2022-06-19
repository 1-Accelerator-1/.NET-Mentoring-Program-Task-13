using DAL.Interfaces;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.EFRepositories
{
    internal class EFProductRepository : IRepository<Product>
    {
        private readonly DbContext _dbContext;

        public EFProductRepository(OrderManagmentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Product> Create(Product entity)
        {
            entity.Id = Guid.NewGuid().ToString();

            _dbContext.Set<Product>().Add(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task Delete(string id)
        {
            var entity = await _dbContext.Set<Product>().FindAsync(id);

            _dbContext.Set<Product>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<List<Product>> ReadAll()
        {
            var result = await Task.Run(() => _dbContext.Set<Product>().AsNoTracking().ToList());

            return result;
        }

        public async Task<Product> ReadById(string id)
        {
            var result = await _dbContext.Set<Product>().FindAsync(id);

            return result;
        }

        public async Task<Product> Update(Product entity)
        {
            _dbContext.Set<Product>().Update(entity);
            await _dbContext.SaveChangesAsync();

            return entity;
        }
    }
}
