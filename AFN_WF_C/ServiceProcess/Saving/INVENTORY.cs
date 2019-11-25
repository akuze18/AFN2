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
                var codigos = inventario.ByParts(CurPartesId);
                if (!(codigos == null || codigos.Count == 0))
                {
                    res.set(-3, "YA TIENE CODIGOS CREADOS");
                    return res;
                }
                DateTime fecha_compra = lote.purchase_date;
                string raiz = fecha_compra.ToString("yyyyMM") + clase.code;
                //ingresamos articulos
                foreach (var part in CurPartes)
                {
                    //determinamos cantidad ingresada
                    int cantidad = part.quantity;
                    int count_raiz = inventario.GetCorrelativoCodigo(raiz);
                    for (int i = 1; i <= cantidad; i++)
                    {
                        string nuevo_code = raiz + (i + count_raiz).ToString("D7");
                        var nuevo_art = new ARTICLE();
                        nuevo_art.code = nuevo_code;
                        nuevo_art.part_id = part.id;
                        nuevo_art.codigo_old = string.Empty;    //only keep as reference for legacy inventory
                        nuevo_art.ubicacion_id = 0;             //unknown place by default
                        nuevo_art.desde = fecha_compra;
                        //nuevo_art.hasta = maxdate;            //has default value in DB
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
    }
}
