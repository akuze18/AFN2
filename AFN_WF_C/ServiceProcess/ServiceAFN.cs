using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AFN_WF_C.ServiceProcess.DataContract;
using System.Data.Objects.DataClasses;

using Newtonsoft.Json;
using ACode;

namespace AFN_WF_C.ServiceProcess
{
    public class ServiceAFN
    {
        #region Helpers
        private HelperServiceAFN.ZONA _Zonas;
        private HelperServiceAFN.ESTADO_APROVACION _EstadoAprovacion;
        private HelperServiceAFN.SISTEMA _Sistemas;
        private HelperServiceAFN.CORRECCION_MONETARIA _CorrMon;
        private HelperServiceAFN.CLASE _Clases;
        private HelperServiceAFN.VIGENCIA _Vigencias;
        private HelperServiceAFN.PARTE _Partes;
        private HelperServiceAFN.CABECERA _Cabeceras;
        private HelperServiceAFN.DETALLE_PARAMETRO _Det_Param;
        private HelperServiceAFN.TIPO _Tipos;
        private HelperServiceAFN.SITUACION _Situaciones;
        private HelperServiceAFN.MONEDA _Monedas;

        public HelperServiceAFN.ZONA Zonas {
            get
            {
                if (_Zonas == null)
                    _Zonas = new HelperServiceAFN.ZONA();
                return _Zonas;
            }
        }
        public HelperServiceAFN.ESTADO_APROVACION EstadoAprovacion {
            get {
                if (_EstadoAprovacion == null)
                    _EstadoAprovacion = new HelperServiceAFN.ESTADO_APROVACION();
                return _EstadoAprovacion;
            }
        }
        public HelperServiceAFN.SISTEMA Sistemas
        {
            get
            {
                if (_Sistemas == null)
                    _Sistemas = new HelperServiceAFN.SISTEMA();
                return _Sistemas;
            }
        }
        public HelperServiceAFN.CORRECCION_MONETARIA CorreccionMonetaria {
            get
            {
                if (_CorrMon == null)
                    _CorrMon = new HelperServiceAFN.CORRECCION_MONETARIA();
                return _CorrMon;
            }
        }
        public HelperServiceAFN.CLASE Clases{
            get{
                if (_Clases == null)
                    _Clases = new HelperServiceAFN.CLASE();
                return _Clases;
            }
        }
        public HelperServiceAFN.VIGENCIA Vigencias {
            get
            {
                if (_Vigencias == null)
                    _Vigencias = new HelperServiceAFN.VIGENCIA();
                return _Vigencias;
            }
        }
        public HelperServiceAFN.TIPO Tipos
        {
            get
            {
                if (_Tipos == null)
                    _Tipos = new HelperServiceAFN.TIPO();
                return _Tipos;
            }
        }
        public HelperServiceAFN.SITUACION Situaciones
        {
            get
            {
                if (_Situaciones == null)
                    _Situaciones = new HelperServiceAFN.SITUACION();
                return _Situaciones;
            }
        }
        public HelperServiceAFN.MONEDA Monedas
        {
            get
            {
                if (_Monedas == null)
                    _Monedas = new HelperServiceAFN.MONEDA();
                return _Monedas;
            }
        }

        public HelperServiceAFN.PARTE Partes
        {
            get
            {
                if (_Partes == null)
                    _Partes = new HelperServiceAFN.PARTE();
                return _Partes;
            }
        }
        public HelperServiceAFN.CABECERA Cabeceras
        {
            get
            {
                if (_Cabeceras == null)
                    _Cabeceras = new HelperServiceAFN.CABECERA();
                return _Cabeceras;
            }
        }
        public HelperServiceAFN.DETALLE_PARAMETRO DetallesParametros
        {
            get
            {
                if (_Det_Param == null)
                    _Det_Param = new HelperServiceAFN.DETALLE_PARAMETRO();
                return _Det_Param;
            }
        }
        

        #endregion
        
        private Migration _migration;
        public Migration Migracion
        {
            get
            {
                if (_migration == null) _migration = new Migration();
                return _migration; 
            }
        }

        public List<DETAIL_PROCESS> buscar_Articulo(DateTime desde, DateTime hasta, int codigo, string descrip, string zona, int[] vigencias, string[] aprovados)
        {
            using (AFN2Entities context = new AFN2Entities())
            using (var repo = new Repositories.Main(context))
            {
                DateTime current = DateTime.Now;
                SYSTEM sis = repo.sistemas.Default;

                List<DETAIL_PROCESS> consulta;
                
                if (codigo > 0)
                {
                    if (aprovados != null && aprovados.Length != 0)
                    {
                        consulta = repo.get_detailed(sis, current, codigo, aprovados);
                    }
                    else
                    {
                        consulta = repo.get_detailed(sis, current, codigo);
                    }
                }
                else {
                    if (aprovados != null && aprovados.Length != 0)
                    {
                        consulta = repo.get_detailed(sis, current, aprovados);
                    }
                    else
                    {
                        consulta = repo.get_detailed(sis, current);
                    }
                }
                
                //filtro fecha compra
                consulta = consulta.Where(x => x.fecha_compra >= desde && x.fecha_compra <= hasta).ToList();
                //filtro vigencia (Vigente, Castivo o Venta)
                if (vigencias != null && vigencias.Length != 0)
                {
                    consulta = consulta.Where(x => vigencias.Contains(x.vigencia.id)).ToList();
                }
                //filtro zonas
                if (zona != "00" && zona != string.Empty)
                {
                    consulta = consulta.Where(x => x.zona.code == zona).ToList();
                }
                //filtro descripcion
                if (!string.IsNullOrEmpty(descrip))
                {
                    consulta = consulta.Where(x => x.dsc_extra.ToUpper().Contains(descrip.ToUpper())).ToList();
                }
                return consulta;
            }
        }
        
        public List<DETAIL_PROCESS> base_movimiento(SYSTEM sistema, DateTime fecha_corte, int[] vigencia, GENERIC_VALUE clase, GENERIC_VALUE zona)
        {
            var salida = new List<DETAIL_PROCESS>();
            using (AFN2Entities context = new AFN2Entities())
            using (var repo = new Repositories.Main(context))
            {
                var aprovados = repo.aprobaciones.OnlyActive.Select(x => x.code).ToArray();
                salida = repo.get_detailed(sistema, fecha_corte,0, aprovados, true, clase,zona);
            }
            if (vigencia.Count() > 0)
                salida = salida.Where(x => vigencia.Contains(x.vigencia.id)).ToList();
            //return JsonConvert.SerializeObject(salida.First()).Substring(0,200);
            return salida;
        }
        public List<DETAIL_PROCESS> base_movimiento(SYSTEM sistema, DateTime fecha_corte, GENERIC_VALUE clase, GENERIC_VALUE zona)
        {
            var vigentes = new int[] { 1 };
            return base_movimiento(sistema, fecha_corte, vigentes, clase, zona);
        }
        public List<DETAIL_PROCESS> base_movimiento(SYSTEM sistema, DateTime fecha_corte, int[] vigencia)
        {
            return base_movimiento(sistema, fecha_corte, vigencia, null, null);
        }
        public List<DETAIL_PROCESS> base_movimiento(SYSTEM sistema, DateTime fecha_corte)
        {
            var vigentes = new int[] { 1 };
            return base_movimiento(sistema, fecha_corte,vigentes,null,null);
        }
        
        public RespuestaAccion GuardarProcesoDepreciacion(List<DETAIL_DEPRECIATE> lineas, DateTime fecha_proceso)
        {
            var informe = new RespuestaAccion();
            using (AFN2Entities context = new AFN2Entities())
            using (Repositories.Main repo = new Repositories.Main(context))
            {
                var default_system = repo.sistemas.Default;
                var wholeArticles = lineas.Select(x => new { arti = x.cod_articulo, part = x.parte }).Distinct();
                //var cont = wholeArticles.Count();
                foreach (var a in wholeArticles) {
                    var arti = a.arti;
                    var part = a.part;
                    var wholeTransac = lineas.Where(x => x.cod_articulo == arti && x.parte == part).ToList();
                    var defaultTransac = wholeTransac.Where(x => x.sistema == default_system).First();
                    var HeadIdSource = defaultTransac.HeadId;
                    foreach (var t in wholeTransac)
                    {
                        if (t.HeadId != HeadIdSource)
                        {
                            //hubo un problema
                            informe.codigo = 3;
                            informe.descripcion = "Transacción de depreciacion no es posible para " + arti.ToString() + "-" + part.ToString();
                            return informe;
                        }
                    }
                    //preparamos cabecera
                    if (defaultTransac.fecha_inicio != fecha_proceso || defaultTransac.RefSource != "dep_calc")
                    {
                        var details = new EntityCollection<TRANSACTION_DETAIL>();
                        var parameters = new EntityCollection<TRANSACTION_PARAMETER_DETAIL>();
                        foreach (var currTransac in wholeTransac)
                        {
                            #region Detail
                            var detail_t_new = new TRANSACTION_DETAIL();
                            detail_t_new.system_id = currTransac.sistema.id;
                            detail_t_new.validity_id = currTransac.vigencia.id;
                            detail_t_new.depreciate = currTransac.se_deprecia;
                            detail_t_new.allow_credit = currTransac.derecho_credito;
                            details.Add(detail_t_new);
                            #endregion
                            #region Parameters
                            if (currTransac.valor_anterior_base != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.PrecioBase.id;
                                param_t_new.parameter_value = (decimal)(currTransac.valor_anterior_base) / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            if (currTransac.DA_AF != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.DepreciacionAcum.id;
                                param_t_new.parameter_value = (decimal)currTransac.DA_AF / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            if (currTransac.deter != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.Deterioro.id;
                                param_t_new.parameter_value = (decimal)currTransac.deter / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            if (currTransac.vu_resi != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.VidaUtil.id;
                                param_t_new.parameter_value = currTransac.vu_resi;
                                parameters.Add(param_t_new);
                            }
                            if (currTransac.val_res != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.ValorResidual.id;
                                param_t_new.parameter_value = (decimal)currTransac.val_res / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            if (currTransac.cred_adi_base != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.Credito.id;
                                param_t_new.parameter_value = (decimal)currTransac.cred_adi_base / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            #endregion
                        }
                        #region Header
                        //debo cortar los registros en sus tiempos
                        var linea_anterior = (from h in context.TRANSACTIONS_HEADERS
                                              where h.id == defaultTransac.HeadId
                                              select h).First();
                        linea_anterior.trx_end = fecha_proceso;

                        var head_t_new = new TRANSACTION_HEADER();
                        head_t_new.article_part_id = defaultTransac.PartId;
                        head_t_new.trx_ini = fecha_proceso;
                        head_t_new.trx_end = defaultTransac.fecha_fin;
                        head_t_new.ref_source = "dep_calc";
                        head_t_new.zone_id = defaultTransac.zona.id;
                        head_t_new.subzone_id = defaultTransac.subzona.id;
                        head_t_new.kind_id = defaultTransac.clase.id;
                        head_t_new.subkind_id = defaultTransac.subclase.id;
                        head_t_new.category_id = defaultTransac.categoria.id;
                        head_t_new.user_own = "DEPRECIANDO";
                        head_t_new.manage_id = (defaultTransac.gestion.id == 0 ? (int?)(null) : defaultTransac.gestion.id);
                        head_t_new.TRANSACTIONS_PARAMETERS_DETAILS = parameters;
                        head_t_new.TRANSACTIONS_DETAILS = details;
                        context.TRANSACTIONS_HEADERS.AddObject(head_t_new);
                        #endregion
                        context.SaveChanges();

                    }
                    else
                    {
                        informe.codigo = 2;
                        informe.descripcion = "Ya se encontraba depreciado";
                    }
                }
            }
            return informe;
        }

        private DETAIL_MOVEMENT define_new_detail_movement(DETAIL_PROCESS art_fin, DETAIL_PROCESS art_ini)
        {
            var NewDetail = new DETAIL_MOVEMENT();
            NewDetail.fecha_compra = art_fin.fecha_compra;
            NewDetail.cod_articulo = art_fin.cod_articulo;
            NewDetail.desc_breve = art_fin.dsc_extra;
            NewDetail.cantidad = art_fin.cantidad;
            NewDetail.zona = art_fin.zona;
            NewDetail.clase = art_fin.clase;
            if (art_ini != null)
            {
                NewDetail.valor_activo_inicial = (art_ini.parametros.GetPrecioBase.value * art_ini.cantidad) + (art_fin.parametros.GetCredito.value * art_fin.cantidad);
                NewDetail.credito_monto = 0;
                NewDetail.depreciacion_acum_inicial = art_ini.parametros.GetDepreciacionAcum.value * art_ini.cantidad;
                NewDetail.deterioro = art_ini.parametros.GetDeterioro.value * art_ini.cantidad;
                NewDetail.valor_residual = art_ini.parametros.GetValorResidual.value * art_ini.cantidad;
                NewDetail.vida_util_asignada = (int)(art_ini.parametros.GetVidaUtil.value);
                NewDetail.vida_util_residual = (int)(art_fin.parametros.GetVidaUtil.value);
                NewDetail.situacion = Situaciones.ByValidations(art_ini.vigencia.id, art_fin.vigencia.id).description;
            }
            else
            {
                NewDetail.valor_activo_inicial = art_fin.parametros.GetPrecioBase.value * art_fin.cantidad;
                NewDetail.credito_monto = art_fin.parametros.GetCredito.value * art_fin.cantidad;
                NewDetail.depreciacion_acum_inicial = 0;
                NewDetail.deterioro = art_fin.parametros.GetDeterioro.value * art_fin.cantidad;
                NewDetail.valor_residual = art_fin.parametros.GetValorResidual.value * art_fin.cantidad;
                NewDetail.vida_util_asignada = art_fin.vida_util_inicial;
                NewDetail.vida_util_residual = (int)(art_fin.parametros.GetVidaUtil.value);
                NewDetail.situacion = Situaciones.ByValidations(4, art_fin.vigencia.id).description;
            }
            NewDetail.porcentaje_cm = 0;
            NewDetail.valor_activo_cm = 0;
            NewDetail.valor_activo_update = NewDetail.valor_activo_inicial + NewDetail.valor_activo_cm;
            NewDetail.preparacion = art_fin.parametros.GetPreparacion.value * art_fin.cantidad;
            NewDetail.desmantelamiento = art_fin.parametros.GetDesmantelamiento.value * art_fin.cantidad;
            NewDetail.transporte = art_fin.parametros.GetTransporte.value * art_fin.cantidad;
            NewDetail.montaje = art_fin.parametros.GetMontaje.value * art_fin.cantidad;
            NewDetail.honorario = art_fin.parametros.GetHonorario.value * art_fin.cantidad;
            NewDetail.valor_activo_final = NewDetail.valor_activo_update + NewDetail.credito_monto + NewDetail.preparacion + NewDetail.desmantelamiento + NewDetail.transporte + NewDetail.montaje + NewDetail.honorario;
            NewDetail.depreciacion_acum_cm = 0;
            NewDetail.depreciacion_acum_update = NewDetail.depreciacion_acum_inicial + NewDetail.depreciacion_acum_cm;
            NewDetail.valor_sujeto_dep = NewDetail.valor_activo_final + NewDetail.depreciacion_acum_update + NewDetail.deterioro + NewDetail.valor_residual;
            NewDetail.vida_util_ocupada = NewDetail.vida_util_asignada - NewDetail.vida_util_residual;
            NewDetail.depreciacion_acum_final = art_fin.parametros.GetDepreciacionAcum.value * art_fin.cantidad;
            NewDetail.depreciacion_ejercicio = NewDetail.depreciacion_acum_final - NewDetail.depreciacion_acum_inicial;
            NewDetail.revalorizacion = art_fin.parametros.GetRevalorizacion.value * art_fin.cantidad;
            NewDetail.valor_libro = NewDetail.depreciacion_acum_final + NewDetail.valor_activo_final + NewDetail.revalorizacion;

            NewDetail.fecha_inicio = art_fin.fecha_inicio;
            NewDetail.fecha_fin = art_fin.fecha_fin;
            NewDetail.vigencia = art_fin.vigencia;
            NewDetail.subzona = art_fin.subzona;
            NewDetail.subclase = art_fin.subclase;
            NewDetail.origen = art_fin.origen;
            return NewDetail;
        }

        public List<DETAIL_MOVEMENT> reporte_vigentes(Vperiodo periodo, GENERIC_VALUE clase, GENERIC_VALUE zona, SYSTEM sistema) 
        {
            var result = new List<DETAIL_MOVEMENT>();
            var vig = this.Vigencias.All().Select(v => v.id).ToArray();
            var estado_final = base_movimiento(sistema, periodo.last,clase,zona);
            var estado_inicial = base_movimiento(sistema, periodo.first, vig, clase, zona);
            foreach (var art_fin in estado_final) {
                DETAIL_PROCESS art_ini = estado_inicial.Where(x => x.cod_articulo == art_fin.cod_articulo && x.parte == art_fin.parte).FirstOrDefault();
                DETAIL_MOVEMENT nL = define_new_detail_movement(art_fin, art_ini);
                result.Add(nL);
            }
            return result;
        }
        public List<DETAIL_MOVEMENT> reporte_bajas(Vperiodo desde, Vperiodo hasta, int situacion, SYSTEM sistema)
        {
            var result = new List<DETAIL_MOVEMENT>();
            var vig_down = this.Vigencias.Downs().Select(v => v.id).ToArray();
            var vig_all = this.Vigencias.All().Select(v => v.id).ToArray();
            var estado_final = base_movimiento(sistema, hasta.last,vig_down);
            var estado_inicial = base_movimiento(sistema, hasta.first, vig_all);
            foreach (var art_fin in estado_final)
            {
                if (art_fin.fecha_inicio >= desde.first)
                {
                    DETAIL_PROCESS art_ini = estado_inicial.Where(x => x.cod_articulo == art_fin.cod_articulo && x.parte == art_fin.parte).FirstOrDefault();
                    //Si no tiene inicial, entonces hay un problema de otro tipo
                    if (art_ini != null)
                    {
                        DETAIL_MOVEMENT nL = define_new_detail_movement(art_fin, art_ini);
                        result.Add(nL);
                    }
                }
            }
            return result;
        }
        public List<DETAIL_MOVEMENT> reporte_completo(Vperiodo periodo, GENERIC_VALUE clase, GENERIC_VALUE zona, SYSTEM sistema)
        {
            var result = new List<DETAIL_MOVEMENT>();
            var vig = this.Vigencias.All().Select(v => v.id).ToArray();
            var estado_final = base_movimiento(sistema, periodo.last,vig, clase, zona);
            ServiceProcess.Tracking.ExportTo.FileText(estado_final, "estado_final_rev1");
            var estado_inicial = base_movimiento(sistema, periodo.first, vig, clase, zona);
            ServiceProcess.Tracking.ExportTo.FileText(estado_inicial, "estado_inicial_rev1");
            foreach (var art_fin in estado_final)
            {
                DETAIL_PROCESS art_ini = estado_inicial.Where(x => x.cod_articulo == art_fin.cod_articulo && x.parte == art_fin.parte).FirstOrDefault();
                DETAIL_MOVEMENT nL = define_new_detail_movement(art_fin, art_ini);
                if (nL.situacion != "HISTORICO")
                    result.Add(nL);
            }
            ServiceProcess.Tracking.ExportTo.FileText(result,"movimiento_revision");
            return result;
        }


        public List<GROUP_MOVEMENT> reporte_vigente_resumen(Vperiodo periodo, GENERIC_VALUE clase, GENERIC_VALUE zona, SYSTEM sistema, string orderBy)
        {
            var resultado = new List<GROUP_MOVEMENT>();
            var detalle = reporte_vigentes(periodo, clase, zona, sistema);
            if (orderBy == "C")
            {
                var grupos = from det in detalle
                             group det by det.clase into newGroup
                             orderby newGroup.Key.code
                             select newGroup;

                foreach (var thisGroup in grupos)
                {
                    var subgrupos = from grp in thisGroup
                                    group grp by new { zona = grp.zona.code, lugar= grp.subzona.code } into subGroup
                                    orderby subGroup.Key.zona, subGroup.Key.lugar
                                    select subGroup;
                    foreach (var thisSubGroup in subgrupos)
                    {
                        var linea = new GROUP_MOVEMENT();
                        linea.clase = thisGroup.Key;
                        linea.zona = (from a in thisSubGroup select a.zona).First();
                        linea.lugar = (from a in thisSubGroup select a.subzona).First();
                        linea.valor_inicial_activo = thisSubGroup.Sum(a => a.valor_activo_inicial);
                        linea.cm_activo = thisSubGroup.Sum(a => a.valor_activo_cm);
                        linea.credito = thisSubGroup.Sum(a => a.credito_monto);
                        linea.valor_final_activo = thisSubGroup.Sum(a => a.valor_activo_final);
                        linea.depreciacion_acumulada_inicial = thisSubGroup.Sum(a => a.depreciacion_acum_inicial);
                        linea.cm_depreciacion = thisSubGroup.Sum(a => a.depreciacion_acum_cm);
                        linea.valor_residual = thisSubGroup.Sum(a => a.valor_residual);
                        linea.depreciacion_ejercicio = thisSubGroup.Sum(a => a.depreciacion_ejercicio);
                        linea.depreciacion_acumulada_final = thisSubGroup.Sum(a => a.depreciacion_acum_final);
                        linea.valor_libro = thisSubGroup.Sum(a => a.valor_libro);
                        linea.orden1 = linea.clase.code;
                        linea.orden2 = linea.zona.code;
                        linea.orden3 = linea.lugar.code;
                        resultado.Add(linea);
                    }
                    var subtotal = new GROUP_MOVEMENT();
                    subtotal.clase = thisGroup.Key;
                    subtotal.zona = new GENERIC_VALUE(99999,"TOTAL","SubTotal1");
                    subtotal.lugar = new GENERIC_VALUE(99999, "", "SubTotal2");
                    subtotal.valor_inicial_activo = thisGroup.Sum(a => a.valor_activo_inicial);
                    subtotal.cm_activo = thisGroup.Sum(a => a.valor_activo_cm);
                    subtotal.credito = thisGroup.Sum(a => a.credito_monto);
                    subtotal.valor_final_activo = thisGroup.Sum(a => a.valor_activo_final);
                    subtotal.depreciacion_acumulada_inicial = thisGroup.Sum(a => a.depreciacion_acum_inicial);
                    subtotal.cm_depreciacion = thisGroup.Sum(a => a.depreciacion_acum_cm);
                    subtotal.valor_residual = thisGroup.Sum(a => a.valor_residual);
                    subtotal.depreciacion_ejercicio = thisGroup.Sum(a => a.depreciacion_ejercicio);
                    subtotal.depreciacion_acumulada_final = thisGroup.Sum(a => a.depreciacion_acum_final);
                    subtotal.valor_libro = thisGroup.Sum(a => a.valor_libro);
                    subtotal.orden1 = subtotal.clase.code;
                    subtotal.orden2 = subtotal.zona.code;
                    subtotal.orden3 = subtotal.lugar.code;
                    resultado.Add(subtotal);
                }
                var total = new GROUP_MOVEMENT();
                total.clase = new GENERIC_VALUE(99999, "TOTAL", "SubTotal3"); ;
                total.zona = new GENERIC_VALUE(99999, "GENERAL", "SubTotal1");
                total.lugar = new GENERIC_VALUE(99999, "", "SubTotal2");
                total.valor_inicial_activo = detalle.Sum(a => a.valor_activo_inicial);
                total.cm_activo = detalle.Sum(a => a.valor_activo_cm);
                total.credito = detalle.Sum(a => a.credito_monto);
                total.valor_final_activo = detalle.Sum(a => a.valor_activo_final);
                total.depreciacion_acumulada_inicial = detalle.Sum(a => a.depreciacion_acum_inicial);
                total.cm_depreciacion = detalle.Sum(a => a.depreciacion_acum_cm);
                total.valor_residual = detalle.Sum(a => a.valor_residual);
                total.depreciacion_ejercicio = detalle.Sum(a => a.depreciacion_ejercicio);
                total.depreciacion_acumulada_final = detalle.Sum(a => a.depreciacion_acum_final);
                total.valor_libro = detalle.Sum(a => a.valor_libro);
                total.orden1 = total.clase.code;
                total.orden2 = total.zona.code;
                total.orden3 = total.lugar.code;
                resultado.Add(total);
            }
            else
            {

            }
            
            return resultado;
        }

        public List<GROUP_MOVEMENT> reporte_cuadro_movimiento(Vperiodo periodo, GENERIC_VALUE tipo, SYSTEM sistema )
        {
            var resultado = new List<GROUP_MOVEMENT>();
            if (tipo.type == "TYPE_ASSET")
            {
                GENERIC_VALUE clase;
                if (tipo.id == 1)
                    clase = Clases.Activos;
                else if (tipo.id == 2)
                    clase = Clases.Intagibles;
                else
                    clase = Clases.Activos;

                var detalle = reporte_completo(periodo, clase, null, sistema);
                var grupos = (from det in detalle
                              group det by det.clase into newGroups
                              orderby newGroups.Key.code
                              select newGroups);
                foreach (var FirstGroup in grupos)
                {
                    var linea = new GROUP_MOVEMENT();
                    linea.clase = FirstGroup.Key;
                    linea.zona = (from a in FirstGroup select a.zona).First();  //no tiene importancia completar este valor para este reporte
                    linea.lugar = (from a in FirstGroup select a.subzona).First(); //no tiene importancia completar este valor para este reporte
                    linea.saldo_inicial_activo = FirstGroup.Where(a=>a.fecha_compra<periodo.first).Sum(a => a.valor_activo_inicial);
                    linea.adiciones_regular = FirstGroup.Where(a => a.fecha_compra >= periodo.first && a.origen.code != "OBC").Sum(a => a.valor_activo_inicial);
                    linea.adiciones_obc = FirstGroup.Where(a => a.fecha_compra >= periodo.first && a.origen.code == "OBC").Sum(a => a.valor_activo_inicial);
                    //linea.valor_inicial_activo = FirstGroup.Sum(a => a.valor_activo_inicial);
                    linea.valor_inicial_activo = linea.saldo_inicial_activo + linea.adiciones_regular+linea.adiciones_obc;
                    linea.cm_activo = FirstGroup.Sum(a => a.valor_activo_cm);
                    linea.credito = FirstGroup.Sum(a => a.credito_monto);
                    linea.castigo_activo = FirstGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 3).Sum(a => (a.valor_activo_inicial))*-1;
                    linea.venta_activo = FirstGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 2).Sum(a => (a.valor_activo_inicial))*-1;
                    linea.valor_final_activo = FirstGroup.Where(a => a.situacion!="BAJA").Sum(a => a.valor_activo_final);
                    linea.depreciacion_acumulada_inicial = FirstGroup.Sum(a => a.depreciacion_acum_inicial);
                    linea.cm_depreciacion = FirstGroup.Sum(a => a.depreciacion_acum_cm);
                    linea.valor_residual = FirstGroup.Sum(a => a.valor_residual);
                    linea.depreciacion_ejercicio = FirstGroup.Sum(a => a.depreciacion_ejercicio);
                    linea.castigo_depreciacion = FirstGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 3).Sum(a => a.depreciacion_acum_final)*-1;
                    linea.venta_depreciacion = FirstGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 2).Sum(a => a.depreciacion_acum_final)*-1;
                    linea.depreciacion_acumulada_final = FirstGroup.Where(a=> a.situacion!="BAJA").Sum(a => a.depreciacion_acum_final);
                    linea.valor_libro = FirstGroup.Where(a => a.situacion != "BAJA").Sum(a => a.valor_libro);
                    linea.orden1 = linea.clase.code;
                    linea.orden2 = linea.zona.code;
                    linea.orden3 = linea.lugar.code;
                    resultado.Add(linea);
                }

                //Agrego Obras en Construccion
                var linea_obc = new GROUP_MOVEMENT();
                var movimiento = movimiento_obras(periodo.first, periodo.last, sistema.CURRENCY.id);
                linea_obc.clase = Clases.OBC;
                //linea_obc.zona = null;  //no tiene importancia completar este valor para este reporte
                //linea_obc.lugar = null; //no tiene importancia completar este valor para este reporte
                linea_obc.saldo_inicial_activo = movimiento.saldo_inicial;
                linea_obc.adiciones_regular = movimiento.incremento;
                linea_obc.adiciones_obc = 0;
                linea_obc.valor_inicial_activo = movimiento.saldo_inicial + movimiento.incremento;
                linea_obc.cm_activo = 0;
                linea_obc.credito = 0;
                linea_obc.castigo_activo = movimiento.dec_to_gasto;
                linea_obc.venta_activo = movimiento.dec_to_activo;
                linea_obc.valor_final_activo = movimiento.saldo_final;
                linea_obc.depreciacion_acumulada_inicial = 0;
                linea_obc.cm_depreciacion = 0;
                linea_obc.valor_residual = 0;
                linea_obc.depreciacion_ejercicio = 0;
                linea_obc.castigo_depreciacion = 0;
                linea_obc.venta_depreciacion = 0;
                linea_obc.depreciacion_acumulada_final = 0;
                linea_obc.valor_libro = 0;
                linea_obc.orden1 = linea_obc.clase.code;
                linea_obc.orden2 = string.Empty;
                linea_obc.orden3 = string.Empty;
                resultado.Add(linea_obc);
            }
            
            return resultado;
        }

        public List<DETAIL_OBC> saldo_obras(DateTime fecha_consulta, int currency_id)
        {
            using (AFN2Entities context = new AFN2Entities())
            using (var repo = new Repositories.Main(context))
            {
                List<DETAIL_OBC> resultado = new List<DETAIL_OBC>();
                var entradas = (from head in context.ASSETS_IN_PROGRESS_HEAD
                         join detail in context.ASSETS_IN_PROGRESS_DETAIL on head.id equals detail.head_id
                         where detail.currency_id == currency_id
                         && head.tipo == "E" && head.post_date <= fecha_consulta
                         && head.aproval_state_id == 2
                                select new { head, detail });
                var salidas = (from head in context.ASSETS_IN_PROGRESS_HEAD
                                join detail in context.ASSETS_IN_PROGRESS_DETAIL on head.id equals detail.head_id
                                join asset in context.BATCHS_ARTICLES on head.batch_id equals asset.id into ga
                                from batch in ga.DefaultIfEmpty()
                                where detail.currency_id == currency_id
                                && head.tipo == "S" && head.post_date <= fecha_consulta
                                && head.aproval_state_id == 2
                               select new { 
                                   head.entrada_id, 
                                   detail.amount,
                                   aproval_state_id = (batch == null ? head.aproval_state_id : batch.aproval_state_id) 
                               }).ToList();
                foreach (var fila in entradas)
                {
                    var detail_obc = new DETAIL_OBC();
                    detail_obc.moneda = fila.detail.CURRENCY;
                    detail_obc.codMov = fila.head.id;
                    detail_obc.description = fila.head.descrip;
                    detail_obc.txFecha = fila.head.trx_date;
                    detail_obc.glFecha = fila.head.post_date;
                    detail_obc.zona = fila.head.ZONE;
                    detail_obc.monto = (double)(fila.detail.amount);
                    detail_obc.ocupado = (double)(salidas
                        .Where(s => s.entrada_id == fila.head.id && s.aproval_state_id == 2)
                        .Select(s=> s.amount)
                        .DefaultIfEmpty(0)
                        .Sum());
                    if(detail_obc.saldo != 0)
                        resultado.Add(detail_obc);
                }
                return resultado;
            }
        }
        public List<DETAIL_OBC> saldo_obras(DateTime fecha_consulta, GENERIC_VALUE currency)
        {
            return saldo_obras(fecha_consulta, currency.id);
        }

        public GROUP_OBC movimiento_obras(DateTime desde,DateTime hasta, int currency_id)
        {
            var saldo_inicial = saldo_obras(desde, currency_id).Select(i => i.saldo);
            var saldo_final = saldo_obras(hasta, currency_id).Select(f => f.saldo);

            using (AFN2Entities context = new AFN2Entities())
            using (var repo = new Repositories.Main(context))
            {
                var entradas = (from head in context.ASSETS_IN_PROGRESS_HEAD
                                join detail in context.ASSETS_IN_PROGRESS_DETAIL on head.id equals detail.head_id
                                where detail.currency_id == currency_id
                                && head.tipo == "E" 
                                && head.post_date <= hasta && head.post_date>desde
                                && head.aproval_state_id == 2
                                select detail.amount ).ToList();
                var salidas = (from head in context.ASSETS_IN_PROGRESS_HEAD
                               join detail in context.ASSETS_IN_PROGRESS_DETAIL on head.id equals detail.head_id
                               where detail.currency_id == currency_id
                               && head.tipo == "S" 
                               && head.post_date <= hasta && head.post_date>desde
                               && head.aproval_state_id == 2
                               select new { 
                                   head_batch_id = head.batch_id, 
                                   detail_amount = detail.amount 
                               }
                               ).ToList();
                var resultado = new GROUP_OBC();
                resultado.saldo_inicial = saldo_inicial.Sum();
                resultado.incremento = (double) entradas.DefaultIfEmpty(0).Sum() ;
                resultado.dec_to_activo = (double)salidas.Where(s => s.head_batch_id != null).Select(s => s.detail_amount).DefaultIfEmpty(0).Sum();
                resultado.dec_to_gasto = (double)salidas.Where(s => s.head_batch_id == null).Select(s => s.detail_amount).DefaultIfEmpty(0).Sum();
                resultado.saldo_final = saldo_final.Sum();
                return resultado;
            }
        }
    }
}
