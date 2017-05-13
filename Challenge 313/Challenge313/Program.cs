using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Challenge313
{
    class Program
    {
        static List<string> dict = new List<string>();
        static MaxLetterCount head;
        static MaxLetterCount pointer;
        static int[] letterCount = new int[25];
        static char[] theMasterString;
        static List<char[]> permutationsOfTheMasterString1 = new List<char[]>();
        static int batchCounter = 0;
        static int numBatches = 1;

        static void Main(string[] args)
        {
            readFile();
            countNumOfEachLetter();
            printValue();
            //readFromFront("bee", theMasterString);
            //readFromBack("zoo", theMasterString);
            GetPer(theMasterString);
            Console.WriteLine("Finished Processing");
            Console.ReadKey();
        }

        static void readFile()
        {
            StreamReader SR = new StreamReader("dictionary.txt");
            while (SR.Peek() >= 0)
            {
                string temp = SR.ReadLine();
                dict.Add(temp);
            }
        }

        static void countNumOfEachLetter()
        {
            MaxLetterCount prev = null;
            char[] alphabet = { 'z', 'y', 'x', 'w', 'v', 'u', 't', 's', 'r', 'q', 'p', 'o', 'n', 'm', 'l', 'k', 'j', 'i', 'h', 'g', 'f', 'e', 'd', 'c', 'b', 'a' };
            for (int i = 0; i < alphabet.Length; i++)
            {
                MaxLetterCount curr = new MaxLetterCount(alphabet[i]);

                if (alphabet[i].Equals('z'))
                {
                    prev = curr;
                }
                else
                {
                    curr.next = prev;
                    head = curr;
                    prev = curr;

                }

            }
            pointer = head;

            //do the actual counting with some crazy looping
            char[] alphabet2 = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            foreach (string s in dict)
            {
                char[] charaters = s.ToCharArray();
                for (int i = 0; i < alphabet2.Length; i++)
                {
                    int count = 0;
                    for (int k = 0; k < charaters.Length; k++)
                    {
                        if (alphabet2[i].Equals(charaters[k]))
                        {
                            count++;
                        }
                    }
                    //point pointer to the right node
                    for (int x = 0; x < i; x++)
                    {
                        pointer = pointer.next;
                    }
                    //check if has highest occurances
                    if (pointer.mostOccurances < count)
                    {
                        pointer.mostOccurances = count;
                        pointer.wordMostOccurances = s;
                    }

                    pointer = head;
                }

            }
        }

        static void printValue()
        {
            int totalcount = 0;
            for (int i = 0; i < 26; i++)
            {
                Console.WriteLine(pointer.Letter + ": " + pointer.mostOccurances + " word: " + pointer.wordMostOccurances);
                totalcount += pointer.mostOccurances;
                pointer = pointer.next;
            }
            pointer = head;
            theMasterString = new char[totalcount];

            int index = 0;
            for (int y = 0; y < 26; y++)
            {
                for (int i = 0; i < pointer.mostOccurances; i++)
                {
                    theMasterString[index] = pointer.Letter;
                    index++;
                }

                pointer = pointer.next;

            }
            Console.WriteLine(totalcount);
            Console.WriteLine(theMasterString);
        }

        static Tuple<int[,], char[], bool> readFromFront(string s, char[] chararray)
        {
            char[] c = s.ToCharArray();
            int[,] iA = new int[c.Length, 2];
            int index = 0;
            for (int i = 0; i < c.Length; i++)
            {
                for (int y = index; y < chararray.Length; y++)
                {
                    if (c[i].Equals(chararray[y]))
                    {
                        iA[i, 0] = 1;
                        iA[i, 1] = y;
                        index = y + 1;
                        break;
                    }
                }
            }
            int count = 0;
            for (int i = 0; i < c.Length; i++)
            {
                count += iA[i, 0];
            }
            if (count < (iA.Length / 2))
                return Tuple.Create(iA, c, false);
            return Tuple.Create(iA, c, true);

        }

        static Tuple<int[,], char[], bool> readFromBack(string s, char[] chararray)
        {
            char[] chararrayreverse = chararray;
            Array.Reverse(chararrayreverse);
            char[] c = s.ToCharArray();
            int[,] iA = new int[c.Length, 2];
            int index = 0;
            for (int i = 0; i < c.Length; i++)
            {
                for (int y = index; y < chararrayreverse.Length; y++)
                {
                    if (c[i].Equals(chararrayreverse[y]))
                    {
                        iA[i, 0] = 1;
                        iA[i, 1] = y;
                        index = y + 1;
                        break;
                    }
                }
            }
            int count = 0;
            for (int i = 0; i < c.Length; i++)
            {
                count += iA[i, 0];
                iA[i, 1] = chararrayreverse.Length - 1 - iA[i, 1];
            }
            if (count < (iA.Length / 2))
                return Tuple.Create(iA, c, false);
            return Tuple.Create(iA, c, true);

        }

        private static void Swap(ref char a, ref char b)
        {
            if (a == b) return;

            a ^= b;
            b ^= a;
            a ^= b;
        }

        public static void GetPer(char[] list)
        {
            int x = list.Length - 1;
            GetPer(list, 0, x);
        }

        private static void GetPer(char[] list, int k, int m)
        {
            if (k == m)
            {
                permutationsOfTheMasterString1.Add(list);
                batchCounter++;
                if(batchCounter == 100)
                {
                    Tuple<bool, char[]> foundPermutation = checkPermutation();
                    if(foundPermutation.Item1)
                    {
                        Console.WriteLine(foundPermutation.Item2);
                        return;
                    }
                    permutationsOfTheMasterString1 = new List<char[]>();
                    batchCounter = 0;
                }

            }
            else
                for (int i = k; i <= m; i++)
                {
                    Swap(ref list[k], ref list[i]);
                    GetPer(list, k + 1, m);
                    Swap(ref list[k], ref list[i]);
                }
        }


        static public Tuple<bool,char[]> checkPermutation()
        {
            bool isgood = false;
            char[] p = new char[1];
            foreach (char[] c in permutationsOfTheMasterString1)
            {
                foreach (string s in dict)
                {
                    Tuple<int[,], char[], bool> t = readFromFront(s, c);
                    if (!t.Item3)
                    {
                        isgood = false;
                        break;
                    }
                        
                    else
                        isgood = true;
                }
                if (isgood)
                    return Tuple.Create(true,c);
            }
            return Tuple.Create(false,p);            
        }


    }
}
