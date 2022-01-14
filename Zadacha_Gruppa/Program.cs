using System;
using System.Collections.Generic;
using System.Linq;

namespace Zadacha_Gruppa
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            manager.Enter();
            Console.ReadKey();
        }

        public class Manager
        {
            public decimal Money { get; private set; }
            public List<Payment> payments;
            public List<Holiday> holidays;
            public List<string> group;
            public Manager(bool generate = true)
            {
                payments = new List<Payment>();
                holidays = new List<Holiday>();
                group = new List<string>();
                if (generate)
                {
                    for(int i = 0; i < 40; i++)
                    {
                        group.Add($"Студент{i}");
                    }
                }
            }

            public void Enter()
            {
                bool alive = true;
                while (alive)
                {
                    Console.Clear();
                    Console.WriteLine("Добро пожаловать в менеджер средств группы!\nВыберите опцию:");
                    Console.WriteLine("[0]Внести платёж");
                    Console.WriteLine("[1]Новое мероприятие");
                    Console.WriteLine("[2]История платежей");
                    Console.WriteLine("[3]История мероприятий");
                    Console.WriteLine("[4]Список группы");
                    Console.WriteLine("[5]Добавить в группу");
                    Console.WriteLine("[6]Убрать из группы");
                    Console.WriteLine("[7]Выход");
                    if (!int.TryParse(Console.ReadLine(), out int answer) || answer < 0 || answer > 7)
                    {
                        continue;
                    }
                    Console.Clear();
                    switch (answer)
                    {
                        case 0:
                            Console.WriteLine("Внесение платежа начато!");
                            Console.WriteLine("Выберите отправителя:");
                            for (int i = 0; i < group.Count; i++)
                            {
                                Console.WriteLine($"[{i}]{group[i]}");
                            }
                            Console.Write("Ответ: ");
                            if (!int.TryParse(Console.ReadLine(), out int nameID) || nameID < 0 || nameID >= group.Count)
                            {
                                Console.WriteLine("Неверно введён номер студента. Попробуйте снова.");
                                Console.ReadKey();
                                continue;
                            }
                            Console.Write("Введите сумму перевода: ");
                            if (!decimal.TryParse(Console.ReadLine().Replace(".", ","), out decimal moneyInput) || moneyInput < 0)
                            {
                                Console.WriteLine("Неверно введёна сумма перевода. Попробуйте снова.");
                                Console.ReadKey();
                                continue;
                            }
                            Money += moneyInput;
                            payments.Add(new Payment(moneyInput, group[nameID]));
                            break;
                        case 1:
                            Console.WriteLine("Внесение мероприятия начато!");
                            Console.Write("Введите название мероприятия: ");
                            string name = Console.ReadLine();
                            Console.Write("Введите сумму списания: ");
                            if (!decimal.TryParse(Console.ReadLine().Replace(".",","), out decimal moneySpent) || moneySpent < 0)
                            {
                                Console.WriteLine("Неверно введёна сумма перевода. Попробуйте снова.");
                                Console.ReadKey();
                                continue;
                            }
                            if (moneySpent > Money)
                            {
                                Console.WriteLine("Потраченная сумма больше чем имеющаяся сумма. Попробуйте позже или потратьте меньше!");
                                Console.ReadKey();
                                continue;
                            }
                            Money -= moneySpent;
                            holidays.Add(new Holiday(name, moneySpent));
                            break;
                        case 2:
                            Console.WriteLine("История платежей:");
                            payments.ForEach(p => Console.WriteLine(p.ToString()));
                            Console.WriteLine($"Всего на счету: {Money} УЕ");
                            Console.WriteLine("\nНажмите на любую кнопку для продолжения");
                            Console.ReadKey();
                            break;
                        case 3:
                            Console.WriteLine("История мероприятий:");
                            holidays.ForEach(h => Console.WriteLine(h.ToString()));
                            Console.WriteLine($"Всего потрачено: {holidays.Sum(x=>x.Spent)} УЕ");
                            Console.WriteLine("\nНажмите на любую кнопку для продолжения");
                            Console.ReadKey();
                            break;
                        case 4:
                            Console.WriteLine("Список группы:");
                            group.ForEach(s => Console.WriteLine(s));
                            Console.WriteLine("\nНажмите на любую кнопку для продолжения");
                            Console.ReadKey();
                            break;
                        case 5:
                            Console.WriteLine("Добавление студента в группы начато!");
                            Console.Write("Введите имя нового студента: ");
                            string studentName = Console.ReadLine();
                            if (group.Contains(studentName))
                            {
                                Console.WriteLine("Такой студент уже добавлен.");
                                Console.ReadKey();
                                continue;
                            }
                            group.Add(studentName);
                            Console.WriteLine("Студент успешно добавлен!");
                            Console.ReadKey();
                            break;
                        case 6:
                            Console.WriteLine("Удаление студента из группы начато!");
                            Console.WriteLine("Выберите студента из списка:");
                            for (int i = 0; i < group.Count; i++)
                            {
                                Console.WriteLine($"[{i}]{group[i]}");
                            }
                            Console.Write("Ответ: ");
                            if (!int.TryParse(Console.ReadLine(), out int nameDeleteID) || nameDeleteID < 0 || nameDeleteID >= group.Count)
                            {
                                Console.WriteLine("Неверно введён номер студента. Попробуйте снова.");
                                Console.ReadKey();
                                continue;
                            }
                            group.RemoveAt(nameDeleteID);
                            Console.WriteLine("Студент успешно удалён!");
                            Console.ReadKey();
                            break;
                        case 7:
                            Console.WriteLine("Спасибо за пользование программой!");
                            alive = false;
                            break;
                    }
                }
            }
        }

        public struct Payment
        {
            private static ulong idgen;
            public ulong ID { get; private set; }
            public DateTime Time { get; private set; }
            public string Payer { get; private set; }
            public decimal Value { get; private set; }

            static Payment() => idgen = 0;

            public Payment(DateTime Time, decimal Value, string Payer)
            {
                ID = idgen++;
                this.Time = Time;
                this.Value = Value;
                this.Payer = Payer;
            }

            public Payment(decimal Value, string Payer)
            {
                ID = idgen++;
                this.Time = DateTime.Now;
                this.Value = Value;
                this.Payer = Payer;
            }

            public override string ToString() => $"[ID:{ID}]Перевод от '{Payer}' ({Value} УЕ) ({Time:dd.MM.yyyy})";
        }

        public struct Holiday
        {
            private static ulong idgen;
            public ulong ID { get; private set; }
            public string Name { get; private set; }
            public DateTime Time { get; private set; }
            public decimal Spent { get; private set; }

            static Holiday() => idgen = 0;

            public Holiday(string Name, DateTime Time, decimal Spent)
            {
                ID = idgen++;
                this.Name = Name;
                this.Time = Time;
                this.Spent = Spent;
            }

            public Holiday(string Name, decimal Spent)
            {
                ID = idgen++;
                this.Name = Name;
                this.Time = DateTime.Now;
                this.Spent = Spent;
            }

            public override string ToString() => $"[ID:{ID}]Мероприятие '{Name}' от {Time:dd.MM.yyyy} (Потрачено:{Spent} УЕ)";
        }
    }
}
