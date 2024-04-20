using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Global
{
    public interface IRepository<T> where T: EntityBase
    {
        Task Create(T entity);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(Int64 Id);
        Task Delete(Int64 Id);
        Task Update(T entity);
    }
}
