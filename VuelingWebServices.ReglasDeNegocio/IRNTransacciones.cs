using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuelingWebServices.ObjetosDeDominio;

namespace VuelingWebServices.ReglasDeNegocio
{
  public interface IRNTransacciones
  {
        IEnumerable<Transaccion> ListaCompleta();
        ResultTransaccionesConTotal ListaCompletaConTotal(string sku, IEnumerable<Transaccion> transacciones, IEnumerable<Conversion> conversiones);
    }
}
