using Microsoft.EntityFrameworkCore;
using PersonnelManagement.Data.Repository.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace PersonnelManagement.Data.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly PersonnelDBContext _db;
        internal DbSet<T> dbSet;
        public Repository(PersonnelDBContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
        }

        public async Task CreateAsync(T entity)
        {
            await dbSet.AddAsync(entity);
            await SaveAsync();
        }

        /// <summary>
        /// Gets a single record of object T
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="tracked"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public async Task<T> GetAsync(Expression<Func<T, bool>> filter = null, bool tracked = true, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;
            if (!tracked)
            {
                query = query.AsNoTracking();
            }
            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> FindAsync(object Id)
        {
            return await dbSet.FindAsync(Id);
        }
        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null,
            int pageSize = 0, int pageNumber = 1)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (pageSize > 0)
            {
                if (pageSize > 100)
                {
                    pageSize = 100;
                }
                //skip0.take(5)
                //page number- 2     || page size -5
                //skip(5*(1)) take(5)
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }
            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.ToListAsync();
        }
        public async Task<T> UpdateAsync(T entity)
        {
            _db.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Hard delete
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task HardDeleteAsync(T entity)
        {
            dbSet.Remove(entity);
            await SaveAsync();
        }

        public async Task DeleteAsync(object Id)
        {
            var entry = await dbSet.FindAsync(Id);

            //entry.DeleteDate = DateTime.Now;
            //entry.IsDeleted = true;
            _db.Update(entry);
            await _db.SaveChangesAsync();

        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }


    }
}
