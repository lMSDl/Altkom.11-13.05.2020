using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    class Program
    {
        static void Main(string[] args)
        {
            //Vehicle vehicle = new Car();

            //vehicle.Move();

            //ISumCalculator calculator = new SumOddCalculator();
            //Console.WriteLine( calculator.Sum(1, 2, 3, 4, 5, 6, 7));

            int a = 4;
            int b = 5;

            SOLID.L.Rectangle rectangle = new SOLID.L.Square();
            rectangle.A = a;
            rectangle.B = b;

            Console.WriteLine($"{a}*{b} = {rectangle.Area}");

            Console.ReadKey();

        }
    }
}
