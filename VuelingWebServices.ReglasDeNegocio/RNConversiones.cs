using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuelingWebServices.ObjetosDeDominio;

namespace VuelingWebServices.ReglasDeNegocio
{
  public class RNConversiones :IRNConversiones
  {
        public IEnumerable<Conversion> ListaCompleta()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Conversion> GetConversionesCruzadas(IEnumerable<Conversion> conversiones, string ModenaDestino)
        {
            IEnumerable<Conversion> resultConversiones = conversiones;
            List<Conversion> nuevasConversiones = new List<Conversion>();

            List<Conversion> conversionesExistentes = conversiones.Where(x => x.to == ModenaDestino && x.from != ModenaDestino).ToList();
            List<Conversion> conversionesFaltantes = conversiones.Where(x => x.to != ModenaDestino && x.from != ModenaDestino).ToList();

            foreach (Conversion item in conversionesFaltantes)
            {
                Conversion match = conversionesExistentes.Where(x => x.from == item.to).FirstOrDefault();
                if (match != null)
                {
                    Conversion NuevaConversion = new Conversion() { from = item.from, to = match.to, rate = (item.rate * match.rate) };

                    if (!conversionesExistentes.Exists(x => x.from == NuevaConversion.from && x.to == NuevaConversion.to))
                        nuevasConversiones.Add(NuevaConversion);
                }
            }

            if (nuevasConversiones.Count != 0)
            {
                resultConversiones = GetConversionesCruzadas(conversiones.Union(nuevasConversiones), ModenaDestino);
            }

            return resultConversiones;

        }
    }
}
