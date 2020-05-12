using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Extensions
{
    public static class StringExtensions
    {
        public static int? ToInt(this string @string)
        {
            if (int.TryParse(@string, out var @int))
                return @int;
            return null;
        }

        public static T? ToEnum<T>(this string @string) where T : struct
        {
            if (Enum.TryParse(@string, true, out T command))
                return command;
            return null;
        }

        public static Commands? ToCommand(this string @string)
        {
            return @string.ToEnum<Commands>();

            //if (Enum.TryParse(@string, out Commands command))
            //    return command;
            //return null;
        }
    }
}
