using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Dominio
{
    public class Processamento
    {
        public int Id { get; set; }
        public string Moeda { get; set; }
        public DateTime Data_inicio { get; set; }
        public DateTime Data_fim { get; set; }
        public int Status { get; set; }
    }
}
