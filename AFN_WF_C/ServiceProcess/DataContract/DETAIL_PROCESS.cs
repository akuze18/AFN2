using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using V = AFN_WF_C.ServiceProcess.DataView;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public class DETAIL_PROCESS
    {

        public V.SV_SYSTEM sistema { get; set; }
        public int cod_articulo { get; set; }           //BATCHS_ARTICLES
        public int parte { get; set; }                  //PARTS
        public DateTime fecha_inicio { get; set; }      //TRANSACTIONS_HEADERS
        public DateTime fecha_fin { get; set; }         //TRANSACTIONS_HEADERS
        public GENERIC_VALUE zona { get; set; }                  //TRANSACTIONS_HEADERS
        public GENERIC_VALUE vigencia { get; set; }            //TRANSACTIONS_DETAILS
        public int cantidad { get; set; }               //PARTS
        public GENERIC_VALUE clase { get; set; }                 //TRANSACTIONS_HEADERS
        public GENERIC_VALUE categoria { get; set; }         //TRANSACTIONS_HEADERS
        public GENERIC_VALUE subzona { get; set; }            //TRANSACTIONS_HEADERS
        public GENERIC_VALUE subclase { get; set; }           //TRANSACTIONS_HEADERS
        public GENERIC_VALUE gestion { get; set; }         //TRANSACTIONS_HEADERS
        public string usuario { get; set; }             //TRANSACTIONS_HEADERS
        public bool se_deprecia { get; set; }           //TRANSACTIONS_DETAILS
        public GENERIC_VALUE aprobacion { get; set; }       //BATCHS_ARTICLES
        public string dscrp { get; set; }               //BATCHS_ARTICLES
        public string dsc_extra { get; set; }           //FUNCION ESPECIAL
        public DateTime fecha_compra { get; set; }      //BATCHS_ARTICLES

        public List<V.SV_DOCUMENT> documentos { get; set; }    //proveedor + num_doc
        public decimal precio_inicial { get; set; }     //BATCHS_ARTICLES
        public int vida_util_inicial { get; set; }      //BATCHS_ARTICLES
        public bool derecho_credito { get; set; }     //TRANSACTIONS_DETAILS
        public DateTime fecha_ing { get; set; }         //BATCHS_ARTICLES
        public GENERIC_VALUE origen { get; set; }              //BATCHS_ARTICLES
        public GENERIC_VALUE tipo { get; set; }                //BATCHS_ARTICLES

        //parametros
        public LIST_PARAM_VALUE parametros { get; set; }

        public int PartId { get; set; }
        public int HeadId { get; set; }
        public string RefSource { get; set; }
    }
}
