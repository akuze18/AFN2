using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

using C = AFN_WF_C.PCClient.Procesos.Estructuras;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public partial class Main
    {
        public RespuestaAccion GENERAR_CODIGO(int batch_id, GENERIC_VALUE clase)
        {
            var res = new RespuestaAccion();
            try
            {
                //Comprobamos existencia del lote a crearle codigos
                var lote = lotes.ById(batch_id);
                if (lote == null)
                {
                    res.set(-2, "NO EXISTE LOTE");
                    return res;
                }
                //Comprobamos existencia de codigos ya creados para el lote
                var CurPartes = Partes.ByLote(batch_id);
                var CurPartesId = CurPartes.Select(l => l.id).ToArray();
                var codigos = inv_articulos.ByParts(CurPartesId);
                if (!(codigos == null || codigos.Count == 0))
                {
                    res.set(-3, "YA TIENE CODIGOS CREADOS");
                    return res;
                }
                DateTime fecha_compra = lote.purchase_date;
                string raiz = fecha_compra.ToString("yyyyMM") + clase.code.Trim();
                //ingresamos articulos
                foreach (var part in CurPartes)
                {
                    //determinamos cantidad ingresada
                    int cantidad = part.quantity;
                    int count_raiz = inv_articulos.GetCorrelativoCodigo(raiz);
                    for (int i = 1; i <= cantidad; i++)
                    {
                        string nuevo_code = raiz + (i + count_raiz).ToString("D7");
                        var nuevo_art = new ARTICLE();
                        nuevo_art.code = nuevo_code;
                        nuevo_art.part_id = part.id;
                        nuevo_art.codigo_old = string.Empty;    //only keep as reference for legacy inventory
                        nuevo_art.ubicacion_id = 0;             //unknown place by default
                        nuevo_art.desde = fecha_compra;
                        nuevo_art.hasta = _final_date;            //has default value in DB
                        _context.ARTICLES.AddObject(nuevo_art);
                        _context.SaveChanges();
                        
                        res.result_objs.Add(nuevo_art);
                    }
                }

                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        public RespuestaAccion ACTUALIZA_PARTES_HASTA(List<C.DetalleArticulo> detalle_articulos,int partToSellId, int newcantidad, DateTime newfecha)
        {
            var res = new RespuestaAccion();
            try
            {
                //checkeo cantidades
                var aBajar = detalle_articulos.Where(d => d.procesar).ToList();
                if (aBajar.Count != newcantidad)
                {
                    res.set(-5, "Cantidad del detalle no coincide con la indicada a bajada");
                    return res;
                }
                //actualizo detalles de inventarios
                foreach (var det in aBajar)
                {
                    var art_baja = (from a in _context.ARTICLES
                                    where a.id == det.rowId
                                    select a).FirstOrDefault();
                    art_baja.part_id = partToSellId;    //actulizo la parte asociada, en caso que se haya aperturado
                    art_baja.hasta = newfecha;
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
        public RespuestaAccion ACTUALIZA_PARTES(List<C.DetalleArticulo> detalle_articulos, int partToSellId, int newcantidad)
        {
            var res = new RespuestaAccion();
            try
            {
                //checkeo cantidades
                var aBajar = detalle_articulos.Where(d => d.procesar).ToList();
                if (aBajar.Count != newcantidad)
                {
                    res.set(-5, "Cantidad del detalle no coincide con la indicada a cambiar");
                    return res;
                }
                bool actualizar = false;
                //actualizo detalles de inventarios
                foreach (var det in aBajar)
                {
                    var art_baja = (from a in _context.ARTICLES
                                    where a.id == det.rowId
                                    select a).FirstOrDefault();
                    if (art_baja.part_id != partToSellId)
                    {
                        art_baja.part_id = partToSellId;    //actulizo la parte asociada, en caso que se haya aperturado
                        actualizar = true;
                    }
                }
                if(actualizar)
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
