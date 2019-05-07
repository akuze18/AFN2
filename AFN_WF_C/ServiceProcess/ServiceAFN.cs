using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AFN_WF_C.ServiceProcess.DataContract;

using Newtonsoft.Json;

namespace AFN_WF_C.ServiceProcess
{
    class ServiceAFN
    {
        public List<GENERIC_VALUE> Zonas_All()
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var z = new Repositories.ZONES(context.ZONES);
                return z.All();
            }
        }
        
        public SYSTEM System_Get(string codeEnv, string codeCurr)
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var S1 = (from A in context.SYSTEMS.Include("ENVIORMENT").Include("CURRENCY")
                          where A.ENVIORMENT.code == codeEnv && A.CURRENCY.code == codeCurr
                          select A).First();
                return S1;
            }
        }

        public RespuestaAccion CargaDatosDesdeAFN(int grupo)
        {
            var r = new RespuestaAccion();
            List<int> lotes = new List<int> { };
            for (int i = ((grupo * 100) - 99); i <= (grupo * 100); i++)
            {
                lotes.Add(i);
            }
            //string connectionOldString = "data source=ATENEA\\GP2013;initial catalog=AFN;persist security info=True;user id=sa;password=NHFC1002;multipleactiveresultsets=True;App=EntityFramework";
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
                {
                    context.CommandTimeout = 3000;
                    SYSTEM sistema = Process.SistemaDefecto();

                    var currentPars = (from a in context.PARTS
                                       join b in context.BATCHS_ARTICLES on a.article_id equals b.id
                                       where lotes.Contains(b.id)
                                       select new { a, b }).ToList();
                    var parametros = new Repositories.PARAMETERS(context.PARAMETERS);
                    foreach (var parte in currentPars)
                    {
                        var myFin_CLP = origen_fin_clp
                            .Where(x => x.id_articulo == parte.b.id && x.parte == parte.a.part_index);
                        foreach (var curr_fin_clp in myFin_CLP)
                        {
                            #region cabecera
                            TRANSACTION_HEADER cab = new TRANSACTION_HEADER();
                            cab.article_part_id = parte.a.id;
                            cab.trx_ini = curr_fin_clp.fecha_inicio;
                            cab.trx_end = (DateTime)curr_fin_clp.fecha_fin;
                            cab.ref_source = "MIG";
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
                                param4_s1.parameter_value = (decimal)curr_fin_clp.valor_residual;
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
                                SYSTEM sistema2 = (from S in context.SYSTEMS where S.ENVIORMENT.code == "FIN" && S.CURRENCY.code == "YEN" select S).First();
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
                                    param4_s2.parameter_value = (decimal)curr_fin_yen.valor_residual;
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
                                SYSTEM sistema3 = (from S in context.SYSTEMS where S.ENVIORMENT.code == "TRIB" && S.CURRENCY.code == "CLP" select S).First();
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
                                    param4_s3.parameter_value = (decimal)curr_trib_clp.valor_residual;
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
                                SYSTEM sistema4 = (from S in context.SYSTEMS where S.ENVIORMENT.code == "IFRS" && S.CURRENCY.code == "CLP" select S).First();
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
                                    param4_s4.parameter_value = (decimal)curr_ifrs_clp.valor_residual;
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
                                SYSTEM sistema5 = (from S in context.SYSTEMS where S.ENVIORMENT.code == "IFRS" && S.CURRENCY.code == "YEN" select S).First();
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
                                    param4_s5.parameter_value = (decimal)curr_ifrs_yen.valor_residual;
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

        public object base_movimiento(int sistema_id, DateTime corte)
        {
            var salida = new List<DETAIL_PROCESS>();

            using (AFN2Entities context = new AFN2Entities())
            using (var repo = new Repositories.ALL(context))
            {
                var datos = (from A in context.BATCHS_ARTICLES
                             join B in context.PARTS on A.id equals B.article_id
                             join C in context.TRANSACTIONS_HEADERS
                                 //.Include("TRANSACTIONS_PARAMETERS_DETAILS")
                             on B.id equals C.article_part_id
                             join D in context.TRANSACTIONS_DETAILS
                             on C.id equals D.trx_head_id
                             //join E in context.TRANSACTIONS_PARAMETERS_DETAILS
                             //on C.id equals E.trx_head_id
                             where
                                 C.trx_ini <= corte &&
                                 C.trx_end > corte &&
                                 A.account_date <= corte &&
                                 A.APROVAL_STATE.code == "CLOSE" &&
                                 D.system_id == sistema_id
                             //&& E.system_id == sistema.id
                             select new { Batch = A, Part = B, Head = C, Detail = D }); //, Parameters = E

                var all_params = repo.parametros_sistemas.BySystem(sistema_id);

                foreach (var d in datos)
                {
                    //string salida;
                    //salida = "Cab : " + d.Head.id.ToString();
                    //salida += " > Detalles : " + d.Head.TRANSACTIONS_DETAILS.Count().ToString();
                    //salida += " > Parametros : " + 
                    //    ( datos.Where(x=>x.Parameters.trx_head_id == d.Head.id).Count()).ToString();
                    //return salida;
                    var line = new DETAIL_PROCESS();
                    //line.sistema = sistema;
                    line.cod_articulo = d.Batch.id;
                    line.parte = d.Part.part_index;
                    line.fecha_inicio = d.Head.trx_ini;
                    line.fecha_fin = d.Head.trx_end;
                    line.zona = repo.zonas.ById(d.Head.zone_id);
                    line.estado = repo.validaciones.ById(d.Detail.validity_id);
                    line.cantidad = d.Part.quantity;
                    line.clase = repo.clases.ById(d.Head.kind_id);
                    line.categoria = repo.categorias.ById(d.Head.category_id);
                    line.subzona = repo.subzonas.ById(d.Head.subzone_id);
                    line.subclase = repo.subclases.ById(d.Head.subkind_id);
                    line.gestion = repo.gestiones.ById(d.Head.manage_id);
                    line.usuario = d.Head.user_own;
                    line.se_deprecia = d.Detail.depreciate;
                    line.status = repo.estados.ById(d.Batch.aproval_state_id);
                    line.dscrp = d.Batch.descrip;
                    //line.dsc_extra = d.Batch.descrip;
                    line.fecha_compra = d.Batch.purchase_date;
                    //line.documentos = repo.documentos.ByBatch(d.Batch);
                    line.precio_inicial = d.Batch.initial_price;
                    line.vida_util_inicial = d.Batch.initial_life_time;
                    line.derecho_credito = d.Detail.allow_credit;
                    line.fecha_ing = d.Batch.account_date;
                    line.origen = repo.origenes.ById(d.Batch.origin_id);
                    line.tipo = repo.tipos.ById(d.Batch.type_asset_id);
                    var valores = new List<PARAM_VALUE>();
                    var curr_vals = (from a in context.TRANSACTIONS_PARAMETERS_DETAILS
                                     where a.trx_head_id == d.Head.id &&
                                     a.system_id == sistema_id
                                     select a).ToList();
                    foreach (var par in all_params)
                    {
                        PARAMETER meta_param = repo.parametros.ById(par.parameter_id);
                        var det = new PARAM_VALUE();
                        det.code = meta_param.code;
                        det.name = meta_param.name;
                        var act_val = curr_vals.Find(x => x.paratemer_id == meta_param.id);
                        if (act_val == null)
                        {
                            det.value = 0;
                        }
                        else
                        {
                            det.value = act_val.parameter_value;
                        }
                        valores.Add(det);
                    }
                    line.parametros = valores;
                    salida.Add(line);
                }


            }
            //return JsonConvert.SerializeObject(salida.First()).Substring(0,200);
            return salida.First();
        }

    }
}
