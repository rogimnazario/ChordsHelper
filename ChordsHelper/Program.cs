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

            while (key.Key != ConsoleKey.End)
            {
                
                Instrumento cvc = new Cavaquinho();

                Console.WriteLine(cvc.ExibeAcorde(EscalaHelper.MontarAcorde("C7")));

                key = Console.ReadKey();
                Console.WriteLine("\n");
            }
        }
    }
}
