using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Extensions
{
    public static class PersonExtensions
    {
        public static int GetAge(this Person person)
        {
            return DateTime.Now.Year - person.BirthDate.Year;
        }
    }
}
