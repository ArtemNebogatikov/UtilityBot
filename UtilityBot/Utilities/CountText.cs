using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Utilities
{
    class CountText
    {
        public static int LengthText(string text)
        {
            string somestring = text.Replace(" ", "");
            char[] chars = somestring.ToCharArray();
            return chars.Length;
        }
    }
}
