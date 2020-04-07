using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuelingWebServices.ObjetosDeDominio;

namespace VuelingWebServices.ReglasDeNegocio
{
    public class RNTransacciones : IRNTransacciones
    {
        private readonly IRNConversiones _IRNConversiones;

        public RNTransacciones(IRNConversiones iRNConversiones)
        {
            _IRNConversiones = iRNConversiones;
        }
        
        public IEnumerable<Transaccion> ListaCompleta()
        {
            throw new NotImplementedException();
        }

        public ResultTransaccionesConTotal ListaCompletaConTotal(string sku, IEnumerable<Transaccion> transacciones, IEnumerable<Conversion> conversiones)
        {
            ResultTransaccionesConTotal result = new ResultTransaccionesConTotal();
            IEnumerable<Conversion> conversionesAEuro = new List<Conversion>();

            result.Transacciones = transacciones.Where(x => x.sku == sku).ToList();

            conversionesAEuro = _IRNConversiones.GetConversionesCruzadas(conversiones, "EUR").Where(x=> x.to =="EUR").ToList();

            foreach (Transaccion item in result.Transacciones)
            {
                if (item.currency != "EUR")
                {
                    double rate = conversionesAEuro.Where(x => x.from == item.currency).Select(x => x.rate).FirstOrDefault();
                    item.amount = item.amount * rate;
                    item.amount = Math.Round(item.amount,2, MidpointRounding.ToEven);
                    item.currency = "EUR";
                }
            }

            result.EurTotal = Math.Round(result.Transacciones.Sum(x => x.amount), 2, MidpointRounding.ToEven);

            return result;
        }
    }
}
