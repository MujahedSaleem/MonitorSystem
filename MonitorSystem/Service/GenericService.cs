using Microsoft.EntityFrameworkCore;
using MonitorSystem.Data;
using MonitorSystem.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace MonitorSystem.Service
{
    public class GenericService<T> : IGenericService<T> where T : EntityBase
    {
        public readonly ApplicationDbContext ApplicationDbContext;

        public GenericService(ApplicationDbContext applicationDbContext)
        {
            ApplicationDbContext = applicationDbContext;
        }

        public bool Add(T t)
        {
            try
            {
                t.AddDate = DateTime.Now;
                t.ModifyDate = DateTime.Now;
                var x = ApplicationDbContext.Add<T>(t);
                ApplicationDbContext.SaveChanges();
                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var contractor = await GetById(id);
                if (contractor == null)
                    return false;
                ApplicationDbContext.Remove<T>(contractor);
                await ApplicationDbContext.SaveChangesAsync();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(T t)
        {
            try
            {
                ApplicationDbContext.Remove(t);
                ApplicationDbContext.SaveChanges();

                return true;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Update(T t)
        {
            try
            {
                t.ModifyDate = DateTime.Now;

                ApplicationDbContext.Update(t);
                ApplicationDbContext.SaveChanges();

                return true;

            }
            catch (Exception e)
            {


                return false;
            }
        }

        public Task<List<T>> Get()
        {
            return ApplicationDbContext.Set<T>().ToListAsync();
        }

        public  Task<int> GetTotalCount()
        {
            return ApplicationDbContext.Set<T>().CountAsync();

        }
        public Task<List<T>> Get(int pageNumber, int pageSize)
        {
            return ApplicationDbContext.Set<T>().OrderByDescending(a => a.AddDate).Skip((pageNumber - 1)  * pageSize).Take(pageSize)
                .ToListAsync();
        }
        public Task<List<T>> Get(int pageNumber, int pageSize, Expression<Func<T, bool>> @where)
        {
            return ApplicationDbContext.Set<T>().Where(where).OrderByDescending(a => a.AddDate).Skip((pageNumber - 1) * pageSize).Take(pageSize)
                .ToListAsync();
        }
        public Task<List<T>> GetWhere(Expression<Func<T, bool>> @where)
        {
            return ApplicationDbContext.Set<T>().Where(where).ToListAsync();
        }
        public Task<T> GetById(Guid id)
        {
            return ApplicationDbContext.Set<T>().FirstOrDefaultAsync(a => a.ID == id);
        }
    }
}
