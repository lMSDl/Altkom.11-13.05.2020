using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public abstract class Person
    {
        protected Person()
        {
        }

        private static DateTime GenerateBirthDate(int seed)
        {
            var random = new Random(seed);
            return new DateTime(random.Next(1940, 1990), random.Next(1, 12), random.Next(1, 28));
        }

        protected Person(string firstName, string lastName, DateTime birthDate, Genders gender) : this(firstName, lastName, gender)
        {
            BirthDate = birthDate;
        }

        protected Person(string firstName, string lastName, Genders gender) : this()
        {
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            BirthDate = GenerateBirthDate(lastName.GetHashCode());
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public Genders Gender { get; set; }

        [JsonIgnore]
        public abstract int Id { get; set; }



        public string ToString(string format)
        {
            return string.Format(format,
                    Id,
                    LastName,
                    FirstName,
                    BirthDate.ToLongDateString(),
                    Gender.ToString(),
                    GetType().Name,
                    GetAdditionalInfo());
        }

        protected virtual string GetAdditionalInfo()
        {
            return string.Empty;
        }
    }
}
