using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IPersonService
    {
        ICollection<Person> Read();
        Person Create(Person entity);
        Person Read(Type type, int id);
        void Update(Person entity);
        void Delete(Type type, int id);
    }
}
