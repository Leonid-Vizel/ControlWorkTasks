using System;

namespace ZadachaEasy_Student
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Student student1 = new ForeignStudent("student1", Living.DU);
            Student student2 = new ForeignStudent("student2", Living.Flat);
            Student student3 = new LocalStudent("student3");
            student1.Eat();
            student2.Eat();
            student2.Eat();
            student2.Eat();
            student3.Eat();
        }
    }

    public abstract class Student
    {
        public readonly string name;

        public Student(string name) => this.name = name;

        abstract public void Eat();
    }

    public class ForeignStudent : Student
    {
        public Living living;
        private Random rnd;

        public ForeignStudent(string name, Living living) : base(name)
        {
            this.living = living;
            rnd = new Random(DateTime.Now.Millisecond);
        }
        
        public override void Eat()
        {
            if (living == Living.DU)
            {
                Console.WriteLine("Хе-хе-хе, я украл соседский дошик)))");
            }
            else
            {
                if (rnd.Next(0,2) == 1)
                {
                    Console.WriteLine("*Наелся в Доброй столовке и спит*");
                }
                else
                {
                    Console.WriteLine("Пишов за дошиком в пятёрочку");
                }
            }
        }
    }

    public class LocalStudent : Student
    {
        public LocalStudent(string name) : base(name) {/*Nothing*/}

        public override void Eat() => Console.WriteLine("Поел борща)");
    }

    public enum Living {DU, Flat}
}
