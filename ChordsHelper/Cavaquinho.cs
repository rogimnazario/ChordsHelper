using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsHelper
{
    public class Cavaquinho : Instrumento
    {
        public List<string> Cordas { get; set; }

        public const int numCordas = 4;

        public Cavaquinho()
        {
            Reinicializa();
        }

        public override void Reinicializa()
        {
            Cordas = new List<string>() { "d", "G", "B", "D" };
        }

        private string intToCorda(int intCorda)
        {
            switch (intCorda)
            {
                case 0:
                    return "Rezona";
                case 1:
                    return "Sol";
                case 2:
                    return "Si";
                case 3:
                    return "Rezinha";
                default:
                    throw new Exception("Essa corda não existe no cavaquinho.");
            }
        }

        public override string ExibeAcorde(string notas)
        {
            var arrNotas = notas.Split(',').ToList();

            var dicTotal = new Dictionary<int, Tuple<int, string>>();

            Reinicializa();

            int index = 0;
            var retorno = string.Empty;

            var cloneCordas = new List<string>();
            cloneCordas.AddRange(Cordas);
            //indo de corda em corda pra montar o acorde.

            //1. rezinha
            for (int i = 0; i < numCordas; i++)
            {
                //pega a proxima corda
                var indexCorda = (new Random(DateTime.Now.Millisecond)).Next(0, Cordas.Count);
                var proximaCorda = Cordas[indexCorda];

                //exclui pra ela nao ser usada mais d uma vez
                Cordas.Remove(proximaCorda);

                //gira a escala pra mesma começar pela nota da em questao
                var Escala = GirarEscala(proximaCorda);

                if (arrNotas.Count > 0)
                {
                    var indexNota = (new Random(DateTime.Now.Millisecond)).Next(0, arrNotas.Count);

                    while (Escala[index] != arrNotas[indexNota])
                        index++;                    

                    arrNotas.Remove(arrNotas[indexNota]);

                    //retorno = (!string.IsNullOrEmpty(retorno) ? retorno + "\n" : string.Empty) + string.Format("Corda {0} : {1}", proximaCorda, index);

                    dicTotal.Add(cloneCordas.IndexOf(proximaCorda), new Tuple<int, string>(index, Escala[index]));
                    
                }

                index = 0;
            }

            foreach (var item in dicTotal.OrderBy(k => k.Key))
                retorno = string.Join("\n", retorno, intToCorda(item.Key) + ": " + item.Value.Item1 + " ("+item.Value.Item2+")");                
            
            return retorno;
        }

    }
}
