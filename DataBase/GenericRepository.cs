using GreenStock.DataBase.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GreenStock.DataBase
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public async Task<T?> GetByIdAsync(int id)
        {
            using(var context = new DataBaseContext())
            {
                return await context.Set<T>().FindAsync(id);
            }
        }

        public async Task<ObservableCollection<T>> GetAllAsync(params Expression<Func<T, object>>[] includes)
        {
            using (var context = new DataBaseContext())
            {
                IQueryable<T> query = context.Set<T>();

                foreach (var include in includes)
                    query = query.Include(include);

                var list = await query.ToListAsync();
                return new ObservableCollection<T>(list);
            }
        }

        public async Task Add(T entity)
        {
            using (var context = new DataBaseContext())
            {
                await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

            }
        }
        public async Task Update(T entity)
        {
            using (var context = new DataBaseContext())
            {
                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

            }
        }
        public async Task Delete(T entity)
        {
            using (var context = new DataBaseContext())
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }
    }
}
