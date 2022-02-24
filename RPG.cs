using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RPG_Artyom_gaci
{
   

    static class RPG
    {
        public static void Why(string[] a) 
        { 
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("*~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~*");
            for (int i = 0; i < a.Length; i++)
            {
                Console.WriteLine($"\n{i+1}. - {a[i]}\n");
            }
            
            Console.WriteLine("*~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~* *~*");
            Console.ResetColor();
        }
        public static int AnswerPlayer(int g, int b) 
        {
            
            bool a = true;
            
            while (a)
            {
                try
                {
                    int task = g;
                    if (task > b)
                    {
                        Console.WriteLine("Такого варианта не предусмотрено");
                        continue;
                    }
                    a = false;
                    return task;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Некорректный символ, попробуйте еще раз!");
                    continue;

                }
                catch (Exception)
                {
                    continue;
                }
                
            }
            return 0;
        }
    }
}
