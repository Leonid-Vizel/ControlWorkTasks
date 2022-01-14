using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Zadacha_Remont
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Work> works = new List<Work>();
            bool alive = true;
            while (alive)
            {
                Console.Clear();
                Console.WriteLine("Добро пожаловать в менеджер ремонта!\nВыберите опцию:");
                Console.WriteLine("0 - Добавить услугу");
                Console.WriteLine("1 - Убрать услугу");
                Console.WriteLine("2 - Получить чек");
                Console.WriteLine("3 - Выход");
                Console.Write("Ответ: ");
                if (!int.TryParse(Console.ReadLine(), out int answer) || answer < 0 || answer > 3)
                {
                    continue;
                }
                switch (answer)
                {
                    case 0:
                        works.Add(new Work());
                        break;
                    case 1:
                        if (works.Count == 0)
                        {
                            continue;
                        }
                        for (int i = 0; i < works.Count; i++)
                        {
                            Console.WriteLine($"[{i}]{works[i]}");
                        }
                        Console.Write("Введите номер удаляемой услуги: ");
                        if (!int.TryParse(Console.ReadLine(), out int answerDelete) || answerDelete < 0 || answerDelete >= works.Count)
                        {
                            continue;
                        }
                        works.RemoveAt(answerDelete);
                        break;
                    case 2:
                        Console.WriteLine("Список работ со стоимостями:");
                        var sortedWorks = works.OrderBy(x => x.Price);
                        foreach(Work work in sortedWorks)
                        {
                            Console.WriteLine($"{work}\t{work.Price} УЕ");
                        }
                        Console.WriteLine($"Итоговая стоимость: {works.Sum(x=>x.Price)} УЕ");
                        Console.WriteLine("Нажмите на любую кнопку для продолжения");
                        Console.ReadKey();
                        break;
                    case 3:
                        alive = false;
                        break;
                }
            }
        }

        public class Work
        {
            public Room room;
            public ServiceCeiling service1 = (ServiceCeiling)(-1);
            public ServiceFloor service2 = (ServiceFloor)(-1);
            public ServiceWalls service3 = (ServiceWalls)(-1);

            public decimal Price => (int)service1 * 1000 + (int)service2 * 1512 + (int)service3 * 2000;

            public override string ToString()
            {
                StringBuilder builder = new StringBuilder($"{room} (");

                switch(service1)
                {
                    case ServiceCeiling.Сeiling1:
                        builder.Append("Натяжной потолок,");
                        break;
                    case ServiceCeiling.Сeiling2:
                        builder.Append("Покрашенный потолок,");
                        break;
                }

                switch (service2)
                {
                    case ServiceFloor.Floor1:
                        builder.Append("Линолеум,");
                        break;
                    case ServiceFloor.Floor2:
                        builder.Append("Ламинат,");
                        break;
                    case ServiceFloor.Floor3:
                        builder.Append("Паркет,");
                        break;
                    case ServiceFloor.Floor4:
                        builder.Append("Плитка,");
                        break;
                }

                switch (service3)
                {
                    case ServiceWalls.Walls1:
                        builder.Append("Обои");
                        break;
                    case ServiceWalls.Walls2:
                        builder.Append("Штукатурка");
                        break;
                    case ServiceWalls.Walls3:
                        builder.Append("Плитка");
                        break;
                    case ServiceWalls.Walls4:
                        if (room == Room.Balcony)
                        {
                            builder.Append("Кирпичные стены");
                        }
                        break;
                }
                builder.Append(")");
                return builder.ToString();
            }

            public Work(Room room, ServiceCeiling service1, ServiceFloor service2, ServiceWalls service3)
            {
                this.room = room;
                this.service1 = service1;
                this.service2 = service2;
                this.service3 = service3;
            }

            public Work() => Ask();

            public void Ask()
            {
                #region Выбор комнаты
                Console.WriteLine("В какой комнате Вы хотите провести ремонт?");
                Console.WriteLine("0 - Спальня");
                Console.WriteLine("1 - Гостинная");
                Console.WriteLine("2 - Ванная");
                Console.WriteLine("3 - Туалет");
                Console.WriteLine("4 - Кухня");
                Console.WriteLine("5 - Балкон");
                bool flag = Room.TryParse(Console.ReadLine(), out room) && (int)room >= 0 && (int)room <= 5;
                if (!flag)
                {
                    Console.WriteLine("Неверно введена комната. Попробуйте снова!");
                    Ask();
                    return;
                }
                #endregion
                #region Потолок
                Console.WriteLine("Хотите ли в этой комнате сделать ремонт потолка?");
                Console.WriteLine("0 - Не хочу");
                Console.WriteLine("1 - Хочу натяжной потолок");
                Console.WriteLine("2 - Хочу покрашенный потолок");
                flag = ServiceCeiling.TryParse(Console.ReadLine(), out service1) && (int)service1 >= 0 && (int)service1 <= 2;
                if (!flag)
                {
                    Console.WriteLine("Услуга была введена неверно. Попробуйте снова!");
                    Ask();
                    return;
                }
                #endregion
                #region Пол
                Console.WriteLine("Хотите ли в этой комнате сделать ремонт пола?");
                Console.WriteLine("0 - Не хочу");
                Console.WriteLine("1 - Хочу Линолеум");
                Console.WriteLine("2 - Хочу Ламинат");
                Console.WriteLine("3 - Хочу Паркет");
                Console.WriteLine("4 - Хочу Плитку");
                flag = ServiceFloor.TryParse(Console.ReadLine(), out service2) && (int)service2 >= 0 && (int)service2 <= 4;
                if (!flag)
                {
                    Console.WriteLine("Услуга была введена неверно. Попробуйте снова!");
                    Ask();
                    return;
                }
                #endregion
                #region Стены
                Console.WriteLine("Хотите ли в этой комнате сделать ремонт стен?");
                Console.WriteLine("0 - Не хочу");
                Console.WriteLine("1 - Хочу Обои");
                Console.WriteLine("2 - Хочу Штукатурку");
                Console.WriteLine("3 - Хочу Плитку");
                if (room == Room.Balcony)
                {
                    Console.WriteLine("4 - Хочу Кирпич");
                }
                flag = ServiceWalls.TryParse(Console.ReadLine(), out service3) && (int)service3 >= 0 && (((int)service3 <= 4 && room == Room.Balcony) || ((int)service3 <= 3 && room != Room.Balcony));
                if (!flag)
                {
                    Console.WriteLine("Услуга была введена неверно. Попробуйте снова!");
                    Ask();
                    return;
                }
                #endregion
            }
        }

        public enum Room
        {
            SleepingRoom,
            LivingRoom,
            BathRoom,
            Toilet,
            Kitchen,
            Balcony
        }

        public enum ServiceCeiling
        {
            Nothing, //Ремонт потолка не требуется
            Сeiling1, //Натяжной потолок
            Сeiling2, //Покрашенный потолок
        }

        public enum ServiceFloor
        {
            Nothing, //Ремонт пола не требуется
            Floor1, //Линолеум
            Floor2, //Ламинат
            Floor3, //Паркет
            Floor4, //Плитка
        }

        public enum ServiceWalls
        {
            Nothing, //Ремонт стен не требуется
            Walls1, //Обои
            Walls2, //Штукатурка
            Walls3, //Плитка
            Walls4, //Кирпич - только для балкона
        }
    }
}
