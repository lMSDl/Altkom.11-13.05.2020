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
            new Instructor("Adam", "Adamski", Genders.Male) {InstructorId = 1, Specialization = "IT"},
            new Student("Piotr", "Piotrowski", Genders.Male) {StudentId = 1, YearsOfStudy = 2},
            new Student("Tomasz", "Piotrowski", Genders.Male) {StudentId = 3, YearsOfStudy = 2},
            new Student("Katarzyna", "Katarzyńska", Genders.Female) {StudentId = 2, YearsOfStudy = 4},
        };

        public Person Create(Person entity)
        {
            int id = 0;

            //id =
            //    (from person in _people
            //     select person.PersonId)
            //    .Max();

            id = _people.Select(person => person.Id).Max();

            //foreach (var person in _people)
            //{
            //    if (person.PersonId > id)
            //        id = person.PersonId;
            //}
            entity.Id = id + 1;
            _people.Add(entity);
            return entity;
        }

        public void Delete(Type type, int id)
        {
            var person = Read(type, id);
            _people.Remove(person);
        }

        public Person Read(Type type, int id)
        {
            //return (from x in _people
            //        where x.PersonId == id
            //        select x)
            //       .SingleOrDefault();

            //return _people.Where(x => x.PersonId == id).SingleOrDefault();

            return _people.Where(x => x.GetType() == type).SingleOrDefault(x => x.Id == id); //pojedyncza wartość lub null
            //return _people.Single(x => x.PersonId == id); //pojedyncza wartość lub wyjątek
            //return _people.FirstOrDefault(x => x.PersonId == id); //pierwsza znaleziona wartość lub null
            //return _people.First(x => x.PersonId == id); //pierwsza znaleziona wartość lub null





            //foreach (var person in _people)
            //{
            //    if (person.PersonId == id)
            //        return person;
            //}
            //return null;
        }

        public ICollection<Person> Read()
        {
            return _people.ToList();
            //return new List<Person>(_people);
        }

        public void Update(Person entity)
        {
            var person = Read(entity.GetType(), entity.Id);
            if (person == null)
                return;
            Delete(entity.GetType(), entity.Id);
            _people.Add(entity);
        }
    }
}
