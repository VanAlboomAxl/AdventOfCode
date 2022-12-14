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

        public static string CreateMD5(string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                return Convert.ToHexString(hashBytes); // .NET 5 +
            }
        }

    }
}
