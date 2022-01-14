using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace Zadacha_Toilet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Separator sep = new Separator();
            for (int i = 0; i < 100; i++)
            {
                sep.queue.Enqueue(new Person($"Person{i}",(Sex)(i % 2)));
            }
            Console.ReadKey();
        }
    }

    public enum Sex { Man, Woman }
    public enum ToiletType { Man, Woman, General }

    public class Person
    {
        public string Name { get; private set; }
        public Sex Sex { get; private set; }
        public Person(string Name, Sex Sex)
        {
            this.Name = Name;
            this.Sex = Sex;
        }
    }

    public class Toilet
    {
        private Person inside1;
        private Person inside2;
        private Stack<Person> stack;
        public ToiletType Type { get; private set; }
        public Separator parent { get; private set; }

        public Toilet(ToiletType Type, Separator parent)
        {
            this.Type = Type;
            this.parent = parent;
            stack = new Stack<Person>();
        }

        public bool Put(Person visitor)
        {
            if (stack.Count >= 5)
            {
                return false;
            }
            else
            {
                stack.Push(visitor);
                return true;
            }
        }


        public void CleanToilet(object sender, ElapsedEventArgs e)
        {
            if (stack.Count > 0)
            {
                inside1 = stack.Pop();
                Console.WriteLine($"В кабинку 1 туалета {Type} входит {inside1.Name}");
                if (stack.Count > 0)
                {
                    inside2 = stack.Pop();
                    Console.WriteLine($"В кабинку 2 туалета {Type} входит {inside2.Name}");
                }
            }
            else if (parent.queue.Count > 0)
            {
                int count = 0;
                switch (Type)
                {
                    case ToiletType.Man:
                        for (int i = 0; i < parent.queue.Count; i++)
                        {
                            Person dequeued = parent.queue.Dequeue();
                            if (dequeued.Sex == Sex.Man)
                            {
                                if (count == 0)
                                {
                                    count++;
                                    inside1 = dequeued;
                                    Console.WriteLine($"В кабинку 1 туалета {Type} входит {inside1.Name}");
                                }
                                else if (count == 1)
                                {
                                    count++;
                                    inside2 = dequeued;
                                    Console.WriteLine($"В кабинку 2 туалета {Type} входит {inside2.Name}");
                                }
                                else if (stack.Count < 5)
                                {
                                    stack.Push(dequeued);
                                    Console.WriteLine($"{dequeued.Name} Встаёт в стек туалета {Type}");
                                }
                                else
                                {
                                    parent.queue.Enqueue(dequeued);
                                }
                            }
                        }
                        break;
                    case ToiletType.Woman:
                        for (int i = 0; i < parent.queue.Count; i++)
                        {
                            Person dequeued = parent.queue.Dequeue();
                            parent.queue.Enqueue(dequeued);
                            if (dequeued.Sex == Sex.Woman)
                            {
                                if (count == 0)
                                {
                                    count++;
                                    inside1 = dequeued;
                                    Console.WriteLine($"В кабинку 1 туалета {Type} входит {inside1.Name}");
                                }
                                else if (count == 1)
                                {
                                    count++;
                                    inside2 = dequeued;
                                    Console.WriteLine($"В кабинку 2 туалета {Type} входит {inside2.Name}");
                                }
                                else if (stack.Count < 5)
                                {
                                    stack.Push(dequeued);
                                    Console.WriteLine($"{dequeued.Name} Встаёт в стек туалета {Type}");
                                }
                                else
                                {
                                    parent.queue.Enqueue(dequeued);
                                }
                            }
                        }
                        break;
                    default:
                        if (parent.queue.Count > 0)
                        {
                            inside1 = parent.queue.Dequeue();
                            Console.WriteLine($"В кабинку 1 туалета {Type} входит {inside1.Name}");
                            count++;
                            if (parent.queue.Count > 0)
                            {
                                inside2 = parent.queue.Dequeue();
                                Console.WriteLine($"В кабинку 2 туалета {Type} входит {inside2.Name}");
                                count++;
                                while (stack.Count < 5 && parent.queue.Count > 0)
                                {
                                    Person dequeued = parent.queue.Dequeue();
                                    stack.Push(dequeued);
                                    Console.WriteLine($"{dequeued.Name} Встаёт в стек туалета {Type}");
                                }
                            }
                        }
                        break;
                }
                if (count == 0)
                {
                    inside1 = inside2 = null;
                    if (inside1 != null)
                    {
                        Console.WriteLine($"{inside1.Name} покидает кабинку 1 туалета {Type}");
                    }
                    if (inside2 != null)
                    {
                        Console.WriteLine($"{inside2.Name} покидает кабинку 2 туалета {Type}");
                    }
                }
                else if (count == 1)
                {
                    inside2 = null;
                    if (inside2 != null)
                    {
                        Console.WriteLine($"{inside2.Name} покидает кабинку 2 туалета {Type}");
                    }
                }
            }
            else
            {
                inside1 = inside2 = null;
                if (inside1 != null)
                {
                    Console.WriteLine($"{inside1.Name} покидает кабинку 1 туалета {Type}");
                }
                if (inside2 != null)
                {
                    Console.WriteLine($"{inside2.Name} покидает кабинку 2 туалета {Type}");
                }
            }
        }
    }

    public class Separator
    {
        public Queue<Person> queue;
        private Toilet forMan;
        private Toilet forWoman;
        private Toilet forGeneral;
        private Timer timer;

        public Separator()
        {
            queue = new Queue<Person>();
            forMan = new Toilet(ToiletType.Man, this);
            forWoman = new Toilet(ToiletType.Woman, this);
            forGeneral = new Toilet(ToiletType.General, this);
            timer = new Timer();
            timer.Interval = 1500;
            timer.Elapsed += forMan.CleanToilet;
            timer.Elapsed += forWoman.CleanToilet;
            timer.Elapsed += forGeneral.CleanToilet;
            timer.AutoReset = true;
            timer.Start();
        }
    }
}