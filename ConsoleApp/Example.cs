using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public class Example
    {
        public static void ABC()
        {
            Nullable<int> a = null;
            int? b = 5;
            int c;
            
            if (a - b == 0)
                c = (a + b) ?? 0;
            else
            {
                var result = a - b;
                if (result.HasValue) // if(result != null)
                    c = result.Value;
                else
                    c = 0;

                //c = result ?? 0;
            }

            c = (a - b == 0 ? a + b : a - b) ?? 0;
        }
    }
}
