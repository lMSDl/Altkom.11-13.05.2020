using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    //public class SumCalculator
    //{
    //    public int Sum(params int[] numbers)
    //    {
    //        return numbers.Sum();
    //    }
    //}

    //public class SumOddCalculator : SumCalculator
    //{
    //    public new int Sum(params int[] numbers)
    //    {
    //        return numbers.Where(x => x % 2 != 0).Sum();
    //    }
    //}

        public interface ISumCalculator
    {
        int Sum(params int[] numbers);
    }

    public class SumCalculator : ISumCalculator
    {
        public int Sum(params int[] numbers)
        {
            return numbers.Sum();
        }
    }

    public class SumOddCalculator : ISumCalculator
    {
        public int Sum(params int[] numbers)
        {
            return numbers.Where(x => x % 2 != 0).Sum();
        }
    }

}
