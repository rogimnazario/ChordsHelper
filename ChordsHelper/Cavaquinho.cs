using ChordsHelper.Exibicao;
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

        public override Acorde ExibeAcorde(string notas, ChordsHelper.Enums.AlturaAcordes alturaAcorde = Enums.AlturaAcordes.Randomica)
        {
            var arrNotas = notas.Split(',').ToList();
            var totalNotas = arrNotas.Count;

            var acorde = new AcordeCavaquinho();

            Reinicializa();

            int index = 0;

            var cloneCordas = new List<string>(Cordas);
            //indo de corda em corda pra montar o acorde.

            while (Cordas.Any() && arrNotas.Any())
            {
                if (!arrNotas.Any()) break;

                index = 0;

                //pega a proxima corda
                var indexCorda = (new Random((int)DateTime.Now.Ticks)).Next(0, Cordas.Count);
                var proximaCorda = Cordas[indexCorda];

                //dar preferencia as cordas diferentes da D quando o acorde for natural (1 3 5), caso alguma D ja tenha saido.
                if (totalNotas < numCordas && acorde.Notas.Any(n => n.numCorda == 0 || n.numCorda == 3) && proximaCorda.ToUpper().Equals("D"))
                    continue;

                //gira a escala pra mesma começar pela nota da em questao
                var Escala = GirarEscala(proximaCorda);

                if (arrNotas.Count > 0)
                {
                    var indexNota = (new Random((int)DateTime.Now.Ticks)).Next(0, arrNotas.Count);

                    while (Escala[index] != arrNotas[indexNota])
                        index++;

                    if ((alturaAcorde == Enums.AlturaAcordes.Baixa && index > 5) ||
                        (alturaAcorde == Enums.AlturaAcordes.Alta && index < 5))
                    {
                        Reinicializa();
                        arrNotas = notas.Split(',').ToList();
                        acorde = new AcordeCavaquinho();

                        continue; 
                    }

                    arrNotas.Remove(arrNotas[indexNota]);
   
                    acorde.Add(cloneCordas.IndexOf(proximaCorda), index, Escala[index]);
                }

                //exclui pra ela nao ser usada mais d uma vez
                Cordas.Remove(proximaCorda);                
            }

            //para o caso do cavaquinho, se o acorde tiver 3 notas (acorde natural), a corda d tem d ser igual a corda D.
            if (totalNotas < numCordas && Cordas.Any(a => a.ToUpper().Equals("D")))
                acorde.Add(new NotaCavaquinho()
                {
                    numCorda = cloneCordas.IndexOf(Cordas.First()),
                    nomeNota = (acorde.Notas.FirstOrDefault(f => f.numCorda == 0) ?? acorde.Notas.FirstOrDefault(f => f.numCorda == 3)).nomeNota,
                    posicaoCorda = (acorde.Notas.FirstOrDefault(f => f.numCorda == 0) ?? acorde.Notas.FirstOrDefault(f => f.numCorda == 3)).posicaoCorda
                });

            return acorde;
        }

    }
}
