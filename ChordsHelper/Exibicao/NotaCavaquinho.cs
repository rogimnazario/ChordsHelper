using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsHelper.Exibicao
{
    public class NotaCavaquinho : Nota
    {
        public int numCorda { get; set; }
        public int posicaoCorda { get; set; }
        public string nomeNota { get; set; }

    }
}
