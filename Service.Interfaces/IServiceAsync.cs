using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IServiceAsync<TEntity> where TEntity : class
    {
        Task<ICollection<TEntity>> ReadAsync();
        Task<TEntity> CreateAsync(TEntity entity);
        Task<TEntity> ReadAsync(Type type, int id);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(Type type, int id);
    }
}
