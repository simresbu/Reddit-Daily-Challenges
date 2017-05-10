using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge_314
{
    class Program
    {
        static string test1 = "79 82 34 83 69";
        static string test2 = "420 34 19 71 341";
        static string test3 = "17 32 91 7 46";

        static void Main(string[] args)
        {
            string[] lg1 = concaternateInt(test1);
            Console.WriteLine("Lowest: " + lg1[0] + " Highest: " + lg1[1]);
            string[] lg2 = concaternateInt(test2);
            Console.WriteLine("Lowest: " + lg2[0] + " Highest: " + lg2[1]);
            string[] lg3 = concaternateInt(test3);
            Console.WriteLine("Lowest: " + lg3[0] + " Highest: " + lg3[1]);
            Console.ReadKey();
        }
    
        static string[] concaternateInt(string Input)
        {
            string[] lg = new string[2];
            string stringToMaipulate = Input;
            List<string> numbers = new List<string>();
            while (stringToMaipulate.Length >0)
            {
                string temp = "";
                if (stringToMaipulate.IndexOf(' ') == -1)
                {
                    temp = stringToMaipulate;
                    numbers.Add(temp);
                    break;
                }
                else
                    temp = stringToMaipulate.Substring(0, stringToMaipulate.IndexOf(' '));
                numbers.Add(temp);
                stringToMaipulate = stringToMaipulate.Substring(stringToMaipulate.IndexOf(' ')+1);
            }
            numbers.Sort();
            string lowest = "";
            foreach (string s in numbers)
            {
                lowest += s;
            }
            lg[0] = lowest;
            numbers.Reverse();
            string greatest = "";
            foreach (string s in numbers)
            {
                greatest += s;
            }
            lg[1] = greatest;
            return lg;
        }
    }
}
