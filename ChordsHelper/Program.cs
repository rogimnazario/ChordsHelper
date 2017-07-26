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
                var chords = new Chords();
                var cvc = new Cavaquinho();

                Console.WriteLine(cvc.ExibeAcorde(chords.MontarAcorde("C7")));

                key = Console.ReadKey();
                Console.WriteLine("\n");
            }
        }
    }
}
