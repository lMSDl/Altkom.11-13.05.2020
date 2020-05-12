using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Instructor : Person, IInstructor
    {
        public Instructor()
        {

        }

        public Instructor(string firstName, string lastName, Genders gender) : base(firstName, lastName, gender)
        {
        }

        public Instructor(string firstName, string lastName, DateTime birthDate, Genders gender) : base(firstName, lastName, birthDate, gender)
        {
        }

        public int InstructorId { get; set; }
        public string Specialization { get; set; }

        public override int Id { get => InstructorId; set => InstructorId = value; }

        protected override string GetAdditionalInfo()
        {
            return Specialization;
        }
    }
}
