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
    public class PersonServiceAsync : IServiceAsync<Person>
    {
        public async Task<Person> CreateAsync(Person entity)
        {
            using (var context = new Context())
            {
                entity = (Person)context.Set(entity.GetType()).Add(entity);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task DeleteAsync(Type type, int id)
        {
            using (var context = new Context())
            {
                var entity = await context.Set(type).FindAsync(id);
                if (entity == null)
                    return;
                context.Set(type).Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Person>> ReadAsync()
        {
            using (var context = new Context())
            {
                var students = await context.Set<Student>().ToListAsync();
                var instructors = await context.Set<Instructor>().ToListAsync();
                return students.Cast<Person>().Concat(instructors).ToList();
            }
        }

        public async Task<Person> ReadAsync(Type type, int id)
        {
            using (var context = new Context())
            {
                return (Person)await context.Set(type).FindAsync(id);
            }
        }

        public async Task UpdateAsync(Person entity)
        {
            using (var context = new Context())
            {
                context.Set(entity.GetType()).Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                await context.SaveChangesAsync();
            }
        }
    }
}
