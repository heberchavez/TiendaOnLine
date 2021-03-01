using MercadoPago.Common;
using MercadoPago.DataStructures.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TiendaEjemplo.Models
{
    public class RespuestaPagoMP
    {
        public RespuestaPagoMP() { }
        public string collection_id { get; set; }// =[PAYMENT_ID] 
        public string collection_status { get; set; } //= approved 
        public string external_reference { get; set; }// =[EXTERNAL_REFERENCE] 
        public string payment_type { get; set; }// = credit_card 
        public string merchant_order_id { get; set; }
        public string preference_id { get; set; } // =[PREFERENCE_ID] 
        public string site_id { get; set; }// =[SITE_ID] 
        public string processing_mode { get; set; }// = aggregator 
        public string merchant_account_id { get; set; }// = null

    }

    public class MPPreference
    {
        public string Id { get; set; }
        public string InitPoint { get; set; }
    }

    public class MPNotif
    {        
        public int FOLIO { get; set; }
        public string ID { get; set; }
        public string TOPIC { get; set; }

    }
}