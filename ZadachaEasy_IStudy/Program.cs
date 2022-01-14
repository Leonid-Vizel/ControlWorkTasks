using System;

namespace ZadachaEasy_IStudy
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person1 = new Person("1");
            Person person2 = new Person("2");
            Person person3 = new Person("3");
            person1.DoItYourself();
            person1.WriteOffTheSmartGuy(person2);
            person1.WriteOffTheSmartGuy(person2, person3);
        }
    }

    public interface IStudy
    {
        void WriteOffTheSmartGuy(Person smartGuy);
        void DoItYourself();
    }

    public class Person : IStudy
    {
        public string name { get; private set; }

        public Person(string name) => this.name = name;

        public Person() => name = "default";

        public void WriteOffTheSmartGuy(Person smartGuy) => Console.WriteLine($"Хе-хе-хе, я списал у '{smartGuy.name}'!");

        public void DoItYourself() => Console.WriteLine($"Хе-хе-хе, я сделал всё сам!");

        public void WriteOffTheSmartGuy(Person one, Person two) => Console.WriteLine($"Хе-хе-хе, я здесь самый умный и списал у '{one.name}' и '{two.name}'!");
    }
}
