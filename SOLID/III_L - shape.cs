using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID.L
{
    public abstract class Shape
    {
        public abstract int Area { get; }
    }

    class Rectangle : Shape
    {
        public virtual int A { get; set; }
        public virtual int B { get; set; }

        public override int Area => A * B;
    }

    class Square : Rectangle
    {
        public override int A { get => base.A; set => base.A = base.B = value; }
        public override int B { get => base.B; set => base.A = base.B = value; }
    }

    
}
