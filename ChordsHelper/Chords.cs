using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsHelper
{
    public class Chords
    {
        List<string> Notas = new List<String>() { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        const int TOM = 2;
        const int SEMITOM = 1;

        private IEnumerable<string> Escala { get; set; }



        public string PrintEscala(string nota)
        {
            Escala = GerarEscala(nota);

            var result = string.Empty;
            Escala.ToList().ForEach(e => result += (result != string.Empty ? " " : string.Empty) + e);
            return result;
        }

        private IEnumerable<string> GerarEscala(string nota, bool somenteNota = true)
        {
            Func<bool, int, bool> tomSemitom = (maior, i) =>
            {
                return (maior)
                    ? (i == 1 || i == 2 || i == 4 || i == 5 || i == 6)
                    : (i == 1 || i == 3 || i == 4 || i == 6);
            };

            var threatedNota = string.Empty;

            foreach (var item in nota)
            {
                if (item == 'm')
                    break;

                threatedNota += item;
            }

            var indexNota = Notas.IndexOf(threatedNota.ToString());

            var retorno = new List<string>();

            retorno.Add(nota); //tonica

            var escalaMaior = !nota.Contains('m');   
                
            for (int i = 1; i <= 6; i++)
            {
                indexNota += (tomSemitom(escalaMaior, i)) ? TOM : SEMITOM;

                var sufixo = string.Empty;

                if (!somenteNota)
                    if (escalaMaior)
                    {
                        if (i == 1 || i == 2 || i == 5)
                            sufixo = "m";
                        else if (i == 6)
                            sufixo = "°";
                    }
                    else
                    {
                        if (i == 3 || i == 4)
                            sufixo = "m";
                        else if (i == 1)
                            sufixo = "°";
                    }

                retorno.Add((((Notas.Count - 1) >= indexNota) 
                    ? Notas[indexNota]
                    : Notas[indexNota - Notas.Count()])
                    + sufixo);                    
            }

            //retorno = retorno.Select();

            return retorno;
        }

        public string MontarAcorde(string acorde)
        {
            //acorde maior: 1 3 5
            var eAcordeMaior = !acorde.Contains('m');

            var escala = GerarEscala(string.Concat(acorde[0], (acorde.Count() > 1 && acorde[1] == '#') ? acorde[1].ToString() : string.Empty), true).ToList();

            var retorno = string.Empty;

            if (eAcordeMaior)
            {
                if (acorde.Contains("°") || acorde.Contains("dim"))
                    retorno += escala[0] + "," + BaixaSemitom(escala[2]) + "," + BaixaSemitom(escala[4]);
                else
                    retorno += escala[0] + "," + escala[2] + "," + escala[4];
            }

            //Sétima
            if (acorde.Contains("7"))
                retorno += "," + BaixaSemitom(escala[6]);

            return retorno;
        }

        private string BaixaSemitom(string nota)
        {
            var index = Notas.IndexOf(nota);

            return (index - 1 >= 0)
                ? Notas[index - 1]
                : Notas[Notas.Count - 1];
        }

        private string AumentaSemitom(string nota)
        {
            var index = Notas.IndexOf(nota);

            return  (index + 1 <= Notas.Count - 1)
                ? Notas[index + 1]            
                : Notas[0];
        }
    }

    public class Cavaquinho
    {
        public string Rezinha { get; set; }
        public string Si { get; set; }
        public string Sol { get; set; }
        public string Rezona { get; set; }

        List<string> Notas = new List<String>() { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };

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

        public List<string> GirarEscala(string notaInicial)
        {
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

        public string ExibeAcorde(string notas)
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
