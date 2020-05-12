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
using ConsoleApp.Extensions;

namespace ConsoleApp
{
    class Program
    {
        static IService<Person> Service = new PersonService();
        delegate void OutputDelegate(string output);
        //c++: typedef void (* OutputDelegate) (string);

        //static OutputDelegate Output;
        static Action<string> Output;

        //delegate ICollection<Person> FilterDelegate(ICollection<Person> people);
        static Func<ICollection<Person>, ICollection<Person>> FilterFunc;

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

            var format = "{0, -3} {1, -15} {2, -15} {3, -30} {4, -10} {5, -10} {6}";
            strings.Add(string.Format(format, Properties.Resources.Id, Properties.Resources.LastName, Properties.Resources.FirstName, Properties.Resources.BirthDate, Properties.Resources.Gender,
                Properties.Resources.Type, Properties.Resources.AdditionalInfo));
            var people = Service.Read();
            if(FilterFunc != null)
                people = FilterFunc(people);
            //people = FilterFunc?.Invoke(people) ?? people;


            strings.AddRange(people.Select(person => person.ToString(format)));

            /*foreach (var person in people)
            {
                //strings.Add(person.PersonId + "\t" + person.LastName + "\t" + person.FirstName + "\t" + person.BirthDate);
                strings.Add(string.Format(format, 
                    person.PersonId, 
                    person.LastName,
                    person.FirstName, 
                    person.BirthDate.ToLongDateString(), 
                    Properties.Resources.ResourceManager.GetString(person.Gender.ToString())));
                //strings.Add($"{person.PersonId}\t{person.LastName}\t{person.FirstName}\t{person.BirthDate}");
            }*/


            //Output?.Invoke(string.Join("\n", strings));
            Output?.Invoke(strings.Aggregate(/*string.Empty,*/ (a, b) => a + "\n" + b));


            //if (Output != null)
            //    Output(string.Join("\n", strings));

        }

        static bool ExecuteCommand(string input)
        {
            var splittedInput = input.Split(' ');

            //var id = splittedInput.Length > 1 ? int.Parse(splittedInput[1]) : 0;
            int id = 0;
            Type type = null;
            if (splittedInput.Length > 1)
            {
                type = typeof(Person).Assembly.GetType(typeof(Person).Namespace + "." + splittedInput[1], false, true);
                if(splittedInput.Length > 2)
                    id = splittedInput[2].ToInt() ?? 0;
            }

            switch (splittedInput[0].ToCommand())
            {
                case Commands.Exit:
                    return false;
                case Commands.Edit:
                    EditPerson(type, id);
                    break;
                case Commands.Add:
                    if (type == typeof(Student))
                        AddStudent();
                    else if (type == typeof(Instructor))
                        AddInstructor();
                    break;
                case Commands.Delete:
                    Service.Delete(type, id);
                    break;
                case Commands.Filter:
                    Filter();
                    break;
                default:
                    Output?.Invoke(Properties.Resources.UnknownCommand);
                    Console.ReadKey();
                    break;
            }

            return true;
        }

        private static void Filter()
        {
            //FilterFunc = people => { return people.Where(x => x.PersonId > 1).ToList(); };

            //FilterFunc = people => people.Where(x => x.LastName.ToUpper().Contains('A')).ToList();

            //FilterFunc = people => people.Where(x => x.BirthDate < new DateTime(1980, 1, 1)).ToList();
            //FilterFunc = people => people.Where(x => x.BirthDate.Year < 1980).ToList();

            //FilterFunc = people => people.Where(x => new DateTime((DateTime.Now - x.BirthDate).Ticks).Year > 50).Where(x => x.FirstName.ToUpper().Contains('A')).ToList();
            FilterFunc = people => people.Where(x => x.GetAge() > 50).Where(x => x.FirstName.ToUpper().Contains('A')).ToList();
        }

        private static void AddStudent()
        {
            var person = new Student();
            EditPerson(person);
            Service.Create(person);
        }
        private static void AddInstructor()
        {
            var person = new Instructor();
            EditPerson(person);
            Service.Create(person);
        }


        static void EditPerson(Person person)
        {
            person.FirstName = ReadPersonData(Properties.Resources.FirstName, person.FirstName, null);
            person.LastName = ReadPersonData(Properties.Resources.LastName, person.LastName, x => string.IsNullOrWhiteSpace(x));

            var birthDateString = ReadPersonData(Properties.Resources.BirthDate, person.BirthDate.ToShortDateString(), x => !DateTime.TryParse(x, out _));
            person.BirthDate = DateTime.Parse(birthDateString);

            var genderString = ReadPersonData(Properties.Resources.Gender, person.Gender.ToString(), x => x.ToEnum<Genders>() == null);
            person.Gender = genderString.ToEnum<Genders>().Value;
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

        static void EditPerson(Type type, int id)
        {
            var person = Service.Read(type, id);
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
