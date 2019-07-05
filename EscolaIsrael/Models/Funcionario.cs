using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscolaIsrael.Models
{
    public class Funcionario
    {
        public long? FuncionarioID { get; set; }
        public string Nome { get; set; }
        public string Cargo { get; set; }
        public string Endereço { get; set; }
    }
}
