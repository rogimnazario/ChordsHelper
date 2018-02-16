using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsHelper
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();

            string acorde = "G9";
            string acordeMontado = EscalaHelper.MontarAcorde(acorde);
            Console.WriteLine("Montagem do acorde " + acorde + " (" + acordeMontado + ")" + Environment.NewLine);

            while (key.Key != ConsoleKey.End)
            {
                Instrumento cvc = new Cavaquinho();
                Console.WriteLine(cvc.ExibeAcorde(acordeMontado));

                key = Console.ReadKey();
                Console.WriteLine("\n");
            }
        }
    }
}
