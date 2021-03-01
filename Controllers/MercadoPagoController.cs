using MercadoPago.DataStructures.Preference;
using MercadoPago.Resources;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TiendaEjemplo.Models;

namespace TiendaEjemplo.Controllers
{
    public class MercadoPagoController : Controller
    {

        private RespuestaPagoMP ObtenerRespuesta(HttpRequestBase request)
        {
            RespuestaPagoMP respuesta = new RespuestaPagoMP()
            {
                collection_id = request.QueryString["collection_id"],
                collection_status = request.QueryString["collection_status"],
                external_reference = request.QueryString["external_reference"],
                merchant_account_id = request.QueryString["merchant_account_id"],
                processing_mode = request.QueryString["processing_mode"],
                payment_type = request.QueryString["payment_type"],
                preference_id = request.QueryString["preference_id"],
                site_id = request.QueryString["site_id"]
            };

            return respuesta;

        }

        private string GenerarPreferencia(Telefono producto)
        {
            string _dirSitio = ConfigurationManager.AppSettings["dirSitio"];
            if (MercadoPago.SDK.AccessToken == null)            
            {

                MercadoPago.SDK.AccessToken = "APP_USR-1159009372558727-072921-8d0b9980c7494985a5abd19fbe921a3d-617633181";
                MercadoPago.SDK.IntegratorId = "dev_24c65fb163bf11ea96500242ac130004";
            };
                        

            Preference preference = new Preference();
            MercadoPago.DataStructures.Preference.Address address = new MercadoPago.DataStructures.Preference.Address()
            {
                StreetName = "Insurgentes Sur",
                StreetNumber = 1602,
                ZipCode = "03940"
            };

            MercadoPago.DataStructures.Preference.Payer payer = new MercadoPago.DataStructures.Preference.Payer()
            {
                Name = "Lalo",
                Surname = "Landa",
                Email = "test_user_81131286@testuser.com",
                Address = address
            };

            
            MercadoPago.DataStructures.Preference.BackUrls backurls = new MercadoPago.DataStructures.Preference.BackUrls()
            {
                Success = _dirSitio + "/MercadoPago/Success",
                Failure = _dirSitio + "/MercadoPago/Failure",
                Pending = _dirSitio + "/MercadoPago/Pending"
                
            };
            //preference.NotificationUrl = _dirSitio + "/MercadoPago/Notificacion";
            preference.Payer = payer;
            //preference.SandboxInitPoint = preferencia.SandboxInitPoint;
            //preference.InitPoint = preferencia.InitPoint
            preference.AutoReturn = MercadoPago.Common.AutoReturnType.approved;
            preference.BackUrls = backurls;
            //preference.ExternalReference = preferencia.ExternalReference;
            //preference.NotificationUrl = preferencia.NotificationUrl;
            preference.NotificationUrl = "http://www.muebleriasportillo.com.mx:7412/api/api/MPN";//_dirSitio + "/MercadoPago/Notificacion";
            preference.AdditionalInfo = "CertMPago";

            PaymentMethods paymentMethods = new PaymentMethods()
            {
                Installments = 6
            };


            List<MercadoPago.DataStructures.Preference.PaymentMethod> excludedPaymentMethods = new List<MercadoPago.DataStructures.Preference.PaymentMethod>();
            excludedPaymentMethods.Add(new MercadoPago.DataStructures.Preference.PaymentMethod()
            {
                Id = "amex"
            });
            paymentMethods.ExcludedPaymentMethods = excludedPaymentMethods;

            preference.PaymentMethods = paymentMethods;

            preference.Items.Add(
                new MercadoPago.DataStructures.Preference.Item()
                    {
                        Id = "1234",
                        Title = producto.title,
                        Description =  "Dispositivo móvil de Tienda e-commerce",                        
                        Quantity = 1,
                        CurrencyId = MercadoPago.Common.CurrencyId.MXN,
                        UnitPrice = (decimal)producto.price
                    }
                );
            

            preference.Save();
            //return preference.Id;
            return preference.InitPoint;

        }

        [HttpPost]
        public ActionResult Pagar(Telefono prod)
        {
            MPPreference preferencia = new MPPreference();
            preferencia.InitPoint = GenerarPreferencia(prod);

            return View("Pagar", preferencia);
        }

        [HttpGet]
        public ActionResult Success()
        {
            RespuestaPagoMP response = ObtenerRespuesta(Request);

            return View("Success", response);
        }

        [HttpGet]
        public ActionResult Failure()
        {
            RespuestaPagoMP response = ObtenerRespuesta(Request);

            return View("Failure", response);
        }

        [HttpGet]
        public ActionResult Pending()
        {
            RespuestaPagoMP response = ObtenerRespuesta(Request);

            return View("Pending", response);
        }
        // POST: MercadoPago/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MercadoPago/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MercadoPago/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MercadoPago/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MercadoPago/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        public ActionResult Notificacion(string id, string topic)
        {
            MPNotif notificacion = new MPNotif();
            notificacion.ID = id;
            notificacion.TOPIC = topic;
            

            return new HttpStatusCodeResult(System.Net.HttpStatusCode.OK);
        }
    }
}
