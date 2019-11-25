using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public partial class Main
    {
        #region Default Values

        private DateTime _final_date = new DateTime(9999,12,31);
        private string _ref_purchase = "PURCHASE";
        private string _ref_sales = "VENTA";
        private string _ref_disposal = "CASTIGO";

        #endregion
        private string ref_source(DateTime WorkingDate, AFN_INVENTARIO curr_fin_clp, DateTime PurchaseDate)
        {
            if (WorkingDate == PurchaseDate)
                return _ref_purchase;
            else
            {
                if (curr_fin_clp.cod_estado == 1)
                {
                    if (curr_fin_clp.ingresado_por.Contains("SALDO_INICIAL"))
                        return "MIG";
                    else
                        return "TRASPASO_MIG";
                }
                else if (curr_fin_clp.cod_estado == 2)
                    return "VENTA_MIG";
                else
                    return "CASTIGO_MIG";
            }
        }

        private void _load_transactions_headers()
        {
            _transactions_headers = new TRANSACTIONS_HEADERS(_context.TRANSACTIONS_HEADERS);
        }


        public TRANSACTION_HEADER TRANSACTION_HEAD_NEW(SV_TRANSACTION_HEADER Tprevious, AFN_INVENTARIO source)  //para migracion
        {
            DateTime PurchaseDate = (from a in _context.BATCHS_ARTICLES
                                     join b in _context.PARTS on a.id equals b.article_id
                                     where b.id == Tprevious.article_part_id
                                     select a.purchase_date).First();

            var head = new TRANSACTION_HEADER();
            head.article_part_id = Tprevious.article_part_id;
            head.trx_ini = source.fecha_inicio;
            head.trx_end = Tprevious.trx_end;
            head.ref_source = ref_source(source.fecha_inicio,source,PurchaseDate);
            head.zone_id = zonas.ByCode(source.zona).id;
            head.subzone_id = (int)source.subzona + 1;
            head.kind_id = Clases.ByCode(source.clase).id;
            head.subkind_id = subclases.ByCode(source.subclase).id;
            head.category_id = categorias.ByCode(source.categoria).id;
            head.user_own = source.ingresado_por;
            int cGest;
            if (Int32.TryParse(source.gestion.ToString(), out cGest))
            { head.manage_id = cGest; }
            else { head.manage_id = null; }
            head.method_revalue_id = 1;
            _context.TRANSACTIONS_HEADERS.AddObject(head);
            //actualizo la fecha fin de la cabecera previa
            var prev_head = (from h in _context.TRANSACTIONS_HEADERS
                             where h.id == Tprevious.id
                             select h).First();
            prev_head.trx_end = source.fecha_inicio;

            _context.SaveChanges();
            if (head.id == 0) 
                _context.ObjectStateManager.ChangeObjectState(head, System.Data.EntityState.Added);

            _load_transactions_headers();

            return head;
        }
        public TRANSACTION_HEADER TRANSACTION_HEAD_NEW(SV_PART article_part, AFN_INVENTARIO source) //para migracion
        {
            DateTime PurchaseDate = (from a in _context.BATCHS_ARTICLES
                                     join b in _context.PARTS on a.id equals b.article_id
                                     where b.id == article_part.id
                                     select a.purchase_date).First();

            var head = new TRANSACTION_HEADER();
            head.article_part_id = article_part.id;
            head.trx_ini = source.fecha_inicio;
            head.trx_end = (DateTime)source.fecha_fin;
            head.ref_source = ref_source(source.fecha_inicio, source, PurchaseDate);
            head.zone_id = zonas.ByCode(source.zona).id;
            head.subzone_id = (int)source.subzona + 1;
            head.kind_id = Clases.ByCode(source.clase).id;
            head.subkind_id = subclases.ByCode(source.subclase).id;
            head.category_id = categorias.ByCode(source.categoria).id;
            head.user_own = source.ingresado_por;
            head.method_revalue_id = 1;
            int cGest;
            if (Int32.TryParse(source.gestion.ToString(), out cGest))
            { head.manage_id = cGest; }
            else { head.manage_id = null; }
            _context.TRANSACTIONS_HEADERS.AddObject(head);

            _context.SaveChanges();
            if (head.id == 0)
                _context.ObjectStateManager.ChangeObjectState(head, System.Data.EntityState.Added);

            _load_transactions_headers();

            return head;
        }

        public RespuestaAccion REGISTER_PURCHASE_HEAD(List<GENERIC_VALUE> parts, DateTime fecha_compra, GENERIC_VALUE zona, GENERIC_VALUE subzona, GENERIC_VALUE clase, GENERIC_VALUE subclase, GENERIC_VALUE categoria, GENERIC_VALUE gestion, string usuario)
        {
            var res = new RespuestaAccion();
            try
            {
                //List<TRANSACTION_HEADER> generados = new List<TRANSACTION_HEADER>();
                foreach (var part in parts)
                {
                    TRANSACTION_HEADER nueva_cab = new TRANSACTION_HEADER();
                    nueva_cab.article_part_id = part.id;
                    nueva_cab.trx_ini = fecha_compra;
                    nueva_cab.trx_end = _final_date;
                    nueva_cab.ref_source = _ref_purchase;
                    nueva_cab.zone_id = zona.id;
                    nueva_cab.subzone_id = subzona.id;
                    nueva_cab.kind_id = clase.id;
                    nueva_cab.subkind_id = subclase.id;
                    nueva_cab.category_id = categoria.id;
                    nueva_cab.user_own = usuario;
                    nueva_cab.manage_id = gestion.id;
                    nueva_cab.method_revalue_id = 1;    //por defecto será 1, luego el usuario podrá indicar el definitivo

                    //generados.Add(nueva_cab);
                    _context.TRANSACTIONS_HEADERS.AddObject(nueva_cab);
                    _context.SaveChanges();

                    res.result_objs.Add((SV_TRANSACTION_HEADER)nueva_cab);
                }
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        public RespuestaAccion MODIF_PURCHASE_HEAD(List<SV_PART> partes, GENERIC_VALUE zona, GENERIC_VALUE subzona, GENERIC_VALUE subclase, GENERIC_VALUE categoria, GENERIC_VALUE gestion, string usuario)
        {
            var res = new RespuestaAccion();
            //TODO: completar proceso de modificacion de cabecera para compras

            try
            {
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }
    }
}
