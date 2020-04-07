using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VuelingWebServices.ObjetosDeDominio;

namespace VuelingWebServices.ReglasDeNegocio
{
    public interface IRNConversiones
    {
        IEnumerable<Conversion> ListaCompleta();
        IEnumerable<Conversion> GetConversionesCruzadas(IEnumerable<Conversion> conversiones, string ModenaDestino);
    }
}
