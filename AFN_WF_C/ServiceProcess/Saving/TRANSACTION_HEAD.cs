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
        private string ref_source(DateTime WorkingDate, AFN_INVENTARIO curr_fin_clp, DateTime PurchaseDate)
        {
            if (WorkingDate == PurchaseDate)
                return "PURCHASE";
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

        public TRANSACTION_HEADER TRANSACTION_HEAD_NEW(SV_TRANSACTION_HEADER Tprevious, AFN_INVENTARIO source)
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
        public TRANSACTION_HEADER TRANSACTION_HEAD_NEW(SV_PART article_part, AFN_INVENTARIO source)
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
    }
}
