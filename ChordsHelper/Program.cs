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
            var acordeStr = Console.ReadLine().ToUpper();
            string acordeMontado = EscalaHelper.MontarAcorde(acordeStr);
            Console.WriteLine("Montagem do acorde " + acordeStr + " (" + acordeMontado + ")" + Environment.NewLine);

            var cvc = new Cavaquinho();

            while (!string.IsNullOrEmpty(acordeStr))
            {                
                var acorde = cvc.ExibeAcorde(acordeMontado, Enums.AlturaAcordes.Randomica);

                Console.WriteLine(acorde.ToString());

                acordeStr = Console.ReadLine().ToUpper();
                acordeMontado = EscalaHelper.MontarAcorde(acordeStr);
                Console.WriteLine("Montagem do acorde " + acordeStr + " (" + acordeMontado + ")" + Environment.NewLine);
            }
        }
    }
}
