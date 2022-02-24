using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_nullName
{
    class Player
    {
        public int Health  { get; set; }
        public int Armory { get; set; }
        public string NamePlayer { get; set; }
        public List<string> Item { get; set; } = new List<string>();

        public void HealthHeal(int Flask) 
        {
            Health = Health + Flask;
            if (Health > 100) 
            {

                Health = 100;

            }
        }

        public void NewItem(string a) 
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine(" = ! = ! = ! = ! = ! = ! = ! = ! = ! = ! = ! = !  = ! = ! = ! = ! = ! = !  = ! = ! = ! = ! = ! = !  ");
            Console.WriteLine($"\n НОВЫЙ ПРЕДМЕТ  - \"{a}\"\n");
            Console.WriteLine(" = ! = ! = ! = ! = ! = ! = ! = ! = ! = ! = ! = !  = ! = ! = ! = ! = ! = !  = ! = ! = ! = ! = ! = !  ");
            Item.Add(a);
            Console.ResetColor();
        }

        public string UseItem() 
        {
            if (Item.Count == 0)
            {
                Console.WriteLine("В вашей сумке ничего нет!");
                return "пусто";
            }

            int resultItem = 0;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(" = ! = ! = ! = ! = ! = ! = ! = ! = ! = ! = ! == СУМКА == ! = ! = ! = ! = ! = !  = ! = ! = ! = ! = ! = !");
            Console.WriteLine("Что вы хотите использовать из этого ?");
            foreach (var item in Item) 
            {
                Console.WriteLine($"{resultItem} - "+item);
                resultItem++;
            }
            Console.WriteLine(" = ! = ! = ! = ! = ! = ! = ! = ! = ! = ! = ! == СУМКА == ! = ! = ! = ! = ! = !  = ! = ! = ! = ! = ! = !");
            Console.ResetColor();
            
            while (true) 
            {
                try
                {
                    int result = int.Parse(Console.ReadLine());
                    if (result <= resultItem)
                    {
                        return Item[result];
                    }
                    else
                    {
                      Console.WriteLine("Предмет не найден!");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Некорректный ввод!!!");
                }
                catch (Exception) 
                {
                    Console.WriteLine("Некорректный ввод!!!");
                }
            }
        }


    }
}
