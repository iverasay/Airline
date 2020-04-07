using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VuelingWebServices.ObjetosDeDominio
{
    public class ResultTransaccionesConTotal
    {
        public double EurTotal { get; set; }
        public IEnumerable<Transaccion> Transacciones { get; set; }
    }
}
