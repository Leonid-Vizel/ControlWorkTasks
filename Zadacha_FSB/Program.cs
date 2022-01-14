using System;
using System.Collections.Generic;
using System.Linq;

namespace Zadachi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person marusia = new Person("Маруся");
            Person p1 = new Person("1");
            Person p2 = new Person("2");
            Person p3 = new Person("3");
            Person p4 = new Person("4");
            Person p5 = new Person("5");
            Person p6 = new Person("6");
            Person p7 = new Person("7");
            Person p8 = new Person("8");
            Person p9 = new Person("9");
            Person p10 = new Person("10");
            Person p11 = new Person("11");
            Person p12 = new Person("12");
            Person p13 = new Person("13");
            Person kolia = new Person("Коля");

            marusia.AddFriends(p1);
            marusia.AddFriends(p2);
            marusia.AddFriends(p3);
            marusia.AddFriends(p4);
            marusia.AddFriends(p5);

            p1.AddFriends(p6, p7);
            p2.AddFriends(p8);
            p3.AddFriends(p9, p10);
            p4.AddFriends(p11, p12);
            p5.AddFriends(p13);

            p6.AddFriends(kolia);
            p7.AddFriends(kolia);
            p9.AddFriends(kolia);
            p12.AddFriends(kolia);
            p13.AddFriends(kolia);

            Find(marusia, kolia, new bool[16]);
        }

        private static void Find(Person current, Person end, bool[] visited, string str = "")
        {
            str += $"-{current.Name}";
            visited[current.ID] = true;
            if (current.friends.Contains(end))
            {
                Console.WriteLine($"{str}-{end.Name}");
            }
            foreach(Person friend in current.friends.Where(x=>x.ID != end.ID))
            {
                if (!visited[friend.ID])
                {
                    Find(friend, end, visited, str);
                }
            }
        }
    }

    public class Person
    {
        private static int idcount;

        public int ID { get; private set; }
        public string Name { get; private set; }
        public List<Person> friends;

        public Person(string Name)
        {
            ID = idcount++;
            this.Name = Name;
            friends = new List<Person>();
        }

        public void AddFriends(params Person[] friends)
        {
            this.friends.AddRange(friends);
            foreach(Person friend in friends)
            {
                friend.friends.Add(this);
            }
        }
    }
}