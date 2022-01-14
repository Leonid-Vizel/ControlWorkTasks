using System;
using System.Collections.Generic;

namespace ZadachaEasy_Delegate
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Sotrudnik> collection = new List<Sotrudnik>();
            Random random = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < 20; i++)
            {
                collection.Add(new Sotrudnik($"имя{random.Next(0, 100)}", "1234", (byte)random.Next(0, 11), true));
                Console.WriteLine(collection[i].ToString());
            }
            Console.WriteLine("\n\n");
            Comparison<Sotrudnik> comp1 = new Comparison<Sotrudnik>(CompareName);
            Comparison<Sotrudnik> comp2 = new Comparison<Sotrudnik>(CompareNaglost);
            Comparison<Sotrudnik> comp3 = new Comparison<Sotrudnik>(CompareStupid);
            collection.Sort(comp1);
            collection.ForEach(x=>Console.WriteLine(x));
            Console.WriteLine("\n\n");
            collection.Sort(comp2);
            collection.ForEach(x => Console.WriteLine(x));
            Console.WriteLine("\n\n");
            collection.Sort(comp3);
            collection.ForEach(x => Console.WriteLine(x));
            Console.ReadKey();
        }

        public static int CompareName(Sotrudnik x, Sotrudnik y)
        {
            if (x.Name.Length < y.Name.Length)
            {
                return -1;
            }
            else if (x.Name.Length == y.Name.Length)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static int CompareNaglost(Sotrudnik x, Sotrudnik y)
        {
            if (x.naglost < y.naglost)
            {
                return -1;
            }
            else if (x.naglost == y.naglost)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public static int CompareStupid(Sotrudnik x, Sotrudnik y)
        {
            if (!x.stupid && y.stupid)
            {
                return -1;
            }
            else if ((x.stupid && y.stupid) || (!x.stupid && !y.stupid))
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
    }

    public delegate int ComparisonDelegate<in Sotrudnik>(Sotrudnik x, Sotrudnik y);

    public class Sotrudnik
    {
        public string Name { get; private set; }
        public string Department { get; private set; }
        public byte naglost { get; private set; }
        public bool stupid { get; private set; }

        public Sotrudnik(string Name, string Department, byte naglost, bool stupid)
        {
            this.Name = Name;
            this.Department = Department;
            this.naglost = naglost;
            this.stupid = stupid;
        }

        public override string ToString() => $"{Name} {Department} {naglost} {stupid}";
    }
}
