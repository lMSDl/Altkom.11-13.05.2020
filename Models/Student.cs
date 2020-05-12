using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Student : Person, IStudent
    {
        public Student()
        {

        }

        public Student(string firstName, string lastName, Genders gender) : base(firstName, lastName, gender)
        {
        }

        public Student(string firstName, string lastName, DateTime birthDate, Genders gender) : base(firstName, lastName, birthDate, gender)
        {
        }

        public int StudentId { get; set; }
        public int YearsOfStudy { get; set; }

        public override int Id { get => StudentId; set => StudentId = value; }

        protected override string GetAdditionalInfo()
        {
            return YearsOfStudy.ToString();
        }
    }
}
