using System;
using System.Collections.Generic;
using System.Linq;

namespace ZadachaEasy_Bar
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string owner = "Abobus Abobusovich";
            PivoBar pivo1 = new PivoBar(owner);
            PivoBar pivo2 = new PivoBar(owner);
            PivoBar pivo3 = new PivoBar("BAR", "Abiba");
            pivo1.drinks.Add(new Drink("pivo", 100, true));
            Console.WriteLine(pivo1.ToString());
            Console.WriteLine(pivo1.Equals(pivo2));
            Console.WriteLine(pivo1.Equals(pivo3));
            pivo1.GetPrice(pivo1.drinks[0], 5);
        }
    }

    public class PivoBar
    {
        private static int idgen;
        public readonly int id;
        public string name;
        public readonly string owner;
        public List<Drink> drinks;

        static PivoBar() => idgen = 0;

        public PivoBar(string name, string owner)
        {
            id = idgen++;
            this.owner = owner;
            this.name = name;
            drinks = new List<Drink>();
        }

        public PivoBar(string owner)
        {
            id = idgen++;
            this.owner = owner;
            name = $"{owner}'s pivobar";
            drinks = new List<Drink>();
        }

        public void GetPrice(Drink drink, int amount)
        {
            if (!drinks.Contains(drink))
            {
                Console.WriteLine("Данный напиток не продаётся в этом баре!");
            }
            else
            {
                Console.WriteLine($"Стоимость {amount}x'{drink.name}': {drink.price*amount} УЕ");
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is PivoBar))
            {
                //условие 'obj is not PivoBar' может не работать на более ранних версиях языка
                return false;
            }
            else
            {
                PivoBar other = (PivoBar)obj;
                return other.name.Equals(name) && other.owner.Equals(owner);
            }
        }

        public override int GetHashCode() => (name, owner).GetHashCode();

        public override string ToString() => $"Бар #{id}\nНаименование:'{name}'\nВладелец:'{owner}'\nЗдесь доступны напитки:\n{string.Join("\n",drinks.Select(x=>x.ToString()))}";
    }

    public struct Drink
    {
        public string name;
        public decimal price;
        public bool alcohol;

        public Drink(string name, decimal price, bool alcohol)
        {
            this.name = name;
            this.price = price;
            this.alcohol = alcohol;
        }

        public override string ToString()
        {
            if (alcohol)
            {
                return $"{name} ({price} УЕ) (Алкогольный)";
            }
            else
            {
                return $"{name} ({price} УЕ) (Безалкогольный)";
            }
        }
    }
}
