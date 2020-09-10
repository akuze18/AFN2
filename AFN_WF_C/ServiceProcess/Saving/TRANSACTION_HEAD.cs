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
        private string _ref_change = "TRASPASO";
        private string _ref_error = "ERROR";

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
                    return _ref_sales+"_MIG";
                else
                    return _ref_disposal+"_MIG";
            }
        }

        private void _load_transactions_headers()
        {
            _transactions_headers = new TRANSACTIONS_HEADERS(_context.TRANSACTIONS_HEADERS);
        }

        public int GetNextHeadIndex(int ArticlePartId)
        {
            int nextIndex = (from A in _context.TRANSACTIONS_HEADERS
                             where A.article_part_id == ArticlePartId
                             orderby A.head_index
                             select A.head_index).FirstOrDefault();
            return nextIndex + 1;
        }

        public TRANSACTION_HEADER TRANSACTION_HEAD_NEW(SV_TRANSACTION_HEADER Tprevious, AFN_INVENTARIO source)  //para migracion
        {
            DateTime PurchaseDate = (from a in _context.BATCHS_ARTICLES
                                     join b in _context.PARTS on a.id equals b.article_id
                                     where b.id == Tprevious.article_part_id
                                     select a.purchase_date).First();

            var head = new TRANSACTION_HEADER();
            head.article_part_id = Tprevious.article_part_id;
            head.head_index = GetNextHeadIndex(Tprevious.article_part_id);
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
            head.head_index = GetNextHeadIndex(article_part.id);
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

        public RespuestaAccion REGISTER_PURCHASE_HEAD(GENERIC_VALUE part, DateTime fecha_compra, GENERIC_VALUE zona, GENERIC_VALUE subzona, GENERIC_VALUE clase, GENERIC_VALUE subclase, GENERIC_VALUE categoria, GENERIC_VALUE gestion, string usuario)
        {
            var res = new RespuestaAccion();
            try
            {
                //List<TRANSACTION_HEADER> generados = new List<TRANSACTION_HEADER>();
                TRANSACTION_HEADER nueva_cab = new TRANSACTION_HEADER();
                nueva_cab.article_part_id = part.id;
                nueva_cab.head_index = GetNextHeadIndex(part.id);
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
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        public RespuestaAccion MODIF_PURCHASE_HEAD(SV_PART parte, GENERIC_VALUE zona, GENERIC_VALUE subzona, GENERIC_VALUE subclase, GENERIC_VALUE categoria, GENERIC_VALUE gestion, string usuario)
        {
            var res = new RespuestaAccion();
            //TODO: completar proceso de modificacion de cabecera para compras
            try
            {
                int contar = 0;
                //foreach (SV_PART SPart in parte.Where(p => p.first_date == firstFecha))
                //{
                    TRANSACTION_HEADER ToModif = (from h in _context.TRANSACTIONS_HEADERS
                                                  where h.article_part_id == parte.id &&
                                                  h.trx_ini == parte.first_date
                                                  select h).FirstOrDefault();
                    if (ToModif == null)
                    {
                        res.set(-2, "No existe una cabecera valida para la parte solicitada");
                        return res;
                    }
                    //ToModif.article_part_id   //No se puede cambiar
                    //ToModif.trx_ini           //Por definición no se puede cambiar
                    //ToModif.trx_end           //Por definición no se puede cambiar
                    //ToModif.ref_source        //Por definición no se puede cambiar
                    ToModif.zone_id = zona.id;
                    ToModif.subzone_id = subzona.id;
                    //ToModif.kind_id           //Por definición no se puede cambiar
                    ToModif.subkind_id = subclase.id;
                    ToModif.category_id = categoria.id;
                    ToModif.user_own = usuario;
                    ToModif.manage_id = gestion.id;
                    //ToModif.method_revalue_id //No se cambia durante este proceso
                    contar++;
                    res.result_objs.Add((GENERIC_VALUE)(SV_TRANSACTION_HEADER)ToModif);
                //}
                if (contar > 0)
                {
                    _context.SaveChanges();
                    res.set_ok();
                }
                else
                    res.set(-3, "No existen una parte valida asociada a la compra");
                
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        public RespuestaAccion MODIF_PURCHASE_HEAD_MethodVal(int headId, int method_val)
        {
            var res = new RespuestaAccion();
            try
            {
                var FindHead = (from h in _context.TRANSACTIONS_HEADERS
                                where h.id == headId
                                select h).FirstOrDefault();
                if (FindHead != null)
                {
                    FindHead.method_revalue_id = method_val;
                    _context.SaveChanges();
                }
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        //public RespuestaAccion REGISTER_SALE_HEAD(int parteId, DateTime fechaVenta, SV_TRANSACTION_HEADER prevHead, string userName)
        //{
        //    var Vventa = Vigencias.VENTA();
        //    return REGISTER_DOWNS_HEAD(parteId, fechaVenta, prevHead, userName, Vventa);
        //}
        //public RespuestaAccion REGISTER_DISPOSAL_HEAD(int parteId, DateTime fechaVenta, SV_TRANSACTION_HEADER prevHead, string userName)
        //{
        //    var Vcastigo = Vigencias.CASTIGO();
        //    return REGISTER_DOWNS_HEAD(parteId, fechaVenta, prevHead, userName, Vcastigo);
        //}

        public RespuestaAccion REGISTER_DOWNS_HEAD(int parteId, DateTime fechaBaja, SV_TRANSACTION_HEADER prevHead, string userName, SV_VALIDATY tipo_baja)
        {
            var res = new RespuestaAccion();
            string reference = "";
            switch (tipo_baja.id)
            {
                case 2:
                    reference = _ref_sales;
                    break;
                case 3:
                    reference = _ref_disposal;
                    break;
                default:
                    reference = _ref_error;
                    break;
            }
            
            try
            {
                TRANSACTION_HEADER headDown = new TRANSACTION_HEADER();
                headDown.article_part_id = parteId;
                headDown.head_index = GetNextHeadIndex(parteId);
                headDown.trx_ini = fechaBaja;
                headDown.trx_end = prevHead.trx_end;
                headDown.ref_source = reference;
                headDown.zone_id = prevHead.zone_id;
                headDown.subzone_id = prevHead.subzone_id;
                headDown.kind_id = prevHead.kind_id;
                headDown.subkind_id = prevHead.subkind_id;
                headDown.category_id = prevHead.category_id;
                headDown.user_own = userName;
                headDown.manage_id = prevHead.manage_id;
                headDown.method_revalue_id = prevHead.method_revalue_id;

                TRANSACTION_HEADER _prevHead = (from h in _context.TRANSACTIONS_HEADERS
                                                where h.id == prevHead.id
                                                select h).FirstOrDefault();
                _prevHead.trx_end = fechaBaja;
                _context.TRANSACTIONS_HEADERS.AddObject(headDown);

                _context.SaveChanges();
                res.set_ok();
                res.result_objs.Add((SV_TRANSACTION_HEADER)headDown);

            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }


        public RespuestaAccion REGISTER_CHANGE_HEAD(int parteId, DateTime fechaCambio, GENERIC_VALUE newZona, GENERIC_VALUE newSubzona, SV_TRANSACTION_HEADER prevHead, string userName)
        {
            var res = new RespuestaAccion();

            //TODO: crear ingreso de transaccion de venta
            try
            {
                TRANSACTION_HEADER headChange = new TRANSACTION_HEADER();
                headChange.article_part_id = parteId;
                headChange.head_index = GetNextHeadIndex(parteId);
                headChange.trx_ini = fechaCambio;
                headChange.trx_end = prevHead.trx_end;
                headChange.ref_source = _ref_change;
                headChange.zone_id = newZona.id;
                headChange.subzone_id = newSubzona.id;
                headChange.kind_id = prevHead.kind_id;
                headChange.subkind_id = prevHead.subkind_id;
                headChange.category_id = prevHead.category_id;
                headChange.user_own = userName;
                headChange.manage_id = prevHead.manage_id;
                headChange.method_revalue_id = prevHead.method_revalue_id;

                TRANSACTION_HEADER _prevHead = (from h in _context.TRANSACTIONS_HEADERS
                                                where h.id == prevHead.id
                                                select h).FirstOrDefault();
                _prevHead.trx_end = fechaCambio;
                _context.TRANSACTIONS_HEADERS.AddObject(headChange);

                _context.SaveChanges();
                res.set_ok();
                res.result_objs.Add((SV_TRANSACTION_HEADER)headChange);

            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }
    }
}
