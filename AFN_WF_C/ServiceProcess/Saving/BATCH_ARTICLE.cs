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
            if (lote_old.num_doc != DOCUMENTS.defaultDocument)
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

        public RespuestaAccion INGRESO_LOTE(string descripcion, DateTime fecha_compra, string cod_proveedor, string documento, decimal total_compra, int vida_util, bool derecho_credito, DateTime fecha_contab, int origen_id, GENERIC_VALUE CtiPo)
        {
            var respuesta = new RespuestaAccion();
            try
            {
                //check document
                var find_doc = documentos.ByNumProv(documento, cod_proveedor);
                var lote_rel_doc = new DOCS_BATCH();
                if (find_doc == null)
                {
                    //no existe documento asociado, lo creamos
                    var nuevo_documento = new DOCUMENT();
                    nuevo_documento.docnumber = documento;
                    nuevo_documento.comment = string.Empty;
                    nuevo_documento.proveedor_id = cod_proveedor;
                    nuevo_documento.proveedor_name = Proveedor.getNameByCode(cod_proveedor);
                    lote_rel_doc.DOCUMENT = nuevo_documento;
                }
                else
                {
                    //ya existe, asi que usamos ese mismo
                    lote_rel_doc.document_id = find_doc.id;
                }

                BATCH_ARTICLE nuevo_lote = new BATCH_ARTICLE();
                nuevo_lote.aproval_state_id = EstadoAprobacion.OPEN.id;
                nuevo_lote.descrip = descripcion;
                nuevo_lote.purchase_date = fecha_compra;
                nuevo_lote.initial_price = total_compra;
                nuevo_lote.initial_life_time = vida_util;
                nuevo_lote.account_date = fecha_contab;
                nuevo_lote.origin_id = origen_id;
                nuevo_lote.type_asset_id = CtiPo.id;

                lote_rel_doc.BATCHS_ARTICLES = nuevo_lote;

                _context.DOCS_BATCH.AddObject(lote_rel_doc);
                _context.BATCHS_ARTICLES.AddObject(nuevo_lote);

                _context.SaveChanges();

                respuesta.set_ok();
                respuesta.result_objs.Add((SV_BATCH_ARTICLE)nuevo_lote);
            }
            catch (Exception e)
            {
                respuesta.set(-1, e.StackTrace);
            }
            return respuesta;
        }

        public RespuestaAccion MODIFICA_LOTE(int batch_id, string descripcion, string cod_proveedor, string documento, decimal total_compra, int vida_util, DateTime fecha_contab)
        {
            //TODO: Completar proceso para modificar un lote
            var res = new RespuestaAccion();
            try
            {
                BATCH_ARTICLE ToModif = (from c in _context.BATCHS_ARTICLES
                                         where c.id == batch_id
                                         select c).FirstOrDefault();
                if (ToModif == null)
                {
                    res.set(-2, "Codigo de lote a modificar no existe");
                    return res;
                }
                ToModif.descrip = descripcion;
                ToModif.initial_price = total_compra;
                ToModif.initial_life_time = vida_util;
                ToModif.account_date = fecha_contab;
                //ToModif.purchase_date //por definición no puede modificarse
                //ToModif.origin_id //por definición no puede modificarse
                //ToModif.type_asset_id //por definición no puede modificarse

                //Reviso documento
                if (documento != DOCUMENTS.defaultDocument && cod_proveedor != DOCUMENTS.defaultProveed)
                {
                    DOCUMENT DocModif = (from d in _context.DOCUMENTS
                                         where d.docnumber == documento && d.proveedor_id == cod_proveedor
                                         select d).FirstOrDefault();
                    if (DocModif == null)
                    {
                        //no existe el documento, se debe crear y asociar
                        DOCUMENT DocNuevo = new DOCUMENT();
                        DocNuevo.proveedor_id = cod_proveedor;
                        DocNuevo.docnumber = documento;
                        DocNuevo.comment = string.Empty;
                        DocNuevo.proveedor_name = Proveedor.getNameByCode(cod_proveedor);

                        //reviso asociaciones previas del lote
                        var allowRelations = (from rl in _context.DOCS_BATCH
                                              where rl.batch_id == batch_id
                                              select rl).FirstOrDefault();
                        if (allowRelations == null)
                        {
                            var relation = new DOCS_BATCH();
                            relation.BATCHS_ARTICLES = ToModif;
                            relation.DOCUMENT = DocNuevo;
                            _context.DOCS_BATCH.AddObject(relation);
                        }
                        else
                        {
                            //documento existe, se debe actualizar la asociacion
                            allowRelations.DOCUMENT = DocNuevo;
                        }

                        
                    }
                    else
                    {
                        //reviso asociaciones previas
                        var allowRelations = (from rl in _context.DOCS_BATCH
                                              where rl.batch_id == batch_id
                                              select rl).FirstOrDefault();
                        if (allowRelations == null)
                        {
                            //documento existe y debe asociar al batch
                            var relation = new DOCS_BATCH();
                            relation.BATCHS_ARTICLES = ToModif;
                            relation.DOCUMENT = DocModif;
                        }
                        else
                        {
                            //documento existe, se debe actualizar la asociacion
                            allowRelations.DOCUMENT = DocModif;
                        }
                        
                    }
                }
                _context.SaveChanges();

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
