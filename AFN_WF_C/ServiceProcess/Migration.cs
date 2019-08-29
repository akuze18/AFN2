using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;
using System.Data.Objects.DataClasses;

using Newtonsoft.Json;
using ACode;

namespace AFN_WF_C.ServiceProcess
{
    public class Migration
    {
        public RespuestaAccion CargaDatosDesdeAFN(int grupo)
        {
            var r = new RespuestaAccion();
            List<int> lotes = new List<int> { };
            for (int i = ((grupo * 100) - 99); i <= (grupo * 100); i++)
            {
                lotes.Add(i);
            }            
            try
            {
                List<AFN_INVENTARIO> origen_fin_clp;
                List<AFN_INVENTARIO_FYEN> origen_fin_yen;
                List<AFN_INVENTARIO_TRIB> origen_trib_clp;
                List<AFN_INVENTARIO_IFRS> origen_ifrs_clp;
                List<AFN_INVENTARIO_IFRS_YEN> origen_ifrs_yen;

                using (AFNoldEntities contextOld = new AFNoldEntities())
                {
                    origen_fin_clp = (from a in contextOld.AFN_INVENTARIO.Include("AFN_LOTE_ARTICULOS")
                                      where lotes.Contains(a.id_articulo)
                                      select a).ToList();
                    origen_fin_yen = (from a in contextOld.AFN_INVENTARIO_FYEN.Include("AFN_LOTE_ARTICULOS") where lotes.Contains(a.id_articulo) select a).ToList();
                    origen_trib_clp = (from a in contextOld.AFN_INVENTARIO_TRIB.Include("AFN_LOTE_ARTICULOS") where lotes.Contains(a.id_articulo) select a).ToList();
                    origen_ifrs_clp = (from a in contextOld.AFN_INVENTARIO_IFRS.Include("AFN_LOTE_ARTICULOS") where lotes.Contains(a.id_articulo) select a).ToList();
                    origen_ifrs_yen = (from a in contextOld.AFN_INVENTARIO_IFRS_YEN.Include("AFN_LOTE_ARTICULOS") where lotes.Contains(a.id_articulo) select a).ToList();
                }

                using (AFN2Entities context = new AFN2Entities())
                using(Repositories.Main Repo = new Repositories.Main(context))
                {
                    context.CommandTimeout = 3000;
                    SV_SYSTEM sistema = Repo.sistemas.Default;

                    var currentPars = (from a in context.PARTS
                                       join b in context.BATCHS_ARTICLES on a.article_id equals b.id
                                       where lotes.Contains(b.id)
                                       select new { a, b }).ToList();
                    var parametros = Repo.parametros;
                    foreach (var parte in currentPars)
                    {
                        var myFin_CLP = origen_fin_clp
                            .Where(x => x.id_articulo == parte.b.id && x.parte == parte.a.part_index);
                        foreach (var curr_fin_clp in myFin_CLP)
                        {
                            //if (parte.b.id == 2169)
                            //{
                            //    var look = 1;
                            //}
                            //compruebo si es que la cabecera ya ha sido migrada
                            DateTime WorkingDate = curr_fin_clp.fecha_inicio;
                            var find_head = (from h in context.TRANSACTIONS_HEADERS
                                             where h.article_part_id == parte.a.id &&
                                             h.trx_ini == WorkingDate
                                             select h).FirstOrDefault();
                            if (find_head == null)
                            {
                                #region cabecera
                                var find_current_valid_head = (from h in context.TRANSACTIONS_HEADERS
                                                               where h.trx_end > WorkingDate
                                                               && h.trx_ini <= WorkingDate
                                                               && h.article_part_id == parte.a.id
                                                               select h).FirstOrDefault();
                                TRANSACTION_HEADER cab = new TRANSACTION_HEADER();
                                cab.article_part_id = parte.a.id;
                                if (find_current_valid_head == null)
                                {
                                    cab.trx_ini = curr_fin_clp.fecha_inicio;
                                    cab.trx_end = (DateTime)curr_fin_clp.fecha_fin;
                                }
                                else
                                {
                                    cab.trx_ini = WorkingDate;
                                    cab.trx_end = find_current_valid_head.trx_end;
                                    find_current_valid_head.trx_end = WorkingDate;
                                    //(DateTime)curr_fin_clp.fecha_fin;
                                }
                                string ref_source;
                                if (WorkingDate == parte.b.purchase_date)
                                    ref_source = "PURCHASE";
                                else
                                {
                                    if (curr_fin_clp.cod_estado == 1)
                                    {
                                        if(curr_fin_clp.ingresado_por.Contains("SALDO_INICIAL"))
                                            ref_source = "MIG";
                                        else
                                            ref_source = "TRASPASO_MIG";
                                    }
                                    else if (curr_fin_clp.cod_estado == 2)
                                        ref_source = "VENTA_MIG";
                                    else
                                        ref_source = "CASTIGO_MIG";

                                }

                                cab.ref_source = ref_source;
                                var cZona = (from z in context.ZONES
                                             where z.codDept == curr_fin_clp.zona || z.codOld == curr_fin_clp.zona
                                             select z.id).First();
                                cab.zone_id = cZona;
                                //var cSubzone = (from sz in context.SUBZONES where sz.id == ((int)(inv_fin_clp.subzona)+1) select sz.id).First();
                                cab.subzone_id = (int)(curr_fin_clp.subzona) + 1;
                                var cClase = (from c in context.KINDS where c.cod == curr_fin_clp.clase select c.id).First();
                                cab.kind_id = cClase;
                                int cSubclase;
                                if (string.IsNullOrEmpty(curr_fin_clp.subclase)) { cSubclase = 1; }
                                else
                                {
                                    cSubclase = (from sc in context.SUBKINDS where sc.code == curr_fin_clp.subclase select sc.id).First();
                                }
                                cab.subkind_id = cSubclase;
                                var cCat = (from cat in context.CATEGORIES where cat.code == curr_fin_clp.categoria select cat.id).First();
                                cab.category_id = cCat;
                                cab.user_own = curr_fin_clp.ingresado_por;
                                //var cGest = (from ges in context.MANAGEMENTS where ges.code == inv_fin_clp.gestion select ges.id).First();
                                int cGest;
                                if (Int32.TryParse(curr_fin_clp.gestion.ToString(), out cGest))
                                { cab.manage_id = cGest; }
                                else { cab.manage_id = null; }

                                context.TRANSACTIONS_HEADERS.AddObject(cab);
                                context.SaveChanges();
                                #endregion
                                #region detalle fin clp (principal)
                                var detail = new TRANSACTION_DETAIL();
                                detail.trx_head_id = cab.id;
                                detail.system_id = sistema.id;
                                detail.validity_id = curr_fin_clp.cod_estado;
                                detail.depreciate = (bool)curr_fin_clp.se_deprecia;
                                detail.allow_credit = (curr_fin_clp.AFN_LOTE_ARTICULOS.derecho_credito == "SI");
                                context.TRANSACTIONS_DETAILS.AddObject(detail);

                                List<TRANSACTION_PARAMETER_DETAIL> listP = new List<TRANSACTION_PARAMETER_DETAIL>();
                                if (curr_fin_clp.precio_base != 0)
                                {
                                    var param1_s1 = new TRANSACTION_PARAMETER_DETAIL();
                                    param1_s1.trx_head_id = cab.id;
                                    param1_s1.system_id = sistema.id;
                                    param1_s1.paratemer_id = parametros.byCode("PB").id;
                                    param1_s1.parameter_value = curr_fin_clp.precio_base;
                                    listP.Add(param1_s1);
                                }
                                if (curr_fin_clp.depreciacion_acum != 0 && curr_fin_clp.depreciacion_acum != null)
                                {
                                    var param2_s1 = new TRANSACTION_PARAMETER_DETAIL();
                                    param2_s1.trx_head_id = cab.id;
                                    param2_s1.system_id = sistema.id;
                                    param2_s1.paratemer_id = parametros.byCode("DA").id;
                                    param2_s1.parameter_value = (decimal)curr_fin_clp.depreciacion_acum;
                                    listP.Add(param2_s1);
                                }
                                if (curr_fin_clp.deteriodo != 0 && curr_fin_clp.deteriodo != null)
                                {
                                    var param3_s1 = new TRANSACTION_PARAMETER_DETAIL();
                                    param3_s1.trx_head_id = cab.id;
                                    param3_s1.system_id = sistema.id;
                                    param3_s1.paratemer_id = parametros.byCode("DT").id;
                                    param3_s1.parameter_value = (decimal)curr_fin_clp.deteriodo;
                                    listP.Add(param3_s1);
                                }
                                if (curr_fin_clp.valor_residual != 0 && curr_fin_clp.valor_residual != null)
                                {
                                    var param4_s1 = new TRANSACTION_PARAMETER_DETAIL();
                                    param4_s1.trx_head_id = cab.id;
                                    param4_s1.system_id = sistema.id;
                                    param4_s1.paratemer_id = parametros.byCode("VR").id;
                                    param4_s1.parameter_value = -(decimal)curr_fin_clp.valor_residual*-1;
                                    listP.Add(param4_s1);
                                }
                                if (curr_fin_clp.vida_util_base != 0 && curr_fin_clp.vida_util_base != null)
                                {
                                    var param5_s1 = new TRANSACTION_PARAMETER_DETAIL();
                                    param5_s1.trx_head_id = cab.id;
                                    param5_s1.system_id = sistema.id;
                                    param5_s1.paratemer_id = parametros.byCode("VUB").id;
                                    param5_s1.parameter_value = (decimal)curr_fin_clp.vida_util_base;
                                    listP.Add(param5_s1);
                                }
                                #endregion
                                #region detalle fin yen

                                AFN_INVENTARIO_FYEN curr_fin_yen = (from FY in origen_fin_yen where FY.id_articulo == parte.b.id && FY.parte == parte.a.part_index && FY.fecha_inicio == cab.trx_ini select FY).FirstOrDefault();
                                if (curr_fin_yen != null)
                                {
                                    SV_SYSTEM sistema2 = Repo.sistemas.FinYEN;
                                    var detail2 = new TRANSACTION_DETAIL();
                                    detail2.trx_head_id = cab.id;
                                    detail2.system_id = sistema2.id;
                                    detail2.validity_id = curr_fin_yen.cod_estado;
                                    detail2.depreciate = (bool)curr_fin_yen.se_deprecia;
                                    detail2.allow_credit = (curr_fin_yen.AFN_LOTE_ARTICULOS.derecho_credito == "SI");
                                    context.TRANSACTIONS_DETAILS.AddObject(detail2);

                                    if (curr_fin_yen.precio_base != 0)
                                    {
                                        var param1_s2 = new TRANSACTION_PARAMETER_DETAIL();
                                        param1_s2.trx_head_id = cab.id;
                                        param1_s2.system_id = sistema2.id;
                                        param1_s2.paratemer_id = parametros.byCode("PB").id;
                                        param1_s2.parameter_value = curr_fin_yen.precio_base;
                                        listP.Add(param1_s2);
                                    }
                                    if (curr_fin_yen.depreciacion_acum != 0 && curr_fin_yen.depreciacion_acum != null)
                                    {
                                        var param2_s2 = new TRANSACTION_PARAMETER_DETAIL();
                                        param2_s2.trx_head_id = cab.id;
                                        param2_s2.system_id = sistema2.id;
                                        param2_s2.paratemer_id = parametros.byCode("DA").id;
                                        param2_s2.parameter_value = (decimal)curr_fin_yen.depreciacion_acum;
                                        listP.Add(param2_s2);
                                    }
                                    if (curr_fin_yen.deteriodo != 0 && curr_fin_yen.deteriodo != null)
                                    {
                                        var param3_s2 = new TRANSACTION_PARAMETER_DETAIL();
                                        param3_s2.trx_head_id = cab.id;
                                        param3_s2.system_id = sistema2.id;
                                        param3_s2.paratemer_id = parametros.byCode("DT").id;
                                        param3_s2.parameter_value = (decimal)curr_fin_yen.deteriodo;
                                        listP.Add(param3_s2);
                                    }
                                    if (curr_fin_yen.valor_residual != 0 && curr_fin_yen.valor_residual != null)
                                    {
                                        var param4_s2 = new TRANSACTION_PARAMETER_DETAIL();
                                        param4_s2.trx_head_id = cab.id;
                                        param4_s2.system_id = sistema2.id;
                                        param4_s2.paratemer_id = parametros.byCode("VR").id;
                                        param4_s2.parameter_value = -(decimal)curr_fin_yen.valor_residual;
                                        listP.Add(param4_s2);
                                    }
                                    if (curr_fin_yen.vida_util_base != 0 && curr_fin_yen.vida_util_base != null)
                                    {
                                        var param5_s2 = new TRANSACTION_PARAMETER_DETAIL();
                                        param5_s2.trx_head_id = cab.id;
                                        param5_s2.system_id = sistema2.id;
                                        param5_s2.paratemer_id = parametros.byCode("VUB").id;
                                        param5_s2.parameter_value = (decimal)curr_fin_yen.vida_util_base;
                                        listP.Add(param5_s2);
                                    }
                                }

                                #endregion
                                #region detalle trib clp

                                AFN_INVENTARIO_TRIB curr_trib_clp = (from FY in origen_trib_clp where FY.id_articulo == parte.b.id && FY.parte == parte.a.part_index && FY.fecha_inicio == cab.trx_ini select FY).FirstOrDefault();
                                if (curr_trib_clp != null)
                                {
                                    SV_SYSTEM sistema3 = Repo.sistemas.TribCLP;
                                    var detail3 = new TRANSACTION_DETAIL();
                                    detail3.trx_head_id = cab.id;
                                    detail3.system_id = sistema3.id;
                                    detail3.validity_id = curr_trib_clp.cod_estado;
                                    detail3.depreciate = (bool)curr_trib_clp.se_deprecia;
                                    detail3.allow_credit = (curr_trib_clp.AFN_LOTE_ARTICULOS.derecho_credito == "SI");
                                    context.TRANSACTIONS_DETAILS.AddObject(detail3);

                                    if (curr_trib_clp.precio_base != 0)
                                    {
                                        var param1_s3 = new TRANSACTION_PARAMETER_DETAIL();
                                        param1_s3.trx_head_id = cab.id;
                                        param1_s3.system_id = sistema3.id;
                                        param1_s3.paratemer_id = parametros.byCode("PB").id;
                                        param1_s3.parameter_value = curr_trib_clp.precio_base;
                                        listP.Add(param1_s3);
                                    }
                                    if (curr_trib_clp.depreciacion_acum != 0 && curr_trib_clp.depreciacion_acum != null)
                                    {
                                        var param2_s3 = new TRANSACTION_PARAMETER_DETAIL();
                                        param2_s3.trx_head_id = cab.id;
                                        param2_s3.system_id = sistema3.id;
                                        param2_s3.paratemer_id = parametros.byCode("DA").id;
                                        param2_s3.parameter_value = (decimal)curr_trib_clp.depreciacion_acum;
                                        listP.Add(param2_s3);
                                    }
                                    if (curr_trib_clp.deteriodo != 0 && curr_trib_clp.deteriodo != null)
                                    {
                                        var param3_s3 = new TRANSACTION_PARAMETER_DETAIL();
                                        param3_s3.trx_head_id = cab.id;
                                        param3_s3.system_id = sistema3.id;
                                        param3_s3.paratemer_id = parametros.byCode("DT").id;
                                        param3_s3.parameter_value = (decimal)curr_trib_clp.deteriodo;
                                        listP.Add(param3_s3);
                                    }
                                    if (curr_trib_clp.valor_residual != 0 && curr_trib_clp.valor_residual != null)
                                    {
                                        var param4_s3 = new TRANSACTION_PARAMETER_DETAIL();
                                        param4_s3.trx_head_id = cab.id;
                                        param4_s3.system_id = sistema3.id;
                                        param4_s3.paratemer_id = parametros.byCode("VR").id;
                                        param4_s3.parameter_value = -(decimal)curr_trib_clp.valor_residual;
                                        listP.Add(param4_s3);
                                    }
                                    if (curr_trib_clp.vida_util_base != 0 && curr_trib_clp.vida_util_base != null)
                                    {
                                        var param5_s3 = new TRANSACTION_PARAMETER_DETAIL();
                                        param5_s3.trx_head_id = cab.id;
                                        param5_s3.system_id = sistema3.id;
                                        param5_s3.paratemer_id = parametros.byCode("VUB").id;
                                        param5_s3.parameter_value = (decimal)curr_trib_clp.vida_util_base;
                                        listP.Add(param5_s3);
                                    }
                                }

                                #endregion
                                #region detalle ifrs clp

                                AFN_INVENTARIO_IFRS curr_ifrs_clp = (from FY in origen_ifrs_clp where FY.id_articulo == parte.b.id && FY.parte == parte.a.part_index && FY.fecha_inicio == cab.trx_ini select FY).FirstOrDefault();
                                if (curr_ifrs_clp != null)
                                {
                                    SV_SYSTEM sistema4 = Repo.sistemas.IfrsCLP;
                                    var detail4 = new TRANSACTION_DETAIL();
                                    detail4.trx_head_id = cab.id;
                                    detail4.system_id = sistema4.id;
                                    detail4.validity_id = curr_ifrs_clp.cod_estado;
                                    detail4.depreciate = (bool)curr_ifrs_clp.se_deprecia;
                                    detail4.allow_credit = (curr_ifrs_clp.AFN_LOTE_ARTICULOS.derecho_credito == "SI");
                                    context.TRANSACTIONS_DETAILS.AddObject(detail4);

                                    if (curr_ifrs_clp.precio_base != 0)
                                    {
                                        var param1_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param1_s4.trx_head_id = cab.id;
                                        param1_s4.system_id = sistema4.id;
                                        param1_s4.paratemer_id = parametros.byCode("PB").id;
                                        param1_s4.parameter_value = curr_ifrs_clp.precio_base;
                                        listP.Add(param1_s4);
                                    }
                                    if (curr_ifrs_clp.depreciacion_acum != 0 && curr_ifrs_clp.depreciacion_acum != null)
                                    {
                                        var param2_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param2_s4.trx_head_id = cab.id;
                                        param2_s4.system_id = sistema4.id;
                                        param2_s4.paratemer_id = parametros.byCode("DA").id;
                                        param2_s4.parameter_value = (decimal)curr_ifrs_clp.depreciacion_acum;
                                        listP.Add(param2_s4);
                                    }
                                    if (curr_ifrs_clp.deteriodo != 0 && curr_ifrs_clp.deteriodo != null)
                                    {
                                        var param3_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param3_s4.trx_head_id = cab.id;
                                        param3_s4.system_id = sistema4.id;
                                        param3_s4.paratemer_id = parametros.byCode("DT").id;
                                        param3_s4.parameter_value = (decimal)curr_ifrs_clp.deteriodo;
                                        listP.Add(param3_s4);
                                    }
                                    if (curr_ifrs_clp.valor_residual != 0 && curr_ifrs_clp.valor_residual != null)
                                    {
                                        var param4_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param4_s4.trx_head_id = cab.id;
                                        param4_s4.system_id = sistema4.id;
                                        param4_s4.paratemer_id = parametros.byCode("VR").id;
                                        param4_s4.parameter_value = -(decimal)curr_ifrs_clp.valor_residual;
                                        listP.Add(param4_s4);
                                    }
                                    if (curr_ifrs_clp.vida_util_base != 0 && curr_ifrs_clp.vida_util_base != null)
                                    {
                                        var param5_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param5_s4.trx_head_id = cab.id;
                                        param5_s4.system_id = sistema4.id;
                                        param5_s4.paratemer_id = parametros.byCode("VUB").id;
                                        param5_s4.parameter_value = (decimal)curr_ifrs_clp.vida_util_base;
                                        listP.Add(param5_s4);
                                    }
                                    if (curr_ifrs_clp.preparacion != 0 && curr_ifrs_clp.preparacion != null)
                                    {
                                        var param6_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param6_s4.trx_head_id = cab.id;
                                        param6_s4.system_id = sistema4.id;
                                        param6_s4.paratemer_id = parametros.byCode("PREP").id;
                                        param6_s4.parameter_value = (decimal)curr_ifrs_clp.preparacion;
                                        listP.Add(param6_s4);
                                    }
                                    if (curr_ifrs_clp.transporte != 0 && curr_ifrs_clp.transporte != null)
                                    {
                                        var param7_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param7_s4.trx_head_id = cab.id;
                                        param7_s4.system_id = sistema4.id;
                                        param7_s4.paratemer_id = parametros.byCode("TRAN").id;
                                        param7_s4.parameter_value = (decimal)curr_ifrs_clp.transporte;
                                        listP.Add(param7_s4);
                                    }
                                    if (curr_ifrs_clp.montaje != 0 && curr_ifrs_clp.montaje != null)
                                    {
                                        var param8_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param8_s4.trx_head_id = cab.id;
                                        param8_s4.system_id = sistema4.id;
                                        param8_s4.paratemer_id = parametros.byCode("MON").id;
                                        param8_s4.parameter_value = (decimal)curr_ifrs_clp.montaje;
                                        listP.Add(param8_s4);
                                    }
                                    if (curr_ifrs_clp.desmantel != 0 && curr_ifrs_clp.desmantel != null)
                                    {
                                        var param9_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param9_s4.trx_head_id = cab.id;
                                        param9_s4.system_id = sistema4.id;
                                        param9_s4.paratemer_id = parametros.byCode("DESM").id;
                                        param9_s4.parameter_value = (decimal)curr_ifrs_clp.desmantel;
                                        listP.Add(param9_s4);
                                    }
                                    if (curr_ifrs_clp.honorario != 0 && curr_ifrs_clp.honorario != null)
                                    {
                                        var param10_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param10_s4.trx_head_id = cab.id;
                                        param10_s4.system_id = sistema4.id;
                                        param10_s4.paratemer_id = parametros.byCode("HON").id;
                                        param10_s4.parameter_value = (decimal)curr_ifrs_clp.honorario;
                                        listP.Add(param10_s4);
                                    }
                                    if (curr_ifrs_clp.revalorizacion != 0 && curr_ifrs_clp.revalorizacion != null)
                                    {
                                        var param11_s4 = new TRANSACTION_PARAMETER_DETAIL();
                                        param11_s4.trx_head_id = cab.id;
                                        param11_s4.system_id = sistema4.id;
                                        param11_s4.paratemer_id = parametros.byCode("REVAL").id;
                                        param11_s4.parameter_value = (decimal)curr_ifrs_clp.revalorizacion;
                                        listP.Add(param11_s4);
                                    }
                                }

                                #endregion
                                #region detalle ifrs yen

                                AFN_INVENTARIO_IFRS_YEN curr_ifrs_yen = (from FY in origen_ifrs_yen where FY.id_articulo == parte.b.id && FY.parte == parte.a.part_index && FY.fecha_inicio == cab.trx_ini select FY).FirstOrDefault();
                                if (curr_ifrs_yen != null)
                                {
                                    SV_SYSTEM sistema5 = Repo.sistemas.IfrsYEN;
                                    var detail5 = new TRANSACTION_DETAIL();
                                    detail5.trx_head_id = cab.id;
                                    detail5.system_id = sistema5.id;
                                    detail5.validity_id = curr_ifrs_yen.cod_estado;
                                    detail5.depreciate = (bool)curr_ifrs_yen.se_deprecia;
                                    detail5.allow_credit = (curr_ifrs_yen.AFN_LOTE_ARTICULOS.derecho_credito == "SI");
                                    context.TRANSACTIONS_DETAILS.AddObject(detail5);

                                    if (curr_ifrs_yen.precio_base != 0)
                                    {
                                        var param1_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param1_s5.trx_head_id = cab.id;
                                        param1_s5.system_id = sistema5.id;
                                        param1_s5.paratemer_id = parametros.byCode("PB").id;
                                        param1_s5.parameter_value = curr_ifrs_yen.precio_base;
                                        listP.Add(param1_s5);
                                    }
                                    if (curr_ifrs_yen.depreciacion_acum != 0)
                                    {
                                        var param2_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param2_s5.trx_head_id = cab.id;
                                        param2_s5.system_id = sistema5.id;
                                        param2_s5.paratemer_id = parametros.byCode("DA").id;
                                        param2_s5.parameter_value = (decimal)curr_ifrs_yen.depreciacion_acum;
                                        listP.Add(param2_s5);
                                    }
                                    if (curr_ifrs_yen.deteriodo != 0 && curr_ifrs_yen.deteriodo != null)
                                    {
                                        var param3_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param3_s5.trx_head_id = cab.id;
                                        param3_s5.system_id = sistema5.id;
                                        param3_s5.paratemer_id = parametros.byCode("DT").id;
                                        param3_s5.parameter_value = (decimal)curr_ifrs_yen.deteriodo;
                                        listP.Add(param3_s5);
                                    }
                                    if (curr_ifrs_yen.valor_residual != 0)
                                    {
                                        var param4_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param4_s5.trx_head_id = cab.id;
                                        param4_s5.system_id = sistema5.id;
                                        param4_s5.paratemer_id = parametros.byCode("VR").id;
                                        param4_s5.parameter_value = -(decimal)curr_ifrs_yen.valor_residual;
                                        listP.Add(param4_s5);
                                    }
                                    if (curr_ifrs_yen.vida_util_base != 0)
                                    {
                                        var param5_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param5_s5.trx_head_id = cab.id;
                                        param5_s5.system_id = sistema5.id;
                                        param5_s5.paratemer_id = parametros.byCode("VUB").id;
                                        param5_s5.parameter_value = (decimal)curr_ifrs_yen.vida_util_base;
                                        listP.Add(param5_s5);
                                    }
                                    if (curr_ifrs_yen.preparacion != 0 && curr_ifrs_yen.preparacion != null)
                                    {
                                        var param6_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param6_s5.trx_head_id = cab.id;
                                        param6_s5.system_id = sistema5.id;
                                        param6_s5.paratemer_id = parametros.byCode("PREP").id;
                                        param6_s5.parameter_value = (decimal)curr_ifrs_yen.preparacion;
                                        listP.Add(param6_s5);
                                    }
                                    if (curr_ifrs_yen.transporte != 0 && curr_ifrs_yen.transporte != null)
                                    {
                                        var param7_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param7_s5.trx_head_id = cab.id;
                                        param7_s5.system_id = sistema5.id;
                                        param7_s5.paratemer_id = parametros.byCode("TRAN").id;
                                        param7_s5.parameter_value = (decimal)curr_ifrs_yen.transporte;
                                        listP.Add(param7_s5);
                                    }
                                    if (curr_ifrs_yen.montaje != 0 && curr_ifrs_yen.montaje != null)
                                    {
                                        var param8_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param8_s5.trx_head_id = cab.id;
                                        param8_s5.system_id = sistema5.id;
                                        param8_s5.paratemer_id = parametros.byCode("MON").id;
                                        param8_s5.parameter_value = (decimal)curr_ifrs_yen.montaje;
                                        listP.Add(param8_s5);
                                    }
                                    if (curr_ifrs_yen.desmantel != 0 && curr_ifrs_yen.desmantel != null)
                                    {
                                        var param9_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param9_s5.trx_head_id = cab.id;
                                        param9_s5.system_id = sistema5.id;
                                        param9_s5.paratemer_id = parametros.byCode("DESM").id;
                                        param9_s5.parameter_value = (decimal)curr_ifrs_yen.desmantel;
                                        listP.Add(param9_s5);
                                    }
                                    if (curr_ifrs_yen.honorario != 0 && curr_ifrs_yen.honorario != null)
                                    {
                                        var param10_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param10_s5.trx_head_id = cab.id;
                                        param10_s5.system_id = sistema5.id;
                                        param10_s5.paratemer_id = parametros.byCode("HON").id;
                                        param10_s5.parameter_value = (decimal)curr_ifrs_yen.honorario;
                                        listP.Add(param10_s5);
                                    }
                                    if (curr_ifrs_yen.revalorizacion != 0 && curr_ifrs_yen.revalorizacion != null)
                                    {
                                        var param11_s5 = new TRANSACTION_PARAMETER_DETAIL();
                                        param11_s5.trx_head_id = cab.id;
                                        param11_s5.system_id = sistema5.id;
                                        param11_s5.paratemer_id = parametros.byCode("REVAL").id;
                                        param11_s5.parameter_value = (decimal)curr_ifrs_yen.revalorizacion;
                                        listP.Add(param11_s5);
                                    }
                                }

                                #endregion

                                foreach (var p in listP) { context.TRANSACTIONS_PARAMETERS_DETAILS.AddObject(p); }
                                context.SaveChanges();
                            }
                        }
                    }
                }
                r.codigo = 1;
                r.descripcion = "OK";
            }
            catch (Exception ex)
            {
                r.codigo = -1;
                r.descripcion = JsonConvert.SerializeObject(ex);
            }
            return r;
        }
        public RespuestaAccion CargaDepreciacion(int cal_year, int calc_month)
        {
            var respuesta = new RespuestaAccion();
            DateTime F1, F2;
            try
            {

                F1 = DateTime.Now;
                List<AFN_INVENTARIO> origen_fin_clp;

                List<BASE_LOCAL_AFN_CALC> Fin_clp_depre = new List<BASE_LOCAL_AFN_CALC>();
                List<BASE_LOCAL_AFN_CALC> Trib_depre = new List<BASE_LOCAL_AFN_CALC>();
                List<BASE_LOCAL_AFN_CALC> Fin_yen_dep = new List<BASE_LOCAL_AFN_CALC>();
                List<BASE_GLOBAL_AFN_CALC> Ifrs_clp_dep = new List<BASE_GLOBAL_AFN_CALC>();
                List<BASE_GLOBAL_AFN_CALC> Ifrs_yen_dep = new List<BASE_GLOBAL_AFN_CALC>();

                #region Datos AFN
                using (AFNoldEntities contextOld = new AFNoldEntities())
                {
                    origen_fin_clp = (from a in contextOld.AFN_INVENTARIO.Include("AFN_LOTE_ARTICULOS")
                                      //where lotes.Contains(a.id_articulo)
                                      select a).ToList();
                }
                #endregion

                var per = new ACode.Vperiodo(cal_year, calc_month);
                using (AFN2Entities context = new AFN2Entities())
                using (Repositories.Main repo = new Repositories.Main(context))
                {

                    
                    Fin_clp_depre = (from a in context.BASE_LOCAL_AFN_CALC
                                        where a.fecha_corte == per.last && a.fuente == "F"
                                        select a).ToList();
                    Trib_depre = (from a in context.BASE_LOCAL_AFN_CALC
                                        where a.fecha_corte == per.last && a.fuente == "T"
                                        select a).ToList();
                    Fin_yen_dep = (from a in context.BASE_LOCAL_AFN_CALC
                                        where a.fecha_corte == per.last && a.fuente == "Y"
                                        select a).ToList();
                    Ifrs_clp_dep = (from b in context.BASE_GLOBAL_AFN_CALC
                                        where b.fecha_corte == per.last && b.fuente == "GC"
                                        select b).ToList();
                    Ifrs_yen_dep = (from b in context.BASE_GLOBAL_AFN_CALC
                                        where b.fecha_corte == per.last && b.fuente == "GY"
                                        select b).ToList();
                    
                    context.CommandTimeout = 3000;

                    var currentParts = (from a in context.PARTS
                                       join b in context.BATCHS_ARTICLES on a.article_id equals b.id
                                       //where lotes.Contains(b.id)
                                       select new { a, b }).ToList();
                    var parametros = repo.parametros;// new Repositories.PARAMETERS(context.PARAMETERS);
                    foreach (var parte in currentParts)
                    {
                        var LineaFinCLP = Fin_clp_depre.Where(
                            x => x.cod_articulo == parte.b.id && x.parte == parte.a.part_index).FirstOrDefault();
                        if (LineaFinCLP != null && LineaFinCLP.cod_est == 1)
                        {
                            var value_details = new EntityCollection<TRANSACTION_DETAIL>();
                            var value_parameters = new EntityCollection<TRANSACTION_PARAMETER_DETAIL>();
                            var fin_clp_old = origen_fin_clp.Where(x => x.id_articulo == parte.b.id).FirstOrDefault();
                            bool derecho_cred = (fin_clp_old.AFN_LOTE_ARTICULOS.derecho_credito == "SI");
                            #region detalle y parametros financiero clp (principal)

                            var detail = new TRANSACTION_DETAIL();
                            detail.system_id = repo.sistemas.Default.id;
                            detail.validity_id = LineaFinCLP.cod_est;
                            detail.depreciate = LineaFinCLP.se_deprecia;
                            detail.allow_credit = derecho_cred;
                            value_details.Add(detail);

                            if (LineaFinCLP.valor_anterior != 0)
                            {
                                var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_new.system_id = repo.sistemas.Default.id;
                                param_new.paratemer_id = parametros.PrecioBase.id;
                                param_new.parameter_value = LineaFinCLP.valor_anterior / LineaFinCLP.cantidad;
                                value_parameters.Add(param_new);
                            }
                            if (LineaFinCLP.cred_adi != 0)
                            {
                                var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_new.system_id = repo.sistemas.Default.id;
                                param_new.paratemer_id = parametros.Credito.id;
                                param_new.parameter_value = LineaFinCLP.cred_adi / LineaFinCLP.cantidad;
                                value_parameters.Add(param_new);
                            }
                            if (LineaFinCLP.DA_AF != 0)
                            {
                                var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_new.system_id = repo.sistemas.Default.id;
                                param_new.paratemer_id = parametros.DepreciacionAcum.id;
                                param_new.parameter_value = LineaFinCLP.DA_AF / LineaFinCLP.cantidad;
                                value_parameters.Add(param_new);
                            }
                            if (LineaFinCLP.deter != 0)
                            {
                                var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_new.system_id = repo.sistemas.Default.id;
                                param_new.paratemer_id = parametros.Deterioro.id;
                                param_new.parameter_value = LineaFinCLP.deter / LineaFinCLP.cantidad;
                                value_parameters.Add(param_new);
                            }
                            if (LineaFinCLP.val_res != 0)
                            {
                                var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_new.system_id = repo.sistemas.Default.id;
                                param_new.paratemer_id = parametros.ValorResidual.id;
                                param_new.parameter_value = LineaFinCLP.val_res / LineaFinCLP.cantidad;
                                value_parameters.Add(param_new);
                            }
                            if (LineaFinCLP.vu_resi != 0)
                            {
                                var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                param_new.system_id = repo.sistemas.Default.id;
                                param_new.paratemer_id = parametros.VidaUtil.id;
                                param_new.parameter_value = (decimal)LineaFinCLP.vu_resi;
                                value_parameters.Add(param_new);
                            }
                            #endregion  
                            #region detalle y parametros financiero yen
                            var LineaFinYEN = Fin_yen_dep.Where(
                                x => x.cod_articulo == parte.b.id && x.parte == parte.a.part_index).FirstOrDefault();
                            if (LineaFinYEN != null)
                            {
                                var detail_1 = new TRANSACTION_DETAIL();
                                detail_1.system_id = repo.sistemas.FinYEN.id;
                                detail_1.validity_id = LineaFinYEN.cod_est;
                                detail_1.depreciate = LineaFinYEN.se_deprecia;
                                detail_1.allow_credit = derecho_cred;
                                value_details.Add(detail_1);

                                if (LineaFinYEN.valor_anterior != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = repo.sistemas.FinYEN.id;
                                    param_new.paratemer_id = parametros.PrecioBase.id;
                                    param_new.parameter_value = LineaFinYEN.valor_anterior / LineaFinYEN.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (LineaFinYEN.cred_adi != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = repo.sistemas.FinYEN.id;
                                    param_new.paratemer_id = parametros.Credito.id;
                                    param_new.parameter_value = LineaFinYEN.cred_adi / LineaFinYEN.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (LineaFinYEN.DA_AF != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = repo.sistemas.FinYEN.id;
                                    param_new.paratemer_id = parametros.DepreciacionAcum.id;
                                    param_new.parameter_value = LineaFinYEN.DA_AF / LineaFinYEN.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (LineaFinYEN.deter != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = repo.sistemas.FinYEN.id;
                                    param_new.paratemer_id = parametros.Deterioro.id;
                                    param_new.parameter_value = LineaFinYEN.deter / LineaFinYEN.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (LineaFinYEN.val_res != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = repo.sistemas.FinYEN.id;
                                    param_new.paratemer_id = parametros.ValorResidual.id;
                                    param_new.parameter_value = LineaFinYEN.val_res / LineaFinYEN.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (LineaFinYEN.vu_resi != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = repo.sistemas.FinYEN.id;
                                    param_new.paratemer_id = parametros.VidaUtil.id;
                                    param_new.parameter_value = (decimal)LineaFinYEN.vu_resi;
                                    value_parameters.Add(param_new);
                                }
                            }
                            #endregion
                            #region detalle y parametros tributario clp
                            var LineaTribCLP = Trib_depre.Where(
                                x => x.cod_articulo == parte.b.id && x.parte == parte.a.part_index).FirstOrDefault();
                            if (LineaTribCLP != null)
                            {
                                var detail_1 = new TRANSACTION_DETAIL();
                                var proc_linea = LineaTribCLP;
                                var proc_system = repo.sistemas.TribCLP;
                                detail_1.system_id = proc_system.id;
                                detail_1.validity_id = proc_linea.cod_est;
                                detail_1.depreciate = proc_linea.se_deprecia;
                                detail_1.allow_credit = derecho_cred;
                                value_details.Add(detail_1);

                                if (proc_linea.valor_anterior != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.PrecioBase.id;
                                    param_new.parameter_value = proc_linea.valor_anterior / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.cred_adi != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Credito.id;
                                    param_new.parameter_value = proc_linea.cred_adi / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.DA_AF != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.DepreciacionAcum.id;
                                    param_new.parameter_value = proc_linea.DA_AF / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.deter != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Deterioro.id;
                                    param_new.parameter_value = proc_linea.deter / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.val_res != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.ValorResidual.id;
                                    param_new.parameter_value = proc_linea.val_res / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.vu_resi != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.VidaUtil.id;
                                    param_new.parameter_value = (decimal)proc_linea.vu_resi;
                                    value_parameters.Add(param_new);
                                }
                            }
                            #endregion
                            #region detalle y parametros IFRS clp
                            var LineaIFRSCLP = Ifrs_clp_dep.Where(
                                x => x.cod_articulo == parte.b.id && x.parte == parte.a.part_index).FirstOrDefault();
                            if (LineaIFRSCLP != null)
                            {
                                var detail_1 = new TRANSACTION_DETAIL();
                                var proc_linea = LineaIFRSCLP;
                                var proc_system = repo.sistemas.IfrsCLP;
                                detail_1.system_id = proc_system.id;
                                detail_1.validity_id = proc_linea.cod_est;
                                detail_1.depreciate = proc_linea.se_deprecia;
                                detail_1.allow_credit = derecho_cred;
                                value_details.Add(detail_1);

                                if (proc_linea.valor_anterior != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.PrecioBase.id;
                                    param_new.parameter_value = proc_linea.valor_anterior / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.cred_adi != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Credito.id;
                                    param_new.parameter_value = proc_linea.cred_adi / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.DA_AF != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.DepreciacionAcum.id;
                                    param_new.parameter_value = proc_linea.DA_AF / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.deter != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Deterioro.id;
                                    param_new.parameter_value = proc_linea.deter / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.val_res != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.ValorResidual.id;
                                    param_new.parameter_value = proc_linea.val_res / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.vu_resi != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.VidaUtil.id;
                                    param_new.parameter_value = proc_linea.vu_resi;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.preparacion != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Preparacion.id;
                                    param_new.parameter_value = proc_linea.preparacion / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.desmantel != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Desmantelamiento.id;
                                    param_new.parameter_value = proc_linea.desmantel / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.transporte != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Transporte.id;
                                    param_new.parameter_value = proc_linea.transporte / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.montaje != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Montaje.id;
                                    param_new.parameter_value = proc_linea.montaje / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.honorario != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Honorario.id;
                                    param_new.parameter_value = proc_linea.honorario / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.revaluacion != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Revalorizacion.id;
                                    param_new.parameter_value = proc_linea.revaluacion / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                            }
                            #endregion
                            #region detalle y parametros IFRS yen
                            var LineaIFRSYEN = Ifrs_yen_dep.Where(
                                x => x.cod_articulo == parte.b.id && x.parte == parte.a.part_index).FirstOrDefault();
                            if (LineaIFRSYEN != null)
                            {
                                var proc_linea = LineaIFRSYEN;
                                var proc_system = repo.sistemas.IfrsYEN;
                                var detail_1 = new TRANSACTION_DETAIL();
                                detail_1.system_id = proc_system.id;
                                detail_1.validity_id = proc_linea.cod_est;
                                detail_1.depreciate = proc_linea.se_deprecia;
                                detail_1.allow_credit = derecho_cred;
                                value_details.Add(detail_1);

                                if (proc_linea.valor_anterior != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.PrecioBase.id;
                                    param_new.parameter_value = proc_linea.valor_anterior / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.cred_adi != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Credito.id;
                                    param_new.parameter_value = proc_linea.cred_adi / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.DA_AF != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.DepreciacionAcum.id;
                                    param_new.parameter_value = proc_linea.DA_AF / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.deter != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Deterioro.id;
                                    param_new.parameter_value = proc_linea.deter / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.val_res != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.ValorResidual.id;
                                    param_new.parameter_value = proc_linea.val_res / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.vu_resi != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.VidaUtil.id;
                                    param_new.parameter_value = proc_linea.vu_resi;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.preparacion != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Preparacion.id;
                                    param_new.parameter_value = proc_linea.preparacion / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.desmantel != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Desmantelamiento.id;
                                    param_new.parameter_value = proc_linea.desmantel / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.transporte != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Transporte.id;
                                    param_new.parameter_value = proc_linea.transporte / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.montaje != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Montaje.id;
                                    param_new.parameter_value = proc_linea.montaje / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.honorario != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Honorario.id;
                                    param_new.parameter_value = proc_linea.honorario / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                                if (proc_linea.revaluacion != 0)
                                {
                                    var param_new = new TRANSACTION_PARAMETER_DETAIL();
                                    param_new.system_id = proc_system.id;
                                    param_new.paratemer_id = parametros.Revalorizacion.id;
                                    param_new.parameter_value = proc_linea.revaluacion / proc_linea.cantidad;
                                    value_parameters.Add(param_new);
                                }
                            }
                            #endregion

                            #region cabecera

                            SV_TRANSACTION_HEADER view_cab_origin = repo.cabeceras.byPartFecha(parte.a.id, per.last);
                            TRANSACTION_HEADER cab_origin = (from c in context.TRANSACTIONS_HEADERS where c.id == view_cab_origin.id select c).First();
                            DateTime f_fin_origin = cab_origin.trx_end;
                            cab_origin.trx_end = per.last;

                            TRANSACTION_HEADER cab_new = new TRANSACTION_HEADER();
                            cab_new.article_part_id = parte.a.id;
                            cab_new.trx_ini = per.last;
                            cab_new.trx_end = f_fin_origin;
                            cab_new.ref_source = "DEP_MIG";
                            var cZona = repo.zonas.ByCode(LineaFinCLP.zona); 
                            cab_new.zone_id = cZona.id;
                            //var cSubzone = (from sz in context.SUBZONES where sz.id == ((int)(inv_fin_clp.subzona)+1) select sz.id).First();
                            cab_new.subzone_id = (int)(LineaFinCLP.cod_subzona) + 1;
                            var cClase = repo.Clases.ByCode(LineaFinCLP.clase);
                            cab_new.kind_id = cClase.id;

                            var cSubclase = repo.subclases.ByCode(LineaFinCLP.cod_subclase);
                            cab_new.subkind_id = cSubclase.id;
                            var cCat = repo.categorias.ByCode(LineaFinCLP.categoria);
                            cab_new.category_id = cCat.id;
                            cab_new.user_own = "DEP_MIG";
                            //var cGest = (from ges in context.MANAGEMENTS where ges.code == inv_fin_clp.gestion select ges.id).First();
                            int cGest;
                            if (Int32.TryParse(LineaFinCLP.id_gestion.ToString(), out cGest))
                            { cab_new.manage_id = cGest; }
                            else { cab_new.manage_id = null; }

                            cab_new.TRANSACTIONS_DETAILS = value_details;
                            cab_new.TRANSACTIONS_PARAMETERS_DETAILS = value_parameters;

                            context.TRANSACTIONS_HEADERS.AddObject(cab_new);
                            #endregion
                            context.SaveChanges();
                        }
                    }
                }

                F2 = DateTime.Now;
                System.Windows.Forms.MessageBox.Show((F2 - F1).ToString());

                respuesta.codigo = 1;
                respuesta.descripcion = "OK";
            }
            catch (Exception ex)
            {
                respuesta.codigo = -1;
                respuesta.descripcion = JsonConvert.SerializeObject(ex);
            }
            return respuesta;
        }
        public void AgregarCredito()
        {
            using (AFN2Entities context = new AFN2Entities())
            using (Repositories.Main repo = new Repositories.Main(context))
            {
                var begin_year = new DateTime(2019, 1, 1);
                PARAMETER credito = repo.parametros.Credito;
                PARAMETER precio = repo.parametros.PrecioBase;
                var transacs = (from H in context.TRANSACTIONS_HEADERS
                               .Include("TRANSACTIONS_PARAMETERS_DETAILS")
                                where H.trx_ini >= begin_year
                                select H);
                var sistemas = repo.sistemas;
                bool guardar = false;
                foreach (var trans in transacs)
                {
                    var cred = trans.TRANSACTIONS_PARAMETERS_DETAILS.Where(x => x.paratemer_id == credito.id).FirstOrDefault();
                    if (cred == null && (trans.PART.BATCHS_ARTICLES.purchase_date.Year == begin_year.Year))
                    {
                        var details = trans.TRANSACTIONS_PARAMETERS_DETAILS.ToList();
                        foreach (var cur_param in details)
                        {
                            if (cur_param.paratemer_id == precio.id)
                            {
                                TRANSACTION_DETAIL det = (from d in context.TRANSACTIONS_DETAILS
                                                          where d.trx_head_id == cur_param.trx_head_id
                                                          && d.system_id == cur_param.system_id
                                                          select d).FirstOrDefault();
                                if (det != null && det.allow_credit)
                                {
                                    var ratio = sistemas.ById(cur_param.system_id).ENVIORMENT.credit_rate;
                                    var parametro_detalle = new TRANSACTION_PARAMETER_DETAIL();
                                    parametro_detalle.system_id = cur_param.system_id;
                                    parametro_detalle.trx_head_id = cur_param.trx_head_id;
                                    parametro_detalle.paratemer_id = credito.id;
                                    parametro_detalle.parameter_value = cur_param.parameter_value * ratio;
                                    context.TRANSACTIONS_PARAMETERS_DETAILS.AddObject(parametro_detalle);
                                    guardar = true;
                                }
                            }
                        }
                    }
                }
                if (guardar)
                    context.SaveChanges();
            }
        }

        public void CorregirBajas()
        {
            using (AFN2Entities context = new AFN2Entities())
            using (Repositories.Main repo = new Repositories.Main(context))
            {
                DateTime desde = new DateTime(2019, 1, 1);
                DateTime hasta = new DateTime(2019, 6, 30);
                var cabeceras_baja = (from H in context.TRANSACTIONS_HEADERS
                                      join P in context.PARTS on H.article_part_id equals P.id
                                      join L in context.BATCHS_ARTICLES on P.article_id equals L.id
                                      where (H.ref_source.Contains("CASTI")
                                      || H.ref_source.Contains("VENT")) &&
                                      (H.trx_ini >= desde && H.trx_ini <= hasta)
                                      select new { H, P, L });
                var articles = (from C in cabeceras_baja
                                    select C.L.id).Distinct().ToArray();
                var infos_old = (from D in context.BASE_GLOBAL_AFN_CALC
                                where articles.Contains(D.cod_articulo)
                                select D).ToList();
                SV_SYSTEM ifrs_clp = repo.sistemas.IfrsCLP;
                SV_SYSTEM ifrs_yen = repo.sistemas.IfrsYEN;
                var parametros = repo.parametros;
                foreach (var cab in cabeceras_baja)
                {
                    //Seccion IFRS CLP
                    var info_old_clp = infos_old.Where(x=> 
                            x.cod_articulo == cab.L.id &&
                            x.parte == cab.P.part_index &&
                            x.fecha_corte >= cab.H.trx_ini &&
                            x.fuente == "GC"
                           ).First();
                    var params_ifrs_clp = (from TPD in context.TRANSACTIONS_PARAMETERS_DETAILS
                                 where TPD.trx_head_id == cab.H.id
                                 && TPD.system_id == ifrs_clp.id
                                 select TPD);
                    foreach (var param in params_ifrs_clp)
                    {
                        //if (param.paratemer_id == parametros.PrecioBase.id)
                        //    param.parameter_value = info_old_clp.val_AF;
                        if (param.paratemer_id == parametros.DepreciacionAcum.id)
                            param.parameter_value = info_old_clp.DA_AF / info_old_clp.cantidad;
                        if (param.paratemer_id == parametros.VidaUtil.id)
                            param.parameter_value = info_old_clp.vu_resi;
                        //if (param.paratemer_id == parametros.ValorResidual.id)
                        //    param.parameter_value = info_old_clp.val_res;
                        
                    }
                    //Seccion IFRS YEN
                    var info_old_yen = infos_old.Where(x =>
                            x.cod_articulo == cab.L.id &&
                            x.parte == cab.P.part_index &&
                            x.fecha_corte >= cab.H.trx_ini &&
                            x.fuente == "GY"
                           ).First();
                    var params_ifrs_yen = (from TPD in context.TRANSACTIONS_PARAMETERS_DETAILS
                                           where TPD.trx_head_id == cab.H.id
                                           && TPD.system_id == ifrs_yen.id
                                           select TPD);
                    foreach (var param in params_ifrs_yen)
                    {
                        if (param.paratemer_id == parametros.DepreciacionAcum.id)
                            param.parameter_value = info_old_yen.DA_AF / info_old_yen.cantidad;
                        if (param.paratemer_id == parametros.VidaUtil.id)
                            param.parameter_value = info_old_yen.vu_resi;
                    }
                    
                }
                context.SaveChanges();
            }
        }

        public void CargarDatosOBC()
        {
            List<AFN_OBRA_CONS> data_old;
            using (AFNoldEntities contextOld = new AFNoldEntities())
            {
                data_old = contextOld.AFN_OBRA_CONS.ToList();
            }
            using (AFN2Entities context = new AFN2Entities())
            using (Repositories.Main repo = new Repositories.Main(context))
            {
                int[] migrated = (from obc in context.ASSETS_IN_PROGRESS_DETAIL
                                select obc.head_id).ToArray();

                var toMigrate = (from old in data_old
                         where !( migrated.Contains(old.codmov) )
                         select old).OrderBy(x => x.codmov);

                //context.Connection.Open();
                //context.ExecuteStoreCommand("SET IDENTITY_INSERT dbo.ASSETS_IN_PROGRESS_HEAD ON");
                foreach (var fila in toMigrate)
                {
                    ASSET_IN_PROGRESS_HEAD NuevaHead;
                    NuevaHead = (from h in context.ASSETS_IN_PROGRESS_HEAD 
                                 where h.id == fila.codmov
                                 select h).First();

                    //Detalles de OBC
                    var DetalleCLP = new ASSET_IN_PROGRESS_DETAIL();
                    DetalleCLP.currency_id = 1;     //1:CLP
                    DetalleCLP.amount = fila.monto;
                    DetalleCLP.head_id = NuevaHead.id;
                    context.AddToASSETS_IN_PROGRESS_DETAIL(DetalleCLP);

                    var DetalleYEN = new ASSET_IN_PROGRESS_DETAIL();
                    DetalleYEN.currency_id = 2;     //1:YEN
                    DetalleYEN.amount = fila.monto_yen;
                    DetalleYEN.head_id = NuevaHead.id;
                    context.AddToASSETS_IN_PROGRESS_DETAIL(DetalleYEN);

                    //reviso documento
                    DOCUMENT WorkDoc;
                    DOCS_OBC association = new DOCS_OBC();
                    var DocumentFounded = repo.documentos.ByNumProv(fila.num_doc,fila.proveedor);
                    if (DocumentFounded == null)
                    {
                        WorkDoc = new DOCUMENT();
                        WorkDoc.docnumber = fila.num_doc;
                        WorkDoc.proveedor_id = fila.proveedor;
                        WorkDoc.comment = "";
                        WorkDoc.proveedor_name = ""; //traer posteriormente el nombre del proveedor
                        //context.AddToDOCUMENTS(WorkDoc);
                        association.DOCUMENT = WorkDoc;
                        
                    }
                    else
                    {
                        association.document_id = DocumentFounded.id;
                    }

                    association.obc_id = NuevaHead.id;
                    context.AddToDOCS_OBC(association);
                    context.SaveChanges();
                }
                //context.ExecuteStoreCommand("SET IDENTITY_INSERT dbo.ASSETS_IN_PROGRESS_HEAD OFF");
                //context.Connection.Close();
            }
        }
    }
}
