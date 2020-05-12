using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IService<TEntity> where TEntity : class//, new()
    {
        ICollection<TEntity> Read();
        TEntity Create(TEntity entity);
        TEntity Read(Type type, int id);
        void Update(TEntity entity);
        void Delete(Type type, int id);
    }
}
