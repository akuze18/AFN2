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
        public static string _InOBC = "E";
        public static string _OutOBC = "S";

        public RespuestaAccion INGRESO_OBC(DateTime fechaIngreso, int zonaId, string proveedorId, string docNumber, string glosaIngreso, decimal montoOriginal, DateTime fechaContable)
        {
            var res = new RespuestaAccion();
            try
            {
                var OBCHeadNew = new ASSET_IN_PROGRESS_HEAD();
                OBCHeadNew.tipo = _InOBC;
                OBCHeadNew.trx_date = fechaIngreso;
                OBCHeadNew.zone_id = zonaId;
                OBCHeadNew.descrip = glosaIngreso;
                OBCHeadNew.entrada_id = null;
                OBCHeadNew.batch_id = null;
                OBCHeadNew.post_date = fechaContable;
                OBCHeadNew.aproval_state_id = EstadoAprobacion.CLOSE.id;
                _context.ASSETS_IN_PROGRESS_HEAD.AddObject(OBCHeadNew);

                //reviso detalle ingreso
                var OBCDetailCLPNew = new ASSET_IN_PROGRESS_DETAIL();
                OBCDetailCLPNew.ASSET_IN_PROGRESS_HEAD = OBCHeadNew;
                OBCDetailCLPNew.currency_id = Monedas.CLP.id;
                OBCDetailCLPNew.amount = montoOriginal;
                _context.ASSETS_IN_PROGRESS_DETAIL.AddObject(OBCDetailCLPNew);

                var OBCDetailYENNew = new ASSET_IN_PROGRESS_DETAIL();
                OBCDetailYENNew.ASSET_IN_PROGRESS_HEAD = OBCHeadNew;
                OBCDetailYENNew.currency_id = Monedas.YEN.id;
                OBCDetailYENNew.amount = Math.Round(montoOriginal / TipoCambio.YEN(fechaIngreso),0);
                _context.ASSETS_IN_PROGRESS_DETAIL.AddObject(OBCDetailYENNew);

                //reviso documento asociado
                var findDoc = documentos.ByNumProv(docNumber, proveedorId);
                if (findDoc == null)
                {
                    var docNew = new DOCUMENT();
                    docNew.docnumber = docNumber;
                    docNew.comment = string.Empty;
                    docNew.proveedor_id = proveedorId;
                    docNew.proveedor_name = Proveedor.getNameByCode(proveedorId);

                    var relatNew = new DOCS_OBC();
                    relatNew.ASSETS_IN_PROGRESS_HEAD = OBCHeadNew;
                    relatNew.DOCUMENT = docNew;
                    _context.DOCS_OBC.AddObject(relatNew);
                }
                else
                {
                    var relatNew = new DOCS_OBC();
                    relatNew.ASSETS_IN_PROGRESS_HEAD = OBCHeadNew;
                    relatNew.document_id = findDoc.id;
                    _context.DOCS_OBC.AddObject(relatNew);
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

        public RespuestaAccion EGRESO_OBC(int codEntrada, int codSalida, decimal montoOriginal, int zonaId)
        {
            var res = new RespuestaAccion();
            try
            {
                //VALIDO QUE LA ENTRADA CORRESPONDA
                var findEntrada = ObrasConstruccion.IngresoById(codEntrada);
                if (findEntrada == null)
                {
                    res.set(-2, "CODIGO ENTRADA NO CORRESPONDE");
                    return res;
                }
                //VALIDO QUE LA SALIDA CORRESPONDA
                var findBatch = lotes.ById(codSalida);
                if(findBatch == null)
                {
                    res.set(-2, "CODIGO SALIDA NO CORRESPONDE");
                    return res;
                }
                string glosaEgreso = "MOVIMIENTO AL ACTIVO FIJO";


                var OBCHeadNew = new ASSET_IN_PROGRESS_HEAD();
                OBCHeadNew.tipo = _OutOBC;
                OBCHeadNew.trx_date = findBatch.purchase_date;
                OBCHeadNew.zone_id = zonaId;
                OBCHeadNew.descrip = glosaEgreso;
                OBCHeadNew.entrada_id = findEntrada.id;
                OBCHeadNew.batch_id = findBatch.id;
                OBCHeadNew.post_date = findBatch.account_date;
                OBCHeadNew.aproval_state_id = EstadoAprobacion.CLOSE.id;
                _context.ASSETS_IN_PROGRESS_HEAD.AddObject(OBCHeadNew);

                //reviso detalle egreso
                decimal DispCLP, montoYen, TotalCLP, TotalYen;
                DispCLP = ObrasConstruccion.SaldoDisponible(findEntrada.id, Monedas.CLP);
                
                if (DispCLP > montoOriginal)
                {
                    TotalCLP = findEntrada.TotalByCurrency(Monedas.CLP);
                    TotalYen = findEntrada.TotalByCurrency(Monedas.YEN);
                    //montoOriginal = montoOriginal;
                    montoYen = Math.Round(TotalYen * montoOriginal / TotalCLP, 0);
                }
                else
                {
                    montoOriginal = DispCLP;
                    montoYen = ObrasConstruccion.SaldoDisponible(findEntrada.id, Monedas.YEN);
                }   

                var OBCDetailCLPNew = new ASSET_IN_PROGRESS_DETAIL();
                OBCDetailCLPNew.ASSET_IN_PROGRESS_HEAD = OBCHeadNew;
                OBCDetailCLPNew.currency_id = Monedas.CLP.id;
                OBCDetailCLPNew.amount = montoOriginal;
                _context.ASSETS_IN_PROGRESS_DETAIL.AddObject(OBCDetailCLPNew);
                
                var OBCDetailYENNew = new ASSET_IN_PROGRESS_DETAIL();
                OBCDetailYENNew.ASSET_IN_PROGRESS_HEAD = OBCHeadNew;
                OBCDetailYENNew.currency_id = Monedas.YEN.id;
                OBCDetailYENNew.amount = montoYen;
                _context.ASSETS_IN_PROGRESS_DETAIL.AddObject(OBCDetailYENNew);

                _context.SaveChanges();

                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        public RespuestaAccion EGRESO_GASTO(int? id, int codigoEntrada, DateTime fechaSalida, decimal montoOriginal, int aprovalId)
        {
            //TODO: Proceso de egreso hacia gastos
            var res = new RespuestaAccion();
            try
            {
                //VALIDO QUE LA ENTRADA CORRESPONDA
                var findEntrada = ObrasConstruccion.IngresoById(codigoEntrada);
                if (findEntrada == null)
                {
                    res.set(-2, "CODIGO ENTRADA NO CORRESPONDE");
                    return res;
                }
                string glosaEgreso = "MOVIMIENTO AL GASTO";

                if (id == null)
                {
                    var OBCHeadNew = new ASSET_IN_PROGRESS_HEAD();
                    OBCHeadNew.tipo = _OutOBC;
                    OBCHeadNew.trx_date = fechaSalida;
                    OBCHeadNew.zone_id = findEntrada.zone.id;
                    OBCHeadNew.descrip = glosaEgreso;
                    OBCHeadNew.entrada_id = findEntrada.id;
                    OBCHeadNew.batch_id = null;
                    OBCHeadNew.post_date = fechaSalida;
                    OBCHeadNew.aproval_state_id = aprovalId;
                    _context.ASSETS_IN_PROGRESS_HEAD.AddObject(OBCHeadNew);

                    //reviso detalle egreso
                    decimal DispCLP, montoYen, TotalCLP, TotalYen;
                    DispCLP = ObrasConstruccion.SaldoDisponible(findEntrada.id, Monedas.CLP);

                    if (DispCLP > montoOriginal)
                    {
                        TotalCLP = findEntrada.TotalByCurrency(Monedas.CLP);
                        TotalYen = findEntrada.TotalByCurrency(Monedas.YEN);
                        //montoOriginal = montoOriginal;
                        montoYen = Math.Round(TotalYen * montoOriginal / TotalCLP, 0);
                    }
                    else
                    {
                        montoOriginal = DispCLP;
                        montoYen = ObrasConstruccion.SaldoDisponible(findEntrada.id, Monedas.YEN);
                    }

                    var OBCDetailCLPNew = new ASSET_IN_PROGRESS_DETAIL();
                    OBCDetailCLPNew.ASSET_IN_PROGRESS_HEAD = OBCHeadNew;
                    OBCDetailCLPNew.currency_id = Monedas.CLP.id;
                    OBCDetailCLPNew.amount = montoOriginal;
                    _context.ASSETS_IN_PROGRESS_DETAIL.AddObject(OBCDetailCLPNew);

                    var OBCDetailYENNew = new ASSET_IN_PROGRESS_DETAIL();
                    OBCDetailYENNew.ASSET_IN_PROGRESS_HEAD = OBCHeadNew;
                    OBCDetailYENNew.currency_id = Monedas.YEN.id;
                    OBCDetailYENNew.amount = montoYen;
                    _context.ASSETS_IN_PROGRESS_DETAIL.AddObject(OBCDetailYENNew);
                }
                else
                {
                    var findSalida = ObrasConstruccion.EgresoById((int)id);
                    if (findSalida == null)
                    {
                        res.set(-3, "CODIGO SALIDA NO CORRESPONDE");
                        return res;
                    }

                    var OBCHeadEdit = (from a in _context.ASSETS_IN_PROGRESS_HEAD
                                        where a.id == findSalida.id
                                        select  a).First();
                    OBCHeadEdit.tipo = _OutOBC;
                    OBCHeadEdit.trx_date = fechaSalida;
                    OBCHeadEdit.zone_id = findEntrada.zone.id;
                    OBCHeadEdit.descrip = glosaEgreso;
                    OBCHeadEdit.entrada_id = findEntrada.id;
                    OBCHeadEdit.batch_id = null;
                    OBCHeadEdit.post_date = fechaSalida;
                    OBCHeadEdit.aproval_state_id = aprovalId;

                    //reviso detalle egreso
                    decimal DispCLP, montoYen, TotalCLP, TotalYen;
                    DispCLP = ObrasConstruccion.SaldoDisponible(findEntrada.id, Monedas.CLP);

                    if (DispCLP > montoOriginal)
                    {
                        TotalCLP = findEntrada.TotalByCurrency(Monedas.CLP);
                        TotalYen = findEntrada.TotalByCurrency(Monedas.YEN);
                        //montoOriginal = montoOriginal;
                        montoYen = Math.Round(TotalYen * montoOriginal / TotalCLP, 0);
                    }
                    else
                    {
                        montoOriginal = DispCLP;
                        montoYen = ObrasConstruccion.SaldoDisponible(findEntrada.id, Monedas.YEN);
                    }

                    var OBCDetailCLPEdit = (from d in _context.ASSETS_IN_PROGRESS_DETAIL
                                            where d.head_id == OBCHeadEdit.id &&
                                            d.currency_id == Monedas.CLP.id
                                            select d).First();
                    OBCDetailCLPEdit.amount = montoOriginal;

                    var OBCDetailYENEdit = (from d in _context.ASSETS_IN_PROGRESS_DETAIL
                                            where d.head_id == OBCHeadEdit.id &&
                                            d.currency_id == Monedas.YEN.id
                                            select d).First();
                    OBCDetailYENEdit.amount = montoYen;
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
