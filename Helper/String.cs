using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class String
    {

        public static string CamelCase(string text)
        {
            return string.Join("", text
                                .Split(' ')
                                .Select(i => char.ToUpper(i[0]) + i.Substring(1)));
        }

        public static string SeparatedWordsStartsWithCapitalLetters(string text)
        {
            return string.Join(" ", text.Trim()
                                .Split()
                                .Where(i => i != "").Select(i => char.ToUpper(i[0]) + i.Substring(1)));
        }

        public static string RemoveSpaces(string text)
        {
            return string.Join("", text.Trim().Split(' '));
        }

    }
}
