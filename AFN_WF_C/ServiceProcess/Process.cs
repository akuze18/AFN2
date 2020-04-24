using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

using System.Data.Objects.DataClasses;

//using Newtonsoft.Json;
using ACode;

using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;


namespace AFN_WF_C.ServiceProcess
{
    public class Process : IDisposable
    {
        private ServiceAFN2 _svr;

        public Process(ServiceAFN2 svr)
        {
            _svr = svr;
        }
        public List<DETAIL_PROCESS> buscar_Articulo(DateTime desde, DateTime hasta, int codigo, string descrip, string zona, int[] vigencias, string[] aprovados, AFN_WF_C.PCClient.Vistas.Busquedas.moment_data cuando, bool CheckPost)
        {
            var repo = _svr.Repo;
            {
                DateTime current = DateTime.Now;
                SV_SYSTEM sis = repo.sistemas.Default;
                bool withParam = false;
                List<DETAIL_PROCESS> consulta;

                if (codigo > 0)
                {
                    if (aprovados != null && aprovados.Length != 0)
                    {
                        consulta = repo.get_detailed(sis, current, codigo, aprovados, withParam, CheckPost);
                    }
                    else
                    {
                        consulta = repo.get_detailed(sis, current, codigo, withParam, CheckPost);
                    }
                }
                else
                {
                    if (aprovados != null && aprovados.Length != 0)
                    {
                        consulta = repo.get_detailed(sis, current, aprovados, withParam, CheckPost);
                    }
                    else
                    {
                        consulta = repo.get_detailed(sis, current, withParam, CheckPost);
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
                    consulta = consulta.Where(x => x.zona.codDept == zona).ToList();
                }
                //filtro descripcion
                if (!string.IsNullOrEmpty(descrip))
                {
                    consulta = consulta.Where(x => x.dsc_extra.ToUpper().Contains(descrip.ToUpper())).ToList();
                }
                return consulta;
            }
        }
        public List<SV_PROVEEDOR> buscar_proveedor(string idRut, string nombre)
        {
            var repo = _svr.Repo;
            return repo.Proveedor.buscar(idRut, nombre);
        }

        public List<DETAIL_PROCESS> base_movimiento(SV_SYSTEM sistema, DateTime fecha_corte, int[] vigencia, GENERIC_VALUE clase, GENERIC_VALUE zona)
        {
            var salida = new List<DETAIL_PROCESS>();

            var repo = _svr.Repo;
            {
                var aprobados = repo.EstadoAprobacion.OnlyActive.Select(x => x.code).ToArray();
                salida = repo.get_detailed(sistema, fecha_corte, 0, aprobados, true, true, clase, zona);
            }
            if (vigencia.Count() > 0)
                salida = salida.Where(x => vigencia.Contains(x.vigencia.id)).ToList();
            //return JsonConvert.SerializeObject(salida.First()).Substring(0,200);

            ServiceProcess.Tracking.ExportTo.FileText(salida, "rev_mov02");
            return salida;
        }
        public List<DETAIL_PROCESS> base_movimiento(SV_SYSTEM sistema, DateTime fecha_corte, GENERIC_VALUE clase, GENERIC_VALUE zona)
        {
            var vigentes = new int[] { 1 };
            return base_movimiento(sistema, fecha_corte, vigentes, clase, zona);
        }
        public List<DETAIL_PROCESS> base_movimiento(SV_SYSTEM sistema, DateTime fecha_corte, int[] vigencia)
        {
            return base_movimiento(sistema, fecha_corte, vigencia, null, null);
        }
        public List<DETAIL_PROCESS> base_movimiento(SV_SYSTEM sistema, DateTime fecha_corte)
        {
            var vigentes = new int[] { 1 };
            return base_movimiento(sistema, fecha_corte, vigentes, null, null);
        }

        public List<DETAIL_MOVEMENT> changed_movimiento(SV_SYSTEM sistema, Vperiodo periodo_corte,int BatchId, int PartIndex, DateTime desde, DateTime hasta, SV_KIND[] clases, SV_ZONE[] zonas)
        {
            var repo = _svr.Repo;
            {
                return repo.get_changed_mov(sistema, periodo_corte, desde, hasta,BatchId, PartIndex,true, clases, zonas);
            }
        }

        public List<DETAIL_DEPRECIATE> DepreciacionProcesoMensual(int año, int mes, string usuario)
        {
            var result = new List<DETAIL_DEPRECIATE>();
            var periodo = new ACode.Vperiodo(año, mes);

            Repositories.CORRECTIONS_MONETARIES_VALUES CM;
            var repo = _svr.Repo;
            {
                repo.set_correccion_monetaria(periodo.lastDB.Substring(0, 6));
                CM = repo.correcciones_monetarias;
            }
            //var CM = CorreccionMonetaria.ByPeriodo(periodo.lastDB.Substring(0, 6));

            var sistemas = _svr.Repo.sistemas.All();
            foreach (var S in sistemas)
            {
                var activos = base_movimiento(S, periodo.last);
                var anterior = base_movimiento(S, (periodo - 1).last);
                foreach (var act in activos)
                {
                    DETAIL_PROCESS aProcesar;
                    decimal cmVal = CM.byAplica(act.fecha_compra, periodo).amount;
                    var findAnt = anterior.Where(ant => ant.PartId == act.PartId).FirstOrDefault();
                    if (findAnt == null)
                        aProcesar = act;
                    else
                        aProcesar = findAnt;
                    var proceso = new DETAIL_DEPRECIATE(aProcesar, cmVal, periodo.last);
                    result.Add(proceso);
                }
            }
            GuardarProcesoDepreciacion(result, periodo.last, usuario);
            return result;
        }
        private RespuestaAccion GuardarProcesoDepreciacion(List<DETAIL_DEPRECIATE> lineas, DateTime fecha_proceso, string usuario)
        {
            var informe = new RespuestaAccion();
            Repositories.Main repo = _svr.Repo;
            {
                var default_system = repo.sistemas.Default;
                var wholeArticles = lineas.Select(x => new { arti = x.cod_articulo, part = x.parte }).Distinct();
                //var cont = wholeArticles.Count();
                foreach (var a in wholeArticles)
                {
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
                            //Only IFRS
                            if (currTransac.preparacion != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.Preparacion.id;
                                param_t_new.parameter_value = (decimal)currTransac.preparacion / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            if (currTransac.transporte != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.Transporte.id;
                                param_t_new.parameter_value = (decimal)currTransac.transporte / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            if (currTransac.montaje != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.Montaje.id;
                                param_t_new.parameter_value = (decimal)currTransac.montaje / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            if (currTransac.desmantelamiento != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.Desmantelamiento.id;
                                param_t_new.parameter_value = (decimal)currTransac.desmantelamiento / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            if (currTransac.honorario != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.Honorario.id;
                                param_t_new.parameter_value = (decimal)currTransac.honorario / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            if (currTransac.revalorizacion != 0)
                            {
                                var param_t_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_t_new.system_id = currTransac.sistema.id;
                                param_t_new.paratemer_id = repo.parametros.Revalorizacion.id;
                                param_t_new.parameter_value = (decimal)currTransac.revalorizacion / currTransac.cantidad;
                                parameters.Add(param_t_new);
                            }
                            #endregion
                        }
                        #region Header
                        //debo cortar los registros en sus tiempos
                        var linea_anterior = (from h in _svr.DB.TRANSACTIONS_HEADERS
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
                        head_t_new.user_own = usuario;
                        head_t_new.manage_id = (defaultTransac.gestion.id == 0 ? (int?)(null) : defaultTransac.gestion.id);
                        head_t_new.method_revalue_id = 1;
                        head_t_new.TRANSACTIONS_PARAMETERS_DETAILS = parameters;
                        head_t_new.TRANSACTIONS_DETAILS = details;
                        _svr.DB.TRANSACTIONS_HEADERS.AddObject(head_t_new);
                        #endregion
                        _svr.DB.SaveChanges();

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

        //private DETAIL_MOVEMENT define_new_detail_movement(DETAIL_PROCESS art_fin, DETAIL_PROCESS art_ini)
        //{
        //    var NewDetail = new DETAIL_MOVEMENT();
        //    NewDetail.fecha_compra = art_fin.fecha_compra;
        //    NewDetail.cod_articulo = art_fin.cod_articulo;
        //    NewDetail.fecha_ingreso = art_fin.fecha_ing;
        //    NewDetail.desc_breve = art_fin.dsc_extra;
        //    NewDetail.cantidad = art_fin.cantidad;
        //    NewDetail.zona = art_fin.zona;
        //    NewDetail.clase = art_fin.clase;
        //    if (art_ini != null)
        //    {
        //        NewDetail.valor_activo_inicial = (art_ini.parametros.PrecioBaseVal * art_ini.cantidad) + (art_fin.parametros.CreditoVal * art_fin.cantidad);
        //        NewDetail.credito_monto = 0;
        //        NewDetail.depreciacion_acum_inicial = art_ini.parametros.DepreciacionAcumVal * art_ini.cantidad;
        //        NewDetail.deterioro = art_ini.parametros.DeterioroVal * art_ini.cantidad;
        //        NewDetail.valor_residual = art_ini.parametros.ValorResidualVal * art_ini.cantidad;
        //        NewDetail.vida_util_asignada = (int)(art_ini.parametros.VidaUtilVal);
        //        NewDetail.vida_util_residual = (int)(art_fin.parametros.VidaUtilVal);
        //        NewDetail.situacion = _svr.Repo.Situaciones.ByValidations(art_ini.vigencia.id, art_fin.vigencia.id).description;

        //        //Ajuste para determinar cambio de zona
        //        if (NewDetail.situacion == "ACTIVO" && (art_fin.zona.id != art_ini.zona.id || art_fin.subzona.id != art_ini.subzona.id))
        //            NewDetail.situacion = "MOVIDO";
        //    }
        //    else
        //    {
        //        NewDetail.valor_activo_inicial = art_fin.parametros.PrecioBaseVal * art_fin.cantidad;
        //        NewDetail.credito_monto = art_fin.parametros.CreditoVal * art_fin.cantidad;
        //        NewDetail.depreciacion_acum_inicial = 0;
        //        NewDetail.deterioro = art_fin.parametros.DeterioroVal * art_fin.cantidad;
        //        NewDetail.valor_residual = art_fin.parametros.ValorResidualVal * art_fin.cantidad;
        //        NewDetail.vida_util_asignada = art_fin.vida_util_inicial;
        //        NewDetail.vida_util_residual = (int)(art_fin.parametros.VidaUtilVal);
        //        NewDetail.situacion = _svr.Repo.Situaciones.ByValidations(4, art_fin.vigencia.id).description;
        //    }
        //    NewDetail.porcentaje_cm = 0;
        //    NewDetail.valor_activo_cm = 0;
        //    NewDetail.valor_activo_update = NewDetail.valor_activo_inicial + NewDetail.valor_activo_cm;
        //    NewDetail.preparacion = art_fin.parametros.GetPreparacion.value * art_fin.cantidad;
        //    NewDetail.desmantelamiento = art_fin.parametros.GetDesmantelamiento.value * art_fin.cantidad;
        //    NewDetail.transporte = art_fin.parametros.GetTransporte.value * art_fin.cantidad;
        //    NewDetail.montaje = art_fin.parametros.GetMontaje.value * art_fin.cantidad;
        //    NewDetail.honorario = art_fin.parametros.GetHonorario.value * art_fin.cantidad;
        //    NewDetail.valor_activo_final = NewDetail.valor_activo_update + NewDetail.credito_monto + NewDetail.preparacion + NewDetail.desmantelamiento + NewDetail.transporte + NewDetail.montaje + NewDetail.honorario;
        //    NewDetail.depreciacion_acum_cm = 0;
        //    NewDetail.depreciacion_acum_update = NewDetail.depreciacion_acum_inicial + NewDetail.depreciacion_acum_cm;
        //    NewDetail.valor_sujeto_dep = NewDetail.valor_activo_final + NewDetail.depreciacion_acum_update + NewDetail.deterioro + NewDetail.valor_residual;
        //    NewDetail.vida_util_ocupada = NewDetail.vida_util_asignada - NewDetail.vida_util_residual;
        //    NewDetail.depreciacion_acum_final = art_fin.parametros.GetDepreciacionAcum.value * art_fin.cantidad;
        //    NewDetail.depreciacion_ejercicio = NewDetail.depreciacion_acum_final - NewDetail.depreciacion_acum_inicial;
        //    NewDetail.revalorizacion = art_fin.parametros.GetRevalorizacion.value * art_fin.cantidad;
        //    NewDetail.valor_libro = NewDetail.depreciacion_acum_final + NewDetail.valor_activo_final + NewDetail.revalorizacion;

        //    NewDetail.fecha_inicio = art_fin.fecha_inicio;
        //    NewDetail.fecha_fin = art_fin.fecha_fin;
        //    NewDetail.vigencia = art_fin.vigencia;
        //    NewDetail.subzona = art_fin.subzona;
        //    NewDetail.subclase = art_fin.subclase;
        //    NewDetail.origen = art_fin.origen;
        //    NewDetail.gestion = art_fin.gestion;
        //    NewDetail.vida_util_inicial = art_fin.vida_util_inicial;
        //    return NewDetail;
        //}

        public List<DETAIL_MOVEMENT> reporte_vigentes(Vperiodo periodo, GENERIC_VALUE clase, GENERIC_VALUE zona, SV_SYSTEM sistema)
        {
            var result = new List<DETAIL_MOVEMENT>();
            var vig = _svr.Repo.Vigencias.All().Select(v => v.id).ToArray();
            var estado_final = base_movimiento(sistema, periodo.last, clase, zona);
            var estado_inicial = base_movimiento(sistema, periodo.first, vig, clase, zona);
            foreach (var art_fin in estado_final)
            {
                DETAIL_PROCESS art_ini = estado_inicial.Where(x => x.cod_articulo == art_fin.cod_articulo && x.parte == art_fin.parte).FirstOrDefault();
                DETAIL_MOVEMENT nL = new DETAIL_MOVEMENT(art_ini, art_fin, _svr.Repo.Situaciones);
                result.Add(nL);
            }
            return result;
        }
        public List<DETAIL_MOVEMENT> reporte_bajas(Vperiodo desde, Vperiodo hasta, int situacion, SV_SYSTEM sistema)
        {
            var result = new List<DETAIL_MOVEMENT>();
            var vig_down = _svr.Repo.Vigencias.Downs().Select(v => v.id).ToArray();
            var vig_all = _svr.Repo.Vigencias.All().Select(v => v.id).ToArray();
            var estado_final = base_movimiento(sistema, hasta.last, vig_down);
            var estado_inicial = base_movimiento(sistema, hasta.first, vig_all);
            foreach (var art_fin in estado_final)
            {
                if (art_fin.fecha_inicio >= desde.first)
                {
                    DETAIL_PROCESS art_ini = estado_inicial.Where(x => x.cod_articulo == art_fin.cod_articulo && x.parte == art_fin.parte).FirstOrDefault();
                    //Si no tiene inicial, entonces se vendio antes de 1 año de comprado
                    //{
                        DETAIL_MOVEMENT nL = new DETAIL_MOVEMENT(art_ini, art_fin, _svr.Repo.Situaciones);
                        result.Add(nL);
                    //}
                }
            }
            return result;
        }
        public List<DETAIL_MOVEMENT> reporte_completo(Vperiodo periodo, GENERIC_VALUE clase, GENERIC_VALUE zona, SV_SYSTEM sistema)
        {
            var result = new List<DETAIL_MOVEMENT>();
            var vig = _svr.Repo.Vigencias.All().Select(v => v.id).ToArray();
            var estado_final = base_movimiento(sistema, periodo.last, vig, clase, zona);
            ServiceProcess.Tracking.ExportTo.FileText(estado_final, "estado_final_rev1");
            var estado_inicial = base_movimiento(sistema, periodo.first.AddDays(-1), vig, clase, zona);
            ServiceProcess.Tracking.ExportTo.FileText(estado_inicial, "estado_inicial_rev1");
            foreach (var art_fin in estado_final)
            {
                DETAIL_PROCESS art_ini = estado_inicial.Where(x => x.cod_articulo == art_fin.cod_articulo && x.parte == art_fin.parte).FirstOrDefault();
                DETAIL_MOVEMENT nL = new DETAIL_MOVEMENT(art_ini, art_fin, _svr.Repo.Situaciones);
                if (nL.situacion != "HISTORICO")
                    result.Add(nL);
            }
            ServiceProcess.Tracking.ExportTo.FileText(result, "movimiento_revision");
            return result;
        }
        public List<DETAIL_MOVEMENT> reporte_vigentes_con_inv(Vperiodo periodo, GENERIC_VALUE clase, GENERIC_VALUE zona, SV_SYSTEM sistema)
        {
            var resultado = new List<DETAIL_MOVEMENT>();
            var detalleRegular = reporte_vigentes(periodo, clase, zona, sistema);
            var AttrEntrega = _svr.Repo.inv_atributos.Entregado();
            foreach (var det in detalleRegular)
            {
                var articulos_inventario = _svr.Repo.inv_articulos.ByPart(det.PartId);
                var mix = (from articulo in articulos_inventario
                           select 
                           new{ articulo, 
                               ubicacion = _svr.Repo.inv_ubicaciones.ById(articulo.ubicacion_id),
                               entregado = _svr.Repo.inv_articulos_details.ForArticle(det.cod_articulo, articulo.id, AttrEntrega.id),
                               ultimoEstado = GENERIC_VALUE.EmptyText
                           });
                var agrupado = (from m in mix
                                group m by 
                                new {
                                    UbucId = m.ubicacion.id, 
                                    Entregado = m.entregado, 
                                    UltimoEstado = m.ultimoEstado.id }
                                );
                foreach (var grupo in agrupado)
                {
                    var single = grupo.First();
                    var separado = new DETAIL_MOVEMENT(det, grupo.Count(), single.ubicacion, single.articulo.code, single.articulo.codigo_old, single.entregado.detalle, single.ultimoEstado);
                    resultado.Add(separado);
                }
            }
            return resultado;

        }

        public List<GROUP_MOVEMENT> reporte_vigente_resumen(Vperiodo periodo, GENERIC_VALUE clase, GENERIC_VALUE zona, SV_SYSTEM sistema, string orderBy)
        {
            var resultado = new List<GROUP_MOVEMENT>();
            var detalle = reporte_vigentes(periodo, clase, zona, sistema);
            if (orderBy == "C")
            {
                //agrupo el detalle por cada clase
                var grupos_por_clases = from det in detalle
                             group det by det.clase into newGroup
                             orderby newGroup.Key.cod
                             select newGroup;

                foreach (var grupoClase in grupos_por_clases)
                {
                    //agrupo cada detalle de grupo por zona y subzona
                    var subgrupos = from grp in grupoClase
                                    group grp by new { depto = grp.zona.codDept, lugar = grp.subzona.codPlace } into subGroup
                                    orderby subGroup.Key.depto, subGroup.Key.lugar
                                    select subGroup;
                    foreach (var thisSubGroup in subgrupos)
                    {
                        var linea = new GROUP_MOVEMENT();
                        linea.GroupKindDetailed(grupoClase.Key, thisSubGroup, _svr.Repo.subzonas);
                        resultado.Add(linea);
                    }
                    var subtotal = new GROUP_MOVEMENT();
                    subtotal.GroupKindTotalized(grupoClase.Key, grupoClase);
                    resultado.Add(subtotal);
                }
                var total = new GROUP_MOVEMENT();
                total.GroupKindGrandTotalized(detalle);
                resultado.Add(total);
            }
            else
            {
                //agrupo el detalle por cada zona
                var grupos_por_zonas = from det in detalle
                                        group det by det.zona into newGroup
                                        orderby newGroup.Key.codDept
                                        select newGroup;

                foreach (var grupoZona in grupos_por_zonas)
                {
                    //agrupo cada detalle de grupo por clase
                    var subgrupos = from grp in grupoZona
                                    group grp by new { clase = grp.clase.cod, lugar = grp.subzona.codPlace } into subGroup
                                    orderby subGroup.Key.clase, subGroup.Key.lugar
                                    select subGroup;
                    foreach (var thisSubGroup in subgrupos)
                    {
                        var linea = new GROUP_MOVEMENT();
                        linea.GroupZoneDetailed(grupoZona.Key, thisSubGroup, _svr.Repo.subzonas);
                        resultado.Add(linea);
                    }
                    var subtotal = new GROUP_MOVEMENT();
                    subtotal.GroupZoneTotalized(grupoZona.Key, grupoZona);
                    resultado.Add(subtotal);
                }
                var total = new GROUP_MOVEMENT();
                total.GroupKindGrandTotalized(detalle);
                resultado.Add(total);
            }

            return resultado;
        }

        public List<GROUP_MOVEMENT> reporte_cuadro_movimiento(Vperiodo periodo, GENERIC_VALUE tipo, SV_SYSTEM sistema)
        {
            var resultado = new List<GROUP_MOVEMENT>();
            if (tipo.type == "TYPE_ASSET")
            {
                GENERIC_VALUE clase;
                if (tipo.id == 1)
                    clase = _svr.Repo.Clases.Activos;
                else if (tipo.id == 2)
                    clase = _svr.Repo.Clases.Intagibles;
                else
                    clase = _svr.Repo.Clases.Activos;

                var detalle = reporte_completo(periodo, clase, null, sistema);
                var grupos = (from det in detalle
                              group det by det.clase into newGroups
                              orderby newGroups.Key.cod
                              select newGroups);
                foreach (var FirstGroup in grupos)
                {
                    var linea = new GROUP_MOVEMENT();
                    linea.clase = FirstGroup.Key;
                    linea.zona = (from a in FirstGroup select a.zona).First();  //no tiene importancia completar este valor para este reporte
                    linea.lugar = (from a in FirstGroup select a.subzona).First(); //no tiene importancia completar este valor para este reporte
                    linea.saldo_inicial_activo = FirstGroup.Where(a => a.fecha_compra < periodo.first).Sum(a => a.valor_activo_inicial);
                    linea.adiciones_regular = FirstGroup.Where(a => a.fecha_compra >= periodo.first && a.origen.code != "OBC").Sum(a => a.valor_activo_inicial);
                    linea.adiciones_obc = FirstGroup.Where(a => a.fecha_compra >= periodo.first && a.origen.code == "OBC").Sum(a => a.valor_activo_inicial);
                    //linea.valor_inicial_activo = FirstGroup.Sum(a => a.valor_activo_inicial);
                    linea.valor_inicial_activo = linea.saldo_inicial_activo + linea.adiciones_regular + linea.adiciones_obc;
                    linea.cm_activo = FirstGroup.Sum(a => a.valor_activo_cm);
                    linea.credito = FirstGroup.Sum(a => a.credito_monto);
                    linea.castigo_activo = FirstGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 3).Sum(a => (a.valor_activo_inicial)) * -1;
                    linea.venta_activo = FirstGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 2).Sum(a => (a.valor_activo_inicial)) * -1;
                    linea.valor_final_activo = FirstGroup.Where(a => a.situacion != "BAJA").Sum(a => a.valor_activo_final);
                    linea.depreciacion_acumulada_inicial = FirstGroup.Sum(a => a.depreciacion_acum_inicial);
                    linea.cm_depreciacion = FirstGroup.Sum(a => a.depreciacion_acum_cm);
                    linea.valor_residual = FirstGroup.Sum(a => a.valor_residual);
                    linea.depreciacion_ejercicio = FirstGroup.Sum(a => a.depreciacion_ejercicio);
                    linea.castigo_depreciacion = FirstGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 3).Sum(a => a.depreciacion_acum_final) * -1;
                    linea.venta_depreciacion = FirstGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 2).Sum(a => a.depreciacion_acum_final) * -1;
                    linea.depreciacion_acumulada_final = FirstGroup.Where(a => a.situacion != "BAJA").Sum(a => a.depreciacion_acum_final);
                    linea.valor_libro = FirstGroup.Where(a => a.situacion != "BAJA").Sum(a => a.valor_libro);
                    linea.orden1 = linea.clase.code;
                    linea.orden2 = linea.zona.code;
                    linea.orden3 = linea.lugar.code;
                    resultado.Add(linea);
                }

                //Agrego Obras en Construccion
                var linea_obc = new GROUP_MOVEMENT();
                var movimiento = movimiento_obras(periodo.first, periodo.last, sistema.CURRENCY.id);
                linea_obc.clase = _svr.Repo.Clases.OBC;
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

        public List<GROUP_MOVEMENT> reporte_fixed_assets(Vperiodo periodo, GENERIC_VALUE tipo, SV_SYSTEM sistema)
        {
            var resultado = new List<GROUP_MOVEMENT>();
            if (tipo.type == "TYPE_ASSET")
            {
                GENERIC_VALUE clase;
                if (tipo.id == 1)
                    clase = _svr.Repo.Clases.Activos;
                else if (tipo.id == 2)
                    clase = _svr.Repo.Clases.Intagibles;
                else
                    clase = _svr.Repo.Clases.Activos;

                var detalle = reporte_completo(periodo, clase, null, sistema);

                var agrupa = _svr.Repo.PackageClases.ByType(tipo.id);

                foreach (var PClase in agrupa)
                {
                    var linea = new GROUP_MOVEMENT();
                    linea.clase = PClase;
                    if (PClase.id == 8) //obras en construccion
                    {
                        var movimiento = movimiento_obras(periodo.first, periodo.last, sistema.CURRENCY.id);
                        //linea.zona = null;  //no tiene importancia completar este valor para este reporte
                        //linea.lugar = null; //no tiene importancia completar este valor para este reporte
                        linea.saldo_inicial_activo = movimiento.saldo_inicial;
                        //linea.adiciones_regular = movimiento.incremento;
                        linea.adiciones_obc = 0;
                        linea.incremento_obc = movimiento.incremento;
                        linea.decremento_obc = (movimiento.dec_to_activo + movimiento.dec_to_gasto)*-1;
                        //linea.valor_inicial_activo = movimiento.saldo_inicial + movimiento.incremento;
                        linea.cm_activo = 0;
                        linea.credito = 0;
                        //linea.castigo_activo = movimiento.dec_to_gasto;
                        //linea.venta_activo = movimiento.dec_to_activo;
                        linea.valor_final_activo = movimiento.saldo_final;
                        linea.depreciacion_acumulada_inicial = 0;
                        linea.cm_depreciacion = 0;
                        linea.valor_residual = 0;
                        linea.depreciacion_ejercicio = 0;
                        linea.castigo_depreciacion = 0;
                        linea.venta_depreciacion = 0;
                        linea.depreciacion_acumulada_final = 0;
                        linea.valor_libro = movimiento.saldo_final;
                    }
                    else
                    {
                        var CurrentGroup = detalle.Where(d => PClase.PACKAGE_PAIR_KINDS.Contains(d.clase.id)).DefaultIfEmpty(DETAIL_MOVEMENT.Empty());
                        linea.zona = (from a in CurrentGroup select a.zona).First();  //no tiene importancia completar este valor para este reporte
                        linea.lugar = (from a in CurrentGroup select a.subzona).First(); //no tiene importancia completar este valor para este reporte
                        linea.saldo_inicial_activo = CurrentGroup.Where(a => a.fecha_ingreso < periodo.first).Sum(a => a.valor_activo_inicial);
                        linea.adiciones_regular = CurrentGroup.Where(a => a.fecha_ingreso >= periodo.first && a.origen.code != "OBC").Sum(a => a.valor_activo_inicial);
                        linea.adiciones_obc = CurrentGroup.Where(a => a.fecha_ingreso >= periodo.first && a.origen.code == "OBC").Sum(a => a.valor_activo_inicial);
                        //linea.valor_inicial_activo = FirstGroup.Sum(a => a.valor_activo_inicial);
                        linea.valor_inicial_activo = linea.saldo_inicial_activo + linea.adiciones_regular + linea.adiciones_obc;
                        linea.cm_activo = CurrentGroup.Sum(a => a.valor_activo_cm);
                        linea.credito = CurrentGroup.Sum(a => a.credito_monto);
                        linea.castigo_activo = CurrentGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 3).Sum(a => (a.valor_activo_final)) * -1;
                        linea.venta_activo = CurrentGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 2).Sum(a => (a.valor_activo_final)) * -1;
                        linea.valor_final_activo = CurrentGroup.Where(a => a.situacion != "BAJA").Sum(a => a.valor_activo_final);
                        linea.depreciacion_acumulada_inicial = CurrentGroup.Sum(a => a.depreciacion_acum_inicial);
                        linea.cm_depreciacion = CurrentGroup.Sum(a => a.depreciacion_acum_cm);
                        linea.valor_residual = CurrentGroup.Sum(a => a.valor_residual);
                        linea.depreciacion_ejercicio = CurrentGroup.Sum(a => a.depreciacion_ejercicio);
                        linea.castigo_depreciacion = CurrentGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 3).Sum(a => a.depreciacion_acum_final) * -1;
                        linea.venta_depreciacion = CurrentGroup.Where(a => a.situacion == "BAJA" && a.vigencia.id == 2).Sum(a => a.depreciacion_acum_final) * -1;
                        linea.depreciacion_acumulada_final = CurrentGroup.Where(a => a.situacion != "BAJA").Sum(a => a.depreciacion_acum_final);
                        linea.valor_libro = CurrentGroup.Where(a => a.situacion != "BAJA").Sum(a => a.valor_libro);
                    }
                    linea.orden1 = linea.clase.code;
                    linea.orden2 = string.Empty;
                    linea.orden3 = string.Empty;
                    resultado.Add(linea);
                }
            }
            return resultado;
        }

        public List<DETAIL_OBC> salidas_obras(DateTime fecha_inicio, DateTime fecha_fin, int currency_id)
        {
            List<DETAIL_OBC> resultado = new List<DETAIL_OBC>();
            var context = _svr.DB;
            var repo = _svr.Repo;
            var data = (from head in context.ASSETS_IN_PROGRESS_HEAD
                        join detail in context.ASSETS_IN_PROGRESS_DETAIL on head.id equals detail.head_id
                        //join asset in context.BATCHS_ARTICLES on head.batch_id equals asset.id into ga
                        //from batch in ga.DefaultIfEmpty()
                        where detail.currency_id == currency_id
                        && head.tipo == "S"
                        && (head.post_date >= fecha_inicio && head.post_date <= fecha_fin)
                        && head.aproval_state_id == 2
                        select new { head, detail });
                        //select new
                        //{
                        //    head.entrada_id,
                        //    detail.amount,
                        //    aproval_state_id = (batch == null ? head.aproval_state_id : batch.aproval_state_id)
                        //}).ToList();
            foreach (var fila in data)
            {
                var detail_obc = new DETAIL_OBC();
                detail_obc.moneda = (SV_CURRENCY)fila.detail.CURRENCY;
                detail_obc.codMov = fila.head.id;
                detail_obc.description = fila.head.descrip;
                detail_obc.txFecha = fila.head.trx_date;
                detail_obc.glFecha = fila.head.post_date;
                detail_obc.zona = (SV_ZONE)(fila.head.ZONE);
                detail_obc.monto = (fila.detail.amount);
                var relDoc = fila.head.DOCS_OBC.FirstOrDefault();
                if (relDoc != null)
                {
                    detail_obc.num_documento =  relDoc.DOCUMENT.docnumber;
                    detail_obc.id_proveedor =  relDoc.DOCUMENT.proveedor_id;
                    detail_obc.nombre_proveedor = relDoc.DOCUMENT.proveedor_name;
                }
                if (fila.head.batch_id != null)
                {
                    var lote = fila.head.BATCHS_ARTICLES;
                    var parte = repo.Partes.ByLote(lote.id).Where(x => x.part_index == 0).First();
                    var FirstHead = repo.cabeceras.ByParteFirst(parte.id);
                    detail_obc.codigoArticulo = lote.id;
                    detail_obc.descripcionArticulo = lote.descrip;
                    detail_obc.zonaArticulo = repo.zonas.ById(FirstHead.zone_id);
                    detail_obc.claseArticulo = repo.Clases.ById(FirstHead.kind_id);
                    detail_obc.montoTotalArticulo = lote.initial_price;
                }
                if (fila.head.entrada_id != null)
                {
                    detail_obc.codEntrada = (int)fila.head.entrada_id;
                }
                //detail_obc.ocupado = 0;
                resultado.Add(detail_obc);
            }
            return resultado;
        }
        public List<DETAIL_OBC> salidas_obras(DateTime fecha_inicio, DateTime fecha_fin, GENERIC_VALUE currency)
        {
            return salidas_obras(fecha_inicio, fecha_fin, currency.id);
        }
        public List<DETAIL_OBC> entradas_obras(DateTime fecha_inicio, DateTime fecha_fin, int currency_id)
        {
            List<DETAIL_OBC> resultado = new List<DETAIL_OBC>();
            var context = _svr.DB;
            var data = (from head in context.ASSETS_IN_PROGRESS_HEAD
                        join detail in context.ASSETS_IN_PROGRESS_DETAIL on head.id equals detail.head_id
                        where detail.currency_id == currency_id
                        && head.tipo == "E"
                        && (head.post_date >= fecha_inicio && head.post_date <= fecha_fin)
                        && head.aproval_state_id == 2
                        select new { head, detail });
            //var documentos = (from  doc in context.DOCS_OBC
            foreach (var fila in data)
            {
                var detail_obc = new DETAIL_OBC();
                detail_obc.moneda = (SV_CURRENCY)fila.detail.CURRENCY;
                detail_obc.codMov = fila.head.id;
                detail_obc.description = fila.head.descrip;
                detail_obc.txFecha = fila.head.trx_date;
                detail_obc.glFecha = fila.head.post_date;
                detail_obc.zona = (SV_ZONE)(fila.head.ZONE);
                detail_obc.monto = (fila.detail.amount);
                var relDoc = fila.head.DOCS_OBC.First();
                if (relDoc != null)
                {
                    detail_obc.num_documento = relDoc.DOCUMENT.docnumber;
                    detail_obc.id_proveedor = relDoc.DOCUMENT.proveedor_id;
                    detail_obc.nombre_proveedor = relDoc.DOCUMENT.proveedor_name;
                }
                //detail_obc.ocupado = 0;
                resultado.Add(detail_obc);
            }
            return resultado;
        }
        public List<DETAIL_OBC> entradas_obras(DateTime fecha_inicio, DateTime fecha_fin, GENERIC_VALUE currency)
        {
            return entradas_obras(fecha_inicio, fecha_fin, currency.id);
        }
        public List<DETAIL_OBC> saldo_obras(DateTime fecha_consulta, int currency_id)
        {
            var context = _svr.DB;
            //using (var repo = new Repositories.Main(context))
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
                               select new
                               {
                                   head.entrada_id,
                                   detail.amount,
                                   aproval_state_id = (batch == null ? head.aproval_state_id : batch.aproval_state_id)
                               }).ToList();
                foreach (var fila in entradas)
                {
                    var detail_obc = new DETAIL_OBC();
                    detail_obc.moneda = (SV_CURRENCY)fila.detail.CURRENCY;
                    detail_obc.codMov = fila.head.id;
                    detail_obc.description = fila.head.descrip;
                    detail_obc.txFecha = fila.head.trx_date;
                    detail_obc.glFecha = fila.head.post_date;
                    detail_obc.zona = (SV_ZONE)(fila.head.ZONE);
                    detail_obc.monto = (fila.detail.amount);
                    detail_obc.ocupado = (salidas
                        .Where(s => s.entrada_id == fila.head.id && s.aproval_state_id == 2)
                        .Select(s => s.amount)
                        .DefaultIfEmpty(0)
                        .Sum());
                    if (detail_obc.saldo != 0)
                        resultado.Add(detail_obc);
                }
                return resultado;
            }
        }
        public List<DETAIL_OBC> saldo_obras(DateTime fecha_consulta, GENERIC_VALUE currency)
        {
            return saldo_obras(fecha_consulta, currency.id);
        }

        public GROUP_OBC movimiento_obras(DateTime desde, DateTime hasta, int currency_id)
        {
            var saldo_inicial = saldo_obras(desde, currency_id).Select(i => i.saldo);
            var saldo_final = saldo_obras(hasta, currency_id).Select(f => f.saldo);

            var context = _svr.DB;
            var repo = _svr.Repo;
            {
                var entradas = (from head in context.ASSETS_IN_PROGRESS_HEAD
                                join detail in context.ASSETS_IN_PROGRESS_DETAIL on head.id equals detail.head_id
                                where detail.currency_id == currency_id
                                && head.tipo == "E"
                                && head.post_date <= hasta && head.post_date > desde
                                && head.aproval_state_id == 2
                                select detail.amount).ToList();
                var salidas = (from head in context.ASSETS_IN_PROGRESS_HEAD
                               join detail in context.ASSETS_IN_PROGRESS_DETAIL on head.id equals detail.head_id
                               where detail.currency_id == currency_id
                               && head.tipo == "S"
                               && head.post_date <= hasta && head.post_date > desde
                               && head.aproval_state_id == 2
                               select new
                               {
                                   head_batch_id = head.batch_id,
                                   detail_amount = detail.amount
                               }
                               ).ToList();
                var resultado = new GROUP_OBC();
                resultado.saldo_inicial = saldo_inicial.Sum();
                resultado.incremento = entradas.DefaultIfEmpty(0).Sum();
                resultado.dec_to_activo = salidas.Where(s => s.head_batch_id != null).Select(s => s.detail_amount).DefaultIfEmpty(0).Sum();
                resultado.dec_to_gasto = salidas.Where(s => s.head_batch_id == null).Select(s => s.detail_amount).DefaultIfEmpty(0).Sum();
                resultado.saldo_final = saldo_final.Sum();
                return resultado;
            }
        }

        public List<DETAIL_ACCOUNT> CONTABILIZAR_GP2013(Vperiodo periodo)
        {
            var rep = _svr.Repo;
            var fuente = rep.sistemas.Default;

            var result = new List<DETAIL_ACCOUNT>();
            var vig = rep.Vigencias.All().Select(v => v.id).ToArray();
            var estado_final = base_movimiento(fuente, periodo.last, vig);
            var estado_inicial = base_movimiento(fuente, periodo.first, vig);

            var grupos = rep.contabilizar.grupos_contables();
            var lineas = rep.contabilizar.lineas();

            var mix = (from gs in grupos 
                        join lns in lineas on gs.type_acc_id equals lns.type_acc_id
                        select new{grupo = gs,linea = lns});

            foreach (var art_fin in estado_final)
            {
                DETAIL_PROCESS art_ini = estado_inicial.Where(x => x.cod_articulo == art_fin.cod_articulo && x.parte == art_fin.parte).FirstOrDefault();
                DETAIL_MOVEMENT nL = new DETAIL_MOVEMENT(art_ini, art_fin, rep.Situaciones);


                foreach (var grp in mix)
                {
                    if (grp.linea.kind_id == art_fin.clase.id && nL.situacion != "HISTORICO")
                    {
                        DETAIL_ACCOUNT salida = new DETAIL_ACCOUNT();
                        salida.periodo = periodo;
                        salida.grupo = grp.grupo.GROUP_ACCOUNT;
                        salida.tipo_cont = grp.grupo.TYPE_ACCOUNT;
                        salida.NUM_CUENTA = grp.linea.num_account;
                        salida.DSC_CUENTA = grp.linea.dsc_account;
                        salida.dim_depto = art_fin.zona.codDept;
                        salida.dim_lugar = art_fin.subzona.codPlace;

                        salida.codigo = art_fin.cod_articulo;
                        salida.parte = art_fin.parte;
                        salida.situacion = nL.situacion;
                        salida.Fclase = art_fin.clase;
                        salida.Fzona = art_fin.zona;
                        salida.Fsubzona = art_fin.subzona;

                        salida.fingreso = art_fin.fecha_ing;
                        salida.signo = grp.grupo.signo;

                        salida.FEstado = art_fin.vigencia;

                        if (salida.codigo == 549)
                            salida.valor_antes = 0;

                        salida.valor_antes = 0;
                        salida.valor_actual = 0;

                        if (salida.grupo.id == 1)
                        {
                            salida.valor_antes = (salida.fingreso > periodo.first ? 0 : nL.credito_monto) * salida.signo;
                            salida.valor_actual = (salida.fingreso > periodo.last ? 0 : nL.credito_monto) * salida.signo;
                        }
                        if (salida.grupo.id == 2)
                        {
                            salida.valor_antes = (salida.fingreso > periodo.first ? 0 : nL.depreciacion_acum_inicial) * salida.signo;
                            salida.valor_actual = (salida.fingreso > periodo.last ? 0 : nL.depreciacion_acum_final) * salida.signo;
                        }
                        //correccion monetaria activos
                        if (salida.grupo.id == 3)
                        {
                            //TODO: calcular correccion monetaria
                            //if (salida.tipo_cont.id == 1)
                            //{
                            //    salida.valor_antes = (salida.fingreso > periodo.first ? 0 : nL.depreciacion_acum_inicial) * salida.signo;
                            //    salida.valor_actual = 0;
                            //}
                            //if (salida.tipo_cont.id == 3)
                            //{
                            //    salida.valor_antes = 0;
                            //    salida.valor_actual = 0;
                            //}
                            //if (salida.tipo_cont.id == 5)
                            //{
                            salida.valor_antes = 0;
                            salida.valor_actual = 0;
                            //}
                        }
                        //correccion monetaria depreciacion
                        if (salida.grupo.id == 4)
                        {
                            //TODO: Calcular correccion monetaria de depreciacion
                            salida.valor_antes = 0;
                            salida.valor_actual = 0;
                        }
                        //Castigos
                        if (salida.grupo.id == 5)
                        {
                            salida.valor_antes = 0;
                            if (salida.FEstado.id == 3)
                            {
                                if (salida.tipo_cont.id == 1)
                                    salida.valor_actual = nL.valor_activo_final * salida.signo;
                                if (salida.tipo_cont.id == 2)
                                    salida.valor_actual = nL.depreciacion_acum_final * salida.signo;
                                if (salida.tipo_cont.id == 7)
                                    salida.valor_actual = nL.valor_libro * salida.signo;
                            }
                            else
                                salida.valor_actual = 0;
                        }
                        //Ventas
                        if (salida.grupo.id == 6)
                        {
                            salida.valor_antes = 0;
                            if (salida.FEstado.id == 2)
                            {
                                if (salida.tipo_cont.id == 1)
                                    salida.valor_actual = nL.valor_activo_final * salida.signo;
                                if (salida.tipo_cont.id == 2)
                                    salida.valor_actual = nL.depreciacion_acum_final * salida.signo;
                                if (salida.tipo_cont.id == 8)
                                    salida.valor_actual = nL.valor_libro * salida.signo;
                            }
                            else
                                salida.valor_actual = 0;
                            //salida.valor_actual = 0;
                        }
                        //Traspasos
                        if (salida.grupo.id == 7)
                        {
                            salida.valor_antes = 0;
                            if (nL.situacion == "MOVIDO")
                            {
                                if (salida.tipo_cont.id == 1)
                                    salida.valor_actual = nL.valor_activo_inicial * salida.signo;
                                if (salida.tipo_cont.id == 2)
                                    salida.valor_actual = nL.depreciacion_acum_inicial * salida.signo;
                                
                                if (salida.signo == -1)
                                {
                                    salida.dim_depto = art_ini.zona.codDept;
                                    salida.dim_lugar = art_ini.subzona.codPlace;
                                }
                            }
                            
                        }

                        if (salida.valor_actual != salida.valor_antes)
                            result.Add(salida);
                    }
                }

            }
            ServiceProcess.Tracking.ExportTo.FileText(result, "contabilizacion_detalle_revision");
            return result;

        }

        #region Fichas
        private List<DETAIL_PROCESS> get_ingreso_detail(SV_BATCH_ARTICLE lote, SV_SYSTEM sistema)
        {
            var resultado = new SINGLE_DETAIL();
            var AllAprob = _svr.Repo.EstadoAprobacion.NoDeleted.Select(ea => ea.code).ToArray();
            bool TCheckPost = false;
            DateTime fecha_min; // = DateTime.Today
            fecha_min = _svr.Repo.Partes.FirstDateLote(lote.id, lote.purchase_date);
            return _svr.Repo.get_detailed(sistema, fecha_min, lote.id, WithParameters: true, aprobados: AllAprob, CheckPost: TCheckPost);
        }

        private SINGLE_DETAIL ingreso_sistema(int article_id, SV_SYSTEM sistema)
        {
            var resultado = new SINGLE_DETAIL();
            SV_BATCH_ARTICLE lote = _svr.Repo.lotes.ById(article_id);
            var resumen = get_ingreso_detail(lote, sistema);
            if (resumen.Count > 0)
            {
                var First = resumen.First();
                resultado.set_values(First, lote);
            }
            else
                resultado = null;
            return resultado;
        }

        public SINGLE_DETAIL ingreso_financiero(int article_id)
        {
            var SysFinCLP = _svr.Repo.sistemas.FinCLP;
            return ingreso_sistema(article_id, SysFinCLP);
        }
        public SINGLE_DETAIL ingreso_ifrs(int article_id)
        {
            var SysIfrsCLP = _svr.Repo.sistemas.IfrsCLP;
            return ingreso_sistema(article_id, SysIfrsCLP);
        }
        //public bool ingreso_ifrs_check(int article_id)
        //{
        //    var resumen = ingreso_ifrs(article_id);
        //    if (resumen != null)
        //    {
        //        return true;   
        //    }
        //    return false;
        //}

        public List<SINGLE_DETAIL> ficha_ingreso1(int article_id)
        {
            var resultado = new List<SINGLE_DETAIL>();
            var AllSistemas = _svr.Repo.sistemas.All();
            foreach (var curSys in AllSistemas)
            {
                var curData = new SINGLE_DETAIL();
                SV_BATCH_ARTICLE lote = _svr.Repo.lotes.ById(article_id);                
                DateTime fecha_min; // = DateTime.Today
                fecha_min = _svr.Repo.Partes.FirstDateLote(article_id, lote.purchase_date);
                var noDeleted = _svr.Repo.EstadoAprobacion.ArrNoDeleted;
                var resumen = _svr.Repo.get_detailed(curSys, fecha_min, article_id,aprobados: noDeleted, WithParameters: true, CheckPost: false);

                if (resumen.Count > 0)
                {
                    //var First = resumen.First();
                    curData.set_values(resumen, lote);
                    //curData.set_values(First, lote);
                }
                resultado.Add(curData);
            }
            return resultado;
        }

        public List<SINGLE_DETAIL> ficha_baja(DETAIL_PROCESS info)
        {
            var resultado = new List<SINGLE_DETAIL>();
            var AllSistemas = _svr.Repo.sistemas.All();
            foreach (var curSys in AllSistemas)
            {
                var curData = new SINGLE_DETAIL();
                SV_BATCH_ARTICLE lote = _svr.Repo.lotes.ById(info.cod_articulo);
                //SV_PART part = _svr.Repo.Partes.ByLotePart(info.cod_articulo, info.parte);
                DateTime fecha_baja; // = DateTime.Today
                fecha_baja = info.fecha_inicio;
                var onlyActive = _svr.Repo.EstadoAprobacion.ArrOnlyActive;
                var resumen = _svr.Repo.get_detailed(curSys, fecha_baja, info.cod_articulo, aprobados: onlyActive, WithParameters: true, CheckPost: true);

                if (resumen.Count > 0)
                {
                    var First = resumen.First();
                    curData.set_values(First, lote);
                }
                resultado.Add(curData);
            }
            return resultado;
        }

        public List<DETAIL_MOVEMENT> ficha_cambio(DETAIL_MOVEMENT info)
        {
            var resultado = new List<DETAIL_MOVEMENT>();
            var AllSistemas = _svr.Repo.sistemas.All();
            foreach (var curSys in AllSistemas)
            {
                //var curData = new SINGLE_DETAIL();
                //SV_BATCH_ARTICLE lote = _svr.Repo.lotes.ById(info.cod_articulo);
                //SV_PART part = _svr.Repo.Partes.ByLotePart(info.cod_articulo, info.parte);
                DateTime fecha_baja; // = DateTime.Today
                fecha_baja = info.fecha_inicio;
                //var onlyActive = _svr.Repo.EstadoAprobacion.ArrOnlyActive;
                ACode.Vperiodo periodo = new Vperiodo(fecha_baja.Year, fecha_baja.Month);
                var resumen = changed_movimiento(curSys, periodo, info.cod_articulo, info.parte, DateTime.MinValue, DateTime.MaxValue, null, null);
                //var resumen = _svr.Repo.get_detailed(curSys, fecha_baja, info.cod_articulo, aprobados: onlyActive, WithParameters: true, CheckPost: true);

                if (resumen.Count > 0)
                {
                    var First = resumen.First();
                    resultado.Add(First);
                }
                
            }
            return resultado;
        }
        #endregion

        

        #region IDispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);
        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                _svr.Dispose();
            }

            disposed = true;
        }
        #endregion
    }
}
