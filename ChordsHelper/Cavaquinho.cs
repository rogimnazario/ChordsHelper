using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsHelper
{
    public class Cavaquinho : Instrumento
    {
        public string Rezinha { get; set; }
        public string Si { get; set; }
        public string Sol { get; set; }
        public string Rezona { get; set; }

        public Cavaquinho()
        {
            Reinicializa();
        }

        private void Reinicializa()
        {
            Rezona = Rezinha = "D";
            Si = "B";
            Sol = "G";
        }

        private string intToCorda(int intCorda)
        {
            switch (intCorda)
            {
                case 0:
                    return "Rezinha";
                case 1:
                    return "Sol";
                case 2:
                    return "Si";
                case 3:
                    return "Rezona";
                default:
                    return string.Empty;
            }
        }

        public override string ExibeAcorde(string notas)
        {
            var arrNotas = notas.Split(',').ToList();

            Reinicializa();

            var Cordas = new List<string>() { Rezinha, Sol, Si, Rezona };
            int index = 0;
            var retorno = string.Empty;
            //indo de corda em corda pra montar o acorde.

            //1. rezinha
            for (int i = 0; i < 4; i++)
            {
                //pega a proxima corda
                var indexCorda = (new Random(DateTime.Now.Millisecond)).Next(0, 4 - i);
                var proximaCorda = Cordas[indexCorda];

                //exclui pra ela nao ser usada mais d uma vez
                Cordas.Remove(proximaCorda);

                //gira a escala pra mesma começar pela nota da em questao
                var Escala = GirarEscala(proximaCorda);

                if (arrNotas.Count > 0)
                {
                    while (Escala[index] != arrNotas[0])
                        index++;

                    arrNotas.Remove(arrNotas[0]);

                    retorno = (!string.IsNullOrEmpty(retorno) ? retorno + "\n" : string.Empty) + string.Format("Corda {0} : {1}", proximaCorda, index);
                }

                index = 0;
            }

            return retorno;
        }

    }
}
