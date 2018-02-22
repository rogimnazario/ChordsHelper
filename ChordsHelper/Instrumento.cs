using ChordsHelper.Exibicao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsHelper
{
    public abstract class Instrumento
    {
        protected List<string> Notas = new List<String>() { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

        /// <summary>
        /// Coloca a nota inicial como a primeira da escala.
        /// </summary>
        /// <param name="notaInicial"></param>
        /// <returns></returns>
        public List<string> GirarEscala(string notaInicial)
        {
            notaInicial = notaInicial.ToUpper();

            var index = Notas.IndexOf(notaInicial);
            var novaEscala = new List<string>();

            novaEscala.Add(notaInicial);

            for (int i = 1; i < 12; i++)
            {
                if (index + 1 <= Notas.Count - 1)
                    index++;
                else
                    index = 0;

                novaEscala.Add(Notas[index]);
            }

            return novaEscala;
        }

        public abstract Acorde ExibeAcorde(string notas, ChordsHelper.Enums.AlturaAcordes alturaAcorde = Enums.AlturaAcordes.Randomica);

        public abstract void Reinicializa();
        
    }
}
