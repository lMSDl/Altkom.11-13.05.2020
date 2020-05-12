using ConsoleApp.Models;
using Models;
using Service.Interfaces;
using Service.Local;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConsoleApp
{
    class Program
    {
        static IService<Person> Service = new PersonService();
        delegate void OutputDelegate(string output);
        //c++: typedef void (* OutputDelegate) (string);

        //static OutputDelegate Output;
        static Action<string> Output;

        static void Main(string[] args)
        {
            //Podpięcie do delegata funkcji Display
            Output += Display;

            // Podpięcie do delegata funkcji anonimowej
            //Output += delegate(string @string) {
            //    Debug.WriteLine(DateTime.Now);
            //    Debug.WriteLine(@string);
            //};

            //Wykorzystanie wyrażenia lambda
            // (<NazwaParametru>) => <CiałoWyrażenia>
            Output += (@string) => {
                Debug.WriteLine(DateTime.Now);
                Debug.WriteLine(@string);
            }; 


            do
            {
                DisplayPeople();
            } while (ExecuteCommand(Console.ReadLine()));
        }

        //static void Logger(string @string)
        //{
        //    Debug.WriteLine(@string);
        //} 

        private static void DisplayPeople()
        {
            var strings = new List<string>();

            var format = "{0, -3} {1, -15} {2, -15} {3, -30} {4, -10}";
            strings.Add(string.Format(format, Properties.Resources.Id, Properties.Resources.LastName, Properties.Resources.FirstName, Properties.Resources.BirthDate, Properties.Resources.Gender));
            var people = Service.Read();
            foreach (var person in people)
            {
                //strings.Add(person.PersonId + "\t" + person.LastName + "\t" + person.FirstName + "\t" + person.BirthDate);
                strings.Add(string.Format(format, 
                    person.PersonId, 
                    person.LastName,
                    person.FirstName, 
                    person.BirthDate.ToLongDateString(), 
                    Properties.Resources.ResourceManager.GetString(person.Gender.ToString())));
                //strings.Add($"{person.PersonId}\t{person.LastName}\t{person.FirstName}\t{person.BirthDate}");
            }


            Output?.Invoke(string.Join("\n", strings));
            //if (Output != null)
            //    Output(string.Join("\n", strings));

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
                Output?.Invoke(Properties.Resources.UnknownCommand);
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
            person.FirstName = ReadPersonData(Properties.Resources.FirstName, person.FirstName, null);
            person.LastName = ReadPersonData(Properties.Resources.LastName, person.LastName, x => string.IsNullOrWhiteSpace(x));

            var birthDateString = ReadPersonData(Properties.Resources.BirthDate, person.BirthDate.ToShortDateString(), x => !DateTime.TryParse(x, out _));
            person.BirthDate = DateTime.Parse(birthDateString);

            var genderString = ReadPersonData(Properties.Resources.Gender, person.Gender.ToString(), x => !Enum.TryParse<Genders>(x, out _));
            person.Gender = (Genders)Enum.Parse(typeof(Genders), genderString);
        }

        //delegate bool PersonDataValidator(string input); == Func<string, bool>
        private static string ReadPersonData(string label, string currentValue, Func<string, bool> validator)
        {
            string line;
            do
            {
                Output?.Invoke(label);
                SendKeys.SendWait(currentValue);
                line = Console.ReadLine();
            } while (validator?.Invoke(line) ?? false);
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
