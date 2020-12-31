using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MonitorSystem.Service
{
    public interface IBaseEntityService<T> where T : class
    {

        bool Add(T t);
        Task<bool> Delete(Guid id);
        bool Delete(T t);
        bool Update(T t);
        Task<List<T>> Get();
        Task<List<T>> Get(int pageNumber,int pageSize);
        Task<List<T>> Get(int pageNumber, int pageSize, Expression<Func<T, bool>> @where);
        Task<T> GetById(Guid id);
        Task<List<T>> GetWhere(Expression<Func<T, bool>> @where);
        Task<int> GetTotalCount();
    }
}
