﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsHelper
{
    public static class EscalaHelper
    {
        static List<string> Notas = new List<String>() { "C", "C#", "D", "D#", "E", "F", "F#", "G", "G#", "A", "A#", "B" };
        const int TOM = 2;
        const int SEMITOM = 1;

        private static IEnumerable<string> Escala { get; set; }

        public static string PrintEscala(string nota)
        {
            Escala = GerarEscala(nota);

            var result = string.Empty;

            foreach (var item in Escala)
                result += (result != string.Empty ? " " : string.Empty) + item;

            return result;
        }

        private static IEnumerable<string> GerarEscala(string nota, bool somenteNota = true)
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

        /// <summary>
        /// Retorna as notas de um dado acorde.
        /// </summary>
        /// <param name="acorde">Qual o acorde deseja-se obter as notas.</param>
        /// <returns></returns>
        public static string MontarAcorde(string acorde)
        {
            //acorde maior: 1 3 5
            var eAcordeMaior = !acorde.Contains('m');

            var escala = GerarEscala(string.Concat(acorde[0], (acorde.Count() > 1 && acorde[1] == '#') ? acorde[1].ToString() : string.Empty), true).ToList();

            var retorno = string.Empty;

            if (eAcordeMaior)
            {
                /*
                    Exemplo em Dó diminuto:
                    Primeiro grau: C
                    Terceiro grau menor: Eb
                    Quinto grau diminuto: Gb
                    Sétimo grau diminuto: A (ou Bbb)
                 */
                retorno = escala[0];

                if (acorde.Contains("°") || acorde.Contains("dim"))
                    retorno += "," + BaixaSemitom(escala[2]) + "," + BaixaSemitom(escala[4]) + "," + escala[5];
                else
                    retorno += "," + escala[acorde.Contains("4") ? 3 : 2] + "," + escala[4];
            }

            //Sétima
            if (acorde.Contains("7"))
            {
                if (acorde.Contains("7+")) //Sétima maior
                    retorno += "," + escala[6];
                else //Sétima menor
                    retorno += "," + BaixaSemitom(escala[6]);
            }
            else if (char.IsNumber(acorde.Last())) // acorde com 9, 6
            {
                var numero = Convert.ToInt32(acorde.Last().ToString());
                if (numero > 7)
                    numero = numero - 7;

                retorno += "," + escala[numero - 1];
            }  

            return retorno;
        }

        private static string BaixaSemitom(string nota)
        {
            var index = Notas.IndexOf(nota);

            return (index - 1 >= 0)
                ? Notas[index - 1]
                : Notas[Notas.Count - 1];
        }

        private static string AumentaSemitom(string nota)
        {
            var index = Notas.IndexOf(nota);

            return  (index + 1 <= Notas.Count - 1)
                ? Notas[index + 1]            
                : Notas[0];
        }

        private static string BaixaTom(string nota)
        {
            return BaixaSemitom(BaixaSemitom(nota));
        }

        private static string AumentaTom(string nota)
        {
            return AumentaSemitom(AumentaSemitom(nota));
        }
    }

    
}
