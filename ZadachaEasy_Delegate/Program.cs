using System;
using System.Collections.Generic;
using System.Linq;

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
                collection.Add(new Sotrudnik($"имя{random.Next(0, 100)}","1234", (byte)random.Next(0,11),true));
                Console.WriteLine(collection[i].ToString());
            }
            Console.WriteLine("\n\n");
            SortDelegate @delegate = new SortDelegate(SortExamp);
            @delegate.Invoke(ref collection);

            foreach (Sotrudnik sotrudnik in collection)
            {
                Console.WriteLine(sotrudnik.ToString());
            }
        }

        //Как продемонстрировать без методов в классе?
        private static void SortExamp(ref List<Sotrudnik> sotrudniks)
        {
            sotrudniks = sotrudniks.OrderBy(x => x.Name).ThenBy(x => x.naglost).ThenBy(x => x.stupid).ToList();
        }
    }

    public delegate void SortDelegate(ref List<Sotrudnik> sotrudniks);

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
