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
        private RespuestaAccion baja_act(int codigo_articulo, int parte_articulo, DateTime newfecha, int newcantidad, string usuario, List<C.DetalleArticulo> detalle_articulos, SV_VALIDATY tipo_baja )
        {
            var res = new RespuestaAccion();
            try
            {
                ACode.Vperiodo prev_periodo = new ACode.Vperiodo(newfecha.Year, newfecha.Month) - 1;
                //valido que parte existe
                SV_PART partToDown = Partes.ByLotePart(codigo_articulo, parte_articulo);
                if (partToDown == null)
                {
                    res.set(-2, "Activo Fijo no puede ser vendido, ya que no existe la parte solicitada");
                    return res;
                }
                //determino si necesito crear una nueva parte para bajar
                if (partToDown.quantity > newcantidad)
                {
                    res = CREATE_NEW_PART_FROM(codigo_articulo, partToDown.id, newcantidad, newfecha);
                    if (res.codigo < 0) return res;
                    int newPartId = res.result_objs[0].id;
                    partToDown = Partes.ById(newPartId);
                }

                //determino cabecera vigente
                SV_TRANSACTION_HEADER headPrev = cabeceras.byPartFechaValid(partToDown.id, newfecha);
                if (headPrev == null)
                {
                    res.set(-3, "Activo Fijo no puede ser dado de baja ("+tipo_baja.name+"), no tiene una transaccion vigente al periodo");
                    return res;
                }

                //crear nueva cabecera de transaccion
                res = REGISTER_DOWNS_HEAD(partToDown.id, newfecha, headPrev, usuario, tipo_baja);
                //TODO: reporto en log hitos
                if (res.codigo < 0) return res;

                var headDown = res.result_objs[0];
                var AllSystems = sistemas.All();
                //agregar valores de detalle y parametros para la cabecera
                foreach (SV_SYSTEM currSys in AllSystems)
                {
                    //compruebo si la transaccion anterior tenía detalle para este ambiente
                    SV_TRANSACTION_DETAIL findDet = detalles.GetByPartSystem(headPrev.id, currSys);
                    if (findDet != null)
                    {
                        //ingreso detalle de transaccion
                        res = REGISTER_DOWNS_DETAIL(headDown.id, currSys.id, tipo_baja);
                        if (res.codigo < 0) return res;

                        if (currSys.ENVIORMENT.depreciation_rate == "daily")
                        {
                            //si deprecio en días (IFRS) debo depreciar solo los dias desde el ultimo cierre
                            var currProc = get_detailed(currSys, prev_periodo.last, codigo_articulo, true, true).FirstOrDefault();
                            if (currProc != null)
                            {
                                //determino los valores de depreciados segun corresponda
                                var depreciado = new DETAIL_DEPRECIATE(currProc, 0, newfecha);
                                //ingreso parametros
                                decimal monto_param;
                                if (depreciado.val_AF_cm != 0)
                                {
                                    SV_PARAMETER currParam = parametros.PrecioBase;
                                    monto_param = depreciado.val_AF_cm / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                                if (depreciado.DA_AF != 0)
                                {
                                    SV_PARAMETER currParam = parametros.DepreciacionAcum;
                                    monto_param = depreciado.DA_AF / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                                if (depreciado.credi_adi_cm != 0)
                                {
                                    SV_PARAMETER currParam = parametros.Credito;
                                    monto_param = depreciado.credi_adi_cm / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                                if (depreciado.deter != 0)
                                {
                                    SV_PARAMETER currParam = parametros.Deterioro;
                                    monto_param = depreciado.deter / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                                if (depreciado.val_res != 0)
                                {
                                    SV_PARAMETER currParam = parametros.ValorResidual;
                                    monto_param = depreciado.val_res / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                                if (depreciado.vu_resi != 0)
                                {
                                    SV_PARAMETER currParam = parametros.VidaUtil;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, depreciado.vu_resi);
                                }
                                if (depreciado.preparacion != 0)
                                {
                                    SV_PARAMETER currParam = parametros.Preparacion;
                                    monto_param = depreciado.preparacion / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                                if (depreciado.transporte != 0)
                                {
                                    SV_PARAMETER currParam = parametros.Transporte;
                                    monto_param = depreciado.transporte / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                                if (depreciado.montaje != 0)
                                {
                                    SV_PARAMETER currParam = parametros.Montaje;
                                    monto_param = depreciado.montaje / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                                if (depreciado.desmantelamiento != 0)
                                {
                                    SV_PARAMETER currParam = parametros.Desmantelamiento;
                                    monto_param = depreciado.desmantelamiento / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                                if (depreciado.honorario != 0)
                                {
                                    SV_PARAMETER currParam = parametros.Honorario;
                                    monto_param = depreciado.honorario / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                                if (depreciado.revalorizacion != 0)
                                {
                                    SV_PARAMETER currParam = parametros.Revalorizacion;
                                    monto_param = depreciado.revalorizacion / depreciado.cantidad;
                                    res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, currParam.id, monto_param);
                                }
                            }
                            else
                            {
                                res.set(-4, "No se encontro información vigente para dar de baja");
                                return res;
                            }
                        }
                        else //if (currSys.ENVIORMENT.depreciation_rate == "monthly")
                        {
                            //copio los detalles de valores anteriores a la nueva cabecera (no aplica ningun calculo adicional)
                            List<SV_TRANSACTION_PARAMETER_DETAIL> findDetailsParams = DetallesParametros.ByHeadSys(headPrev.id, currSys);
                            foreach (var DetParam in findDetailsParams)
                            {
                                //ingreso detalle de parametros
                                res = REGISTER_PARAM_DETAIL(headDown.id, currSys.id, DetParam.paratemer_id, DetParam.parameter_value);
                                if (res.codigo < 0) return res;
                            }
                        }
                    }
                }
                //actualizo detalles de inventarios
                res = ACTUALIZA_PARTES_HASTA(detalle_articulos, partToDown.id, newcantidad, newfecha);
                if (res.codigo < 0) return res;
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        public RespuestaAccion venta_act(int codigo_articulo, int parte_articulo, DateTime newfecha, int newcantidad, string usuario, List<C.DetalleArticulo> detalle_articulos)
        {
            SV_VALIDATY VBaja = Vigencias.VENTA();
            return baja_act(codigo_articulo, parte_articulo, newfecha, newcantidad, usuario, detalle_articulos, VBaja);
        }
        public RespuestaAccion castigo_act(int codigo_articulo, int parte_articulo, DateTime newfecha, int newcantidad, string usuario, List<C.DetalleArticulo> detalle_articulos)
        {
            SV_VALIDATY VBaja = Vigencias.CASTIGO();
            return baja_act(codigo_articulo, parte_articulo, newfecha, newcantidad, usuario, detalle_articulos, VBaja);
        }

        public RespuestaAccion CAMBIO_ZONA(int codigo_articulo, int parte_articulo, DateTime newfecha, GENERIC_VALUE newzona, GENERIC_VALUE newsubzona, int newcantidad, string usuario, List<C.DetalleArticulo> detalle_articulos)
        {
            var res = new RespuestaAccion();
            //TODO: implementar cambio de zona
            try
            {
                //valido que parte existe
                SV_PART partToSell = Partes.ByLotePart(codigo_articulo, parte_articulo);
                if (partToSell == null)
                {
                    res.set(-2, "Activo Fijo no puede ser cambiado, ya que no existe la parte solicitada");
                    return res;
                }
                //determino si necesito crear una nueva parte para vender
                if (partToSell.quantity > newcantidad)
                {
                    res = CREATE_NEW_PART_FROM(codigo_articulo, partToSell.id, newcantidad, newfecha);
                    if (res.codigo < 0) return res;
                    int newPartId = res.result_objs[0].id;
                    partToSell = Partes.ById(newPartId);
                }

                //determino cabecera vigente
                SV_TRANSACTION_HEADER headPrev = cabeceras.byPartFechaValid(partToSell.id, newfecha);
                if (headPrev == null)
                {
                    res.set(-3, "Activo Fijo no puede ser cambiado, no tiene una transaccion vigente al periodo");
                    return res;
                }

                //crear nueva cabecera de transaccion
                res = REGISTER_CHANGE_HEAD(partToSell.id, newfecha, newzona,newsubzona, headPrev, usuario);
                //TODO: reporto en log hitos
                if (res.codigo < 0) return res;
                var headChange = res.result_objs[0];
                
                //copio los detalles anteriores a la nueva cabecera (no aplico ningun calculo adicional)
                List<SV_TRANSACTION_DETAIL> findDetails = detalles.GetByHead(headPrev.id);
                foreach(var Detail in findDetails)
                {
                    //ingreso detalle
                    res = REGISTER_CHANGE_DETAIL(headChange.id, Detail);
                    if (res.codigo < 0) return res;
                }
                //copio los detalles de valores anteriores a la nueva cabecera (no aplica ningun calculo adicional)
                List<SV_TRANSACTION_PARAMETER_DETAIL> findDetailsParams = DetallesParametros.ByHead(headPrev.id);
                foreach (var DetParam in findDetailsParams)
                {
                    //ingreso detalle de parametros
                    res = REGISTER_PARAM_DETAIL(headChange.id, DetParam.system_id, DetParam.paratemer_id, DetParam.parameter_value);
                    if (res.codigo < 0) return res;
                }
                //actualizo detalles de inventarios
                res = ACTUALIZA_PARTES(detalle_articulos, partToSell.id, newcantidad);
                if (res.codigo < 0) return res;
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
