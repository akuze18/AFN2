using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using V = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class DETAIL_PROCESS
    {
        public DateTime fecha_proceso { get; set; }     //Fecha para la que es valido este proceso

        public int cod_articulo { get; set; }           //BATCHS_ARTICLES (id) //PARTS(article_id)
        public SV_APROVAL_STATE aprobacion { get; set; }//BATCHS_ARTICLES (approval_state_id)
        public string dscrp { get; set; }               //BATCHS_ARTICLES (descrip)
        public string dsc_extra { get; set; }           //FUNCION ESPECIAL
        public DateTime fecha_compra { get; set; }      //BATCHS_ARTICLES (purchase_date)
        public decimal precio_inicial { get; set; }     //BATCHS_ARTICLES (initial_price)
        public int vida_util_inicial { get; set; }      //BATCHS_ARTICLES (inital_life_time)
        public DateTime fecha_ing { get; set; }         //BATCHS_ARTICLES (account_date)
        public SV_ORIGIN origen { get; set; }           //BATCHS_ARTICLES (origin_id)
        public SV_TYPE_ASSET tipo { get; set; }         //BATCHS_ARTICLES (type_asset_id)

        public int PartId { get; set; }                 //PARTS (id) //TRANSACTIONS_HEADERS (article_part_id)
        public int parte { get; set; }                  //PARTS (part_index)
        public int cantidad { get; set; }               //PARTS (quantity)
        public DateTime PrimeraFecha { get; set; }      //PARTS (first_date)

        public int HeadId { get; set; }                 //TRANSACTIONS_HEADERS (id) //TRANSACTIONS_DETAILS (trx_head_id)
        public DateTime fecha_inicio { get; set; }      //TRANSACTIONS_HEADERS (trx_ini)
        public DateTime fecha_fin { get; set; }         //TRANSACTIONS_HEADERS (trx_end)
        public SV_ZONE zona { get; set; }               //TRANSACTIONS_HEADERS (zone_id)
        public SV_KIND clase { get; set; }              //TRANSACTIONS_HEADERS (kind_id)
        public SV_CATEGORY categoria { get; set; }      //TRANSACTIONS_HEADERS (category_id)
        public SV_SUBZONE subzona { get; set; }         //TRANSACTIONS_HEADERS (subzone_id)
        public SV_SUBKIND subclase { get; set; }        //TRANSACTIONS_HEADERS (subkind_id)
        public SV_MANAGEMENT gestion { get; set; }      //TRANSACTIONS_HEADERS (manage_id)
        public string usuario { get; set; }             //TRANSACTIONS_HEADERS (user_own)
        public string RefSource { get; set; }           //TRANSACTIONS_HEADERS (ref_source)
        public SV_METHOD_REVALUE metodo_reval { get; set; } //TRANSACTIONS_HEADERS (method_revalue_id)

        public int DetailId { get; set; }               //TRANSACTIONS_DETAILS (id)
        public SV_SYSTEM sistema { get; set; }          //TRANSACTIONS_DETAILS (system_id)
        public SV_VALIDATY vigencia { get; set; }       //TRANSACTIONS_DETAILS (validity_id)
        public bool se_deprecia { get; set; }           //TRANSACTIONS_DETAILS (depreciate)
        public bool derecho_credito { get; set; }       //TRANSACTIONS_DETAILS (allow_credit)

        //dbo.DOCUMENTS + dbo.DOCS_BATCH
        public List<SV_DOCUMENT> documentos { get; set; }    //proveedor + num_doc

        //TRANSACTIONS_PARAMETERS_DETAILS collection
        public LIST_PARAM_VALUE parametros { get; set; }

        //SCHEMA sales
        public decimal precio_venta { get; set; }
    }
}
