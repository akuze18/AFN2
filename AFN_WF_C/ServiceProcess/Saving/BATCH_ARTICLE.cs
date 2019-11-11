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
        private void _load_batches_articles()
        {
            _batches_articles = new BATCHES_ARTICLES(_context.BATCHS_ARTICLES);
        }
        public BATCH_ARTICLE BATCH_ARTICLE_NEW(AFN_LOTE_ARTICULOS lote_old)
        {
            var lote_new = new BATCH_ARTICLE();
            lote_new.aproval_state_id = EstadoAprobacion.ByCode(lote_old.estado_aprov).id;
            lote_new.descrip = lote_old.descripcion;
            lote_new.purchase_date = (DateTime)lote_old.fecha_compra;
            lote_new.initial_price = (decimal)lote_old.precio_inicial;
            lote_new.initial_life_time = (int)lote_old.vida_util_inicial;
            lote_new.account_date = (DateTime)lote_old.fecha_ing;
            lote_new.origin_id = origenes.ByCode(lote_old.origen).id;
            lote_new.type_asset_id = Tipos.ByDescrip(lote_old.consistencia).id;

            //Analizo documento a migrar
            if (lote_old.num_doc != "SIN_DOCUMENTO")
            {
                var new_doc = DOCUMENT_NEW_PREV(lote_old);
                var new_rel = new DOCS_BATCH();
                new_rel.DOCUMENT = new_doc;
                new_rel.BATCHS_ARTICLES = lote_new;

                _context.DOCS_BATCH.AddObject(new_rel);
            }
            else
            {
                //ingreso solo lote, sin documento asociado
                _context.BATCHS_ARTICLES.AddObject(lote_new);
            }
            _context.SaveChanges();
            _load_batches_articles();
            return lote_new;
        }

        public RespuestaAccion BORRAR_AF(int codigo_lote)
        {
            var NewState = EstadoAprobacion.DELETE;
            return ChangeStateAF(codigo_lote, NewState);
        }
        public RespuestaAccion ACTIVAR_AF(int codigo_lote)
        {
            var NewState = EstadoAprobacion.CLOSE;
            return ChangeStateAF(codigo_lote, NewState);
        }
        private RespuestaAccion ChangeStateAF(int codigo_lote, GENERIC_VALUE StateSet)
        {
            var respuesta = new RespuestaAccion();
            try
            {
                BATCH_ARTICLE lote = (from b in _context.BATCHS_ARTICLES
                                      where b.id == codigo_lote
                                      select b)
                                     .First();
                if (lote.origin_id == 2)    //OBC
                {
                    ASSET_IN_PROGRESS_HEAD[] salidas = (from aph in _context.ASSETS_IN_PROGRESS_HEAD
                                                        where aph.batch_id == codigo_lote
                                                        select aph).ToArray();
                    foreach (var salida in salidas)
                    {
                        salida.aproval_state_id = StateSet.id;
                    }
                }
                lote.aproval_state_id = StateSet.id;
                _context.SaveChanges();
                respuesta.set_ok();
            }
            catch (Exception e)
            {
                respuesta.set(-1, e.StackTrace);
            }

            return respuesta;
        }
    }
}
