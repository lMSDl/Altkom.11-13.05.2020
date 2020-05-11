using ConsoleApp.Models;
using Models;
using Service.Interfaces;
using Service.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp
{
    class Program
    {
        static IService<Person> Service = new PersonService();

        static void Main(string[] args)
        {
            do
            {
                Console.Clear();
                DisplayPeople();
            } while (ExecuteCommand(Console.ReadLine()));
        }

        private static void DisplayPeople()
        {
            var format = "{0, -3} {1, -15} {2, -15} {3, -10}";
            Console.WriteLine(string.Format(format, Properties.Resources.Id, Properties.Resources.LastName, Properties.Resources.FirstName, Properties.Resources.BirthDate));
            var people = Service.Read();
            foreach (var person in people)
            {
                //Console.WriteLine(person.PersonId + "\t" + person.LastName + "\t" + person.FirstName + "\t" + person.BirthDate);
                Console.WriteLine(string.Format(format, person.PersonId, person.LastName, person.FirstName, person.BirthDate.ToLongDateString()));
                //Console.WriteLine($"{person.PersonId}\t{person.LastName}\t{person.FirstName}\t{person.BirthDate}");
            }
        }

        static bool ExecuteCommand(string input)
        {
            var splittedInput = input.Split(' ');

            //var id = splittedInput.Length > 1 ? int.Parse(splittedInput[1]) : 0;
            int id = 0;
            if (splittedInput.Length > 1)
                int.TryParse(splittedInput[1], out id);

            if (Enum.TryParse(splittedInput[0], true, out Commands command))
            {
                switch (command)
                {
                    case Commands.Exit:
                        return false;
                    case Commands.Edit:
                        EditPerson(id);
                        break;
                    case Commands.Add:
                        AddPerson();
                        break;
                    case Commands.Delete:
                        Service.Delete(id);
                        break;
                    default:
                        Console.WriteLine(Properties.Resources.UnknownCommand);
                        Console.ReadKey();
                        break;
                }
            }

            return true;
        }

        private static void AddPerson()
        {
            var person = new Person();
            EditPerson(person);
            Service.Create(person);
        }

        static void EditPerson(Person person)
        {
            Console.WriteLine(Properties.Resources.FirstName);
            SendKeys.SendWait(person.FirstName);
            person.FirstName = Console.ReadLine();


            Console.WriteLine(Properties.Resources.LastName);
            SendKeys.SendWait(person.LastName);
            person.LastName = Console.ReadLine();


            Console.WriteLine(Properties.Resources.BirthDate);
            SendKeys.SendWait(person.BirthDate.ToShortDateString());
            person.BirthDate = DateTime.Parse(Console.ReadLine());
        }

        static void EditPerson(int id)
        {
            var person = Service.Read(id);
            if (person == null)
                return;
            EditPerson(person);
            Service.Update(person);
        }
    }
}
