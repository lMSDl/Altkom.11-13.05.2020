using Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace DAL.Services
{
    public class PersonService : IService<Person>
    {
        public Person Create(Person entity)
        {
            using (var context = new Context())
            {
                entity = (Person)context.Set(entity.GetType()).Add(entity);
                context.SaveChanges();
                return entity;
            }
        }

        public void Delete(Type type, int id)
        {
            using (var context = new Context())
            {
                var entity = context.Set(type).Find(id);
                if (entity == null)
                    return;
                context.Set(type).Remove(entity);
                context.SaveChanges();
            }
        }

        public ICollection<Person> Read()
        {
            using (var context = new Context())
            {
                var students = context.Set<Student>().ToList();
                var instructors = context.Set<Instructor>().ToList();
                return students.Cast<Person>().Concat(instructors).ToList();
            }
        }

        public Person Read(Type type, int id)
        {
            using (var context = new Context())
            {
                return (Person)context.Set(type).Find(id);
            }
        }

        public void Update(Person entity)
        {
            using (var context = new Context())
            {
                context.Set(entity.GetType()).Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
