using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsHelper.Exibicao
{
    class AcordeCavaquinho : Acorde
    {
        public List<NotaCavaquinho> Notas { get; protected set; }

        public AcordeCavaquinho()
        {
            Notas = new List<NotaCavaquinho>();
        }

        public void Add(NotaCavaquinho nota)
        {
            if (Notas.Any(n => n.numCorda == nota.numCorda))
                throw new Exception("Nota já adicionada no acorde");

            Notas.Add(nota);
        }
        
        public void Add(int numCorda, int posicaoCorda, string nomeNota) 
        { 
            Add(new NotaCavaquinho() { nomeNota = nomeNota, numCorda = numCorda, posicaoCorda = posicaoCorda }); 
        }

        public override string ToString()
        {
            var retorno = new StringBuilder();
            foreach (var item in Notas.OrderBy(o => o.numCorda))
                retorno.AppendLine(intToCorda(item.numCorda) + ": " + item.posicaoCorda + " (" + item.nomeNota + ")");

            return retorno.ToString();
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
        
    }
}
