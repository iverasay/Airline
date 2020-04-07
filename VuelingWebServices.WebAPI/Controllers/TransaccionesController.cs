using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using VuelingWebServices.ObjetosDeDominio;
using VuelingWebServices.ReglasDeNegocio;
using log4net;

namespace VuelingWebServices.WebAPI.Controllers
{
    
    public class TransaccionesController : ApiController
    {
        private readonly IRNTransacciones _IRNTransacciones;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public TransaccionesController(IRNTransacciones iRNTransacciones)
        {
            _IRNTransacciones = iRNTransacciones;
        }

        [HttpGet]
        public IEnumerable<Transaccion> ListaTransacciones()
        {
            IEnumerable<Transaccion> Transacciones = new List<Transaccion>();
            string JsonTransacciones;
                        
            try
            {
                using (WebClient client = new WebClient())
                {
                    JsonTransacciones = client.DownloadString("http://quiet-stone-2094.herokuapp.com/transactions.json");
                    System.Web.HttpContext.Current.Application["UltimasTransaccionesJson"] = JsonTransacciones;
                }
            }
            catch (WebException e)
            {
                log.Warn(e.GetType().ToString() + ": " + e.Message + " " + e.StackTrace);
            }
            catch (Exception e)
            {
                log.Error(e.GetType().ToString() + ": " + e.Message + "" + e.StackTrace);
            }
            finally
            {
                JsonTransacciones = (string)(System.Web.HttpContext.Current.Application["UltimasTransaccionesJson"]);
            }

            if(JsonTransacciones != null)
                Transacciones = JsonConvert.DeserializeObject<List<Transaccion>>(JsonTransacciones);
            
            return Transacciones;
        }

        [HttpGet]
        public IEnumerable<Conversion> ListaConversiones()
        {
            IEnumerable<Conversion> Conversiones = new List<Conversion>();
            string JsonConversiones;
            try
            {
                using (WebClient client = new WebClient())
                {
                    JsonConversiones = client.DownloadString("http://quiet-stone-2094.herokuapp.com/rates.json");
                    System.Web.HttpContext.Current.Application["UltimasConversionesJson"] = JsonConversiones;
                }
            }
            catch (WebException e)
            {
                log.Warn(e.GetType().ToString() + ": " + e.Message + " " + e.StackTrace);
            }
            catch (Exception e)
            {
                log.Error(e.GetType().ToString() + ": " + e.Message + "" + e.StackTrace);
            }
            finally
            {
                JsonConversiones = (string)(System.Web.HttpContext.Current.Application["UltimasConversionesJson"]);
            }

            if (JsonConversiones != null)
                Conversiones = JsonConvert.DeserializeObject<List<Conversion>>(JsonConversiones);

            return Conversiones;
        }


        [HttpGet]
        public ResultTransaccionesConTotal ListaTransaccionesConTotal(string sku)
        {
            ResultTransaccionesConTotal result = new ResultTransaccionesConTotal();
            IEnumerable<Transaccion> Transacciones = ListaTransacciones();
            IEnumerable<Conversion> Conversiones = ListaConversiones();
            try
            {                
                result = _IRNTransacciones.ListaCompletaConTotal(sku, Transacciones, Conversiones);
            }
            catch (Exception e)
            {
                log.Error(e.GetType().ToString() + ": " + e.Message + "" + e.StackTrace);
            }

            return result;
        }
    }
}
