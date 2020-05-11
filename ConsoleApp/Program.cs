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
            var strings = new List<string>();

            var format = "{0, -3} {1, -15} {2, -15} {3, -10}";
            strings.Add(string.Format(format, Properties.Resources.Id, Properties.Resources.LastName, Properties.Resources.FirstName, Properties.Resources.BirthDate));
            var people = Service.Read();
            foreach (var person in people)
            {
                //strings.Add(person.PersonId + "\t" + person.LastName + "\t" + person.FirstName + "\t" + person.BirthDate);
                strings.Add(string.Format(format, person.PersonId, person.LastName, person.FirstName, person.BirthDate.ToLongDateString()));
                //strings.Add($"{person.PersonId}\t{person.LastName}\t{person.FirstName}\t{person.BirthDate}");
            }

            Display(string.Join("\n", strings));
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
                }
            }
            else
            {
                Display(Properties.Resources.UnknownCommand);
                Console.ReadKey();
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
            person.FirstName = ReadPersonData(Properties.Resources.FirstName, person.FirstName);
            person.LastName = ReadPersonData(Properties.Resources.LastName, person.LastName);

            DateTime birthDate;
            do
            {
                Display(Properties.Resources.BirthDate);
                SendKeys.SendWait(person.BirthDate.ToShortDateString());
            }
            while (!DateTime.TryParse(Console.ReadLine(), out birthDate));
            person.BirthDate = birthDate;
        }

        private static string ReadPersonData(string label, string currentValue)
        {
            string line;
            do
            {
                Display(label);
                SendKeys.SendWait(currentValue);
                line = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(line));
            return line;
        }

        static void EditPerson(int id)
        {
            var person = Service.Read(id);
            if (person == null)
                return;
            EditPerson(person);
            Service.Update(person);
        }

        static void Display(string output)
        {
            Console.Clear();
            Console.WriteLine(output);
            Console.WriteLine();
        }
    }
}
