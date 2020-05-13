using Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Local
{
    class Service<T> : IService<T> where T : Base, new()
    {
        private static readonly ICollection<T> _entites = new List<T>
        {
        };

        public T Create(T entity)
        {
            int id = 0;
            id = _entites.Select(person => person.Id).Max();
            entity.Id = id + 1;
            _entites.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var person = Read(id);
            _entites.Remove(person);
        }

        public ICollection<T> Read()
        {
            return _entites.ToList();
        }

        public T Read(int id)
        {
            return _entites.SingleOrDefault(x => x.Id == id);
        }

        public void Update(T entity)
        {
            var person = Read(entity.Id);
            if (person == null)
                return;
            Delete(entity.Id);
            _entites.Add(entity);
        }
    }
}
