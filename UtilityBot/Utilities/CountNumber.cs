using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Utilities
{
    class CountNumber
    {
        public static int Count(string text)
        {
            int count = 0;
            string numbers = text.Replace(" ", "");
            foreach(var item in numbers)
            {
                int number = CharUnicodeInfo.GetDecimalDigitValue(item);
                count += number;
            }
            return count;
        }
    }
}
