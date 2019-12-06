using System;
using System.Linq;

namespace AOC_4_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var res = Enumerable.Range(183564, 473910).Count(IsValid);
        }

        public static bool IsValid(int numb)
        {
            var str = numb.ToString();
            char prev = str[0];
            // Check not descresing
            for (int i = 1; i < str.Length; i++)
            {
                if ((int)prev > str[i])
                {
                    return false;
                }
                prev = str[i]; // Next iteration
            }
            return str.ToCharArray().GroupBy(r => r.ToString()).Where(r => r.Count() == 2).Any();
        }
    }
}
