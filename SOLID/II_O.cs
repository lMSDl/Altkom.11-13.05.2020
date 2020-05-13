using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    //abstract class Shape
    //{
    //}

    //class Square : Shape
    //{
    //    public int A { get; set; }
    //}

    //class Rectangle : Shape
    //{
    //    public int A { get; set; }
    //    public int B { get; set; }
    //}

    //class ShapeCalculator
    //{
    //    int Area(Shape shape)
    //    {
    //        switch(shape)
    //        {
    //            case Square square:
    //                return square.A * square.A;
    //            case Rectangle rectangle:
    //                return rectangle.B * rectangle.B;

    //            default:
    //                return 0;
    //        }
    //    }
    //}

    public abstract class Shape
    {
        //public abstract int Area();
        public abstract int Area { get; }
    }

    public class Square : Shape
    {
        public int A { get; set; }

        public override int Area => A*A;

    }

    public class Rectangle : Shape
    {
        public int A { get; set; }
        public int B { get; set; }

        public override int Area => A*B;

        //public override int Area()
        //{
        //    return A * B;
        //}
    }

    class ShapeCalculator
    {
        int Area(Shape shape)
        {
            return shape.Area;
        }
    }
}
