using Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Local
{
    public class PersonService : IService<Person>
    {
        private readonly ICollection<Person> _people = new List<Person>
        {
            new Person{PersonId = 1, FirstName = "Adam", LastName = "Adamski", BirthDate = new DateTime(1976, 1, 23)},
            new Person{PersonId = 2, FirstName = "Piotr", LastName = "Piotrowski", BirthDate = new DateTime(1980, 5, 3)},
            new Person{PersonId = 3, FirstName = "Katarzyna", LastName = "Katarzyńska", BirthDate = new DateTime(1949, 2, 19)},
        };

        public Person Create(Person entity)
        {
            int id = 0;
            foreach (var person in _people)
            {
                if (person.PersonId > id)
                    id = person.PersonId;
            }
            entity.PersonId = id + 1;
            _people.Add(entity);
            return entity;
        }

        public void Delete(int id)
        {
            var person = Read(id);
            _people.Remove(person);
        }

        public Person Read(int id)
        {
            foreach (var person in _people)
            {
                if (person.PersonId == id)
                    return person;
            }
            return null;
        }

        public ICollection<Person> Read()
        {
            return new List<Person>(_people);
        }

        public void Update(Person entity)
        {
            var person = Read(entity.PersonId);
            if (person == null)
                return;
            Delete(entity.PersonId);
            _people.Add(entity);
        }
    }
}
