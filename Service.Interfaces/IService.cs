using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IService<TEntity> where TEntity : class, new()
    {
        TEntity Create(TEntity entity);
        TEntity Read(int id);
        void Update(TEntity entity);
        void Delete(int id);
    }
}
