using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    //abstract class Vehicle
    //{
    //    public string Name { get; set; }
    //    public abstract void Fly();
    //}

    //class Airplain : Vehicle
    //{
    //    public override void Fly()
    //    {
    //        Console.WriteLine("I Am flying!");
    //    }
    //}

    //class Car : Vehicle
    //{
    //    public override void Fly()
    //    {
    //        throw new NotSupportedException();
    //    }
    //}

    abstract class Vehicle
    {
        public string Name { get; set; }
        public abstract void Move();
    }

    class Airplain : Vehicle
    {
        public override void Move()
        {
            Console.WriteLine("I Am flying!");
        }
    }

    class Car : Vehicle
    {
        public override void Move()
        {
            Console.WriteLine("I am driving!");
        }
    }
}
