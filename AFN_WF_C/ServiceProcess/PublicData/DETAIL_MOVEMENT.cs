using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SV = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class DETAIL_MOVEMENT : IElemento
    {
        #region Values
        public SV.SV_SYSTEM sistema { get; private set; }
        public int cod_articulo { get; private set; }
        public int parte { get; private set; }
        public DateTime fecha_compra { get; private set; }
        public string desc_breve { get; private set; }
        //public GENERIC_VALUE vigencia { get; set; }
        public int cantidad { get; private set; }
        public SV_ZONE zona { get; private set; }
        public SV_KIND clase { get; private set; }

        public SV_SUBZONE subzona { get; private set; }
        public GENERIC_VALUE subclase { get; private set; }
        public GENERIC_VALUE gestion { get; private set; }
        public GENERIC_VALUE categoria { get; set; }

        /*Anterior*/
        public SV_ZONE zona_anterior { get; private set; }
        public SV_KIND clase_anterior { get; private set; }

        public SV_SUBZONE subzona_anterior { get; private set; }
        public GENERIC_VALUE subclase_anterior { get; private set; }
        public GENERIC_VALUE gestion_anterior { get; private set; }
        public GENERIC_VALUE categoria_anterior { get; set; }

        public DateTime fecha_ingreso { get; private set; }
        public DateTime fecha_inicio { get; private set; }
        public DateTime fecha_fin { get; private set; }

        public GENERIC_VALUE origen { get; private set; }
        //public GENERIC_VALUE tipo { get; set; }
        public GENERIC_VALUE vigencia { get; private set; }

        public bool derecho_credito { get; private set; }

        //public bool se_deprecia { get; set; }
        public int vida_util_inicial { get; private set; }

        //public GENERIC_VALUE aprobacion { get; set; }
        public SV_METHOD_REVALUE metodo_reval { get; private set; }

        public decimal valor_activo_inicial { get; private set; }
        public decimal credito_monto { get; private set; }
        public decimal valor_activo_final { get; private set; }
        public decimal depreciacion_acum_inicial { get; private set; }
        public decimal deterioro { get; private set; }
        public decimal valor_residual { get; private set; }
        public decimal valor_sujeto_dep { get; private set; }
        public int vida_util_asignada { get; private set; }
        public int vida_util_ocupada { get; private set; }
        public int vida_util_residual { get; private set; }
        public decimal depreciacion_ejercicio { get; private set; }
        public decimal depreciacion_acum_final { get; private set; }
        public decimal valor_libro { get; private set; }
        //public string usuario { get; private set; }             
        //public decimal precio_inicial { get; private set; }     
        public decimal porcentaje_cm { get; private set; }
        public decimal valor_activo_cm { get; private set; }
        public decimal valor_activo_update { get; private set; }
        public decimal depreciacion_acum_cm { get; private set; }
        public decimal depreciacion_acum_update { get; private set; }
        public decimal preparacion { get; private set; }
        public decimal desmantelamiento { get; private set; }
        public decimal transporte { get; private set; }
        public decimal montaje { get; private set; }
        public decimal honorario { get; private set; }
        public decimal revalorizacion { get; private set; }

        public string situacion { get; private set; }
        public decimal precio_venta { get; private set; }

        public List<SV_DOCUMENT> documentos;

        public string num_documento { get { return documentos.Select(d => d.docnumber).DefaultIfEmpty(Repositories.DOCUMENTS.defaultDocument).First(); } }
        public string proveedor_id { get { return documentos.Select(d => d.proveedor_id).DefaultIfEmpty(Repositories.DOCUMENTS.defaultProveed).First(); } }
        public string proveedor_name { get { return documentos.Select(d => d.proveedor_name).DefaultIfEmpty(Repositories.DOCUMENTS.defaultProveed).First().Trim(); } }

        //Para detalle de inventario 
        public SV_PLACE ubicacion { get; private set; }
        public string entregado { get; private set; }
        public string codigo_inv { get; private set; }
        public string codigo_inv_old { get; private set; }
        public GENERIC_VALUE ultimo_estado { get; private set; }

        public int PartId { get; private set; }

        #endregion

        #region Constructor
        private DETAIL_MOVEMENT()
        {
            //if need it to avoid null references fill values required
        }
        public DETAIL_MOVEMENT(DETAIL_PROCESS initial, DETAIL_PROCESS final, Repositories.SITUATIONS Situaciones)
        {
            this.sistema = final.sistema;
            this.fecha_compra = final.fecha_compra;
            this.parte = final.parte;
            this.PartId = final.PartId;
            this.cod_articulo = final.cod_articulo;
            this.fecha_ingreso = final.fecha_ing;
            this.desc_breve = final.dscrp + final.dsc_extra;
            this.cantidad = final.cantidad;
            this.zona = final.zona;
            this.clase = final.clase;
            this.categoria = final.categoria;

            this.derecho_credito = final.derecho_credito;

            if (initial != null)
            {
                this.zona_anterior = initial.zona;
                this.clase_anterior = initial.clase;
                this.subzona_anterior = initial.subzona;
                this.subclase_anterior = initial.subclase;
                this.gestion_anterior = initial.gestion;
                this.categoria_anterior = initial.categoria;

                this.valor_activo_inicial = (initial.parametros.PrecioBaseVal * initial.cantidad) + (final.parametros.CreditoVal * final.cantidad);
                this.credito_monto = 0;
                this.depreciacion_acum_inicial = initial.parametros.DepreciacionAcumVal * initial.cantidad;
                this.deterioro = initial.parametros.DeterioroVal * initial.cantidad;
                this.valor_residual = initial.parametros.ValorResidualVal * initial.cantidad;
                this.vida_util_asignada = (int)(initial.parametros.VidaUtilVal);
                this.vida_util_residual = (int)(final.parametros.VidaUtilVal);
                this.situacion = Situaciones.ByValidations(initial.vigencia.id, final.vigencia.id).description;

                //Ajuste para determinar cambio de zona
                if (this.situacion == "ACTIVO" && (final.zona.id != initial.zona.id || final.subzona.id != initial.subzona.id))
                    this.situacion = "MOVIDO";
            }
            else
            {
                //this.zona_anterior = null;
                //this.clase_anterior = null;
                //this.subzona_anterior = null;
                //this.subclase_anterior = null;
                //this.gestion_anterior = null;
                //this.categoria_anterior = null;

                this.valor_activo_inicial = final.parametros.PrecioBaseVal * final.cantidad;
                this.credito_monto = final.parametros.CreditoVal * final.cantidad;
                this.depreciacion_acum_inicial = 0;
                this.deterioro = final.parametros.DeterioroVal * final.cantidad;
                this.valor_residual = final.parametros.ValorResidualVal * final.cantidad;
                this.vida_util_asignada = final.vida_util_inicial;
                this.vida_util_residual = (int)(final.parametros.VidaUtilVal);
                this.situacion = Situaciones.ByValidations(4, final.vigencia.id).description;
            }
            this.porcentaje_cm = 0;
            this.valor_activo_cm = 0;
            this.valor_activo_update = this.valor_activo_inicial + this.valor_activo_cm;
            this.preparacion = final.parametros.GetPreparacion.value * final.cantidad;
            this.desmantelamiento = final.parametros.GetDesmantelamiento.value * final.cantidad;
            this.transporte = final.parametros.GetTransporte.value * final.cantidad;
            this.montaje = final.parametros.GetMontaje.value * final.cantidad;
            this.honorario = final.parametros.GetHonorario.value * final.cantidad;
            this.valor_activo_final = this.valor_activo_update + this.credito_monto + this.preparacion + this.desmantelamiento + this.transporte + this.montaje + this.honorario;
            this.depreciacion_acum_cm = 0;
            this.depreciacion_acum_update = this.depreciacion_acum_inicial + this.depreciacion_acum_cm;
            this.valor_sujeto_dep = this.valor_activo_final + this.depreciacion_acum_update + this.deterioro + this.valor_residual;
            this.vida_util_ocupada = this.vida_util_asignada - this.vida_util_residual;
            this.depreciacion_acum_final = final.parametros.GetDepreciacionAcum.value * final.cantidad;
            this.depreciacion_ejercicio = this.depreciacion_acum_final - this.depreciacion_acum_inicial;
            this.revalorizacion = final.parametros.GetRevalorizacion.value * final.cantidad;
            this.valor_libro = this.depreciacion_acum_final + this.valor_activo_final + this.revalorizacion;

            this.fecha_inicio = final.fecha_inicio;
            this.fecha_fin = final.fecha_fin;
            this.vigencia = final.vigencia;
            this.subzona = final.subzona;
            this.subclase = final.subclase;
            this.origen = final.origen;
            this.gestion = final.gestion;
            this.vida_util_inicial = final.vida_util_inicial;
            this.precio_venta = final.precio_venta;

            this.metodo_reval = final.metodo_reval;
            this.documentos = final.documentos;
        }
        public DETAIL_MOVEMENT(DETAIL_MOVEMENT AccountDetail, int NewCantidad, SV_PLACE ArtUbicacion, string ArtCodInv, string ArtCordInvOld, string ArtEntregado, GENERIC_VALUE ArtLastState)
        {
            this.sistema = AccountDetail.sistema;
            this.fecha_compra = AccountDetail.fecha_compra;
            this.parte = AccountDetail.parte;
            this.PartId = AccountDetail.PartId;
            this.cod_articulo = AccountDetail.cod_articulo;
            this.fecha_ingreso = AccountDetail.fecha_ingreso;
            this.desc_breve = AccountDetail.desc_breve;
            this.cantidad = NewCantidad;
            this.zona = AccountDetail.zona;
            this.clase = AccountDetail.clase;
            this.categoria = AccountDetail.categoria;

            this.derecho_credito = AccountDetail.derecho_credito;
            this.zona_anterior = AccountDetail.zona;
            this.clase_anterior = AccountDetail.clase;
            this.subzona_anterior = AccountDetail.subzona;
            this.subclase_anterior = AccountDetail.subclase;
            this.gestion_anterior = AccountDetail.gestion;
            this.categoria_anterior = AccountDetail.categoria;

            this.valor_activo_inicial = Math.Round(AccountDetail.valor_activo_inicial/AccountDetail.cantidad)*NewCantidad;
            this.credito_monto = Math.Round(AccountDetail.credito_monto / AccountDetail.cantidad) * NewCantidad;
            this.depreciacion_acum_inicial = Math.Round(AccountDetail.depreciacion_acum_inicial / AccountDetail.cantidad) * NewCantidad ;
            this.deterioro = Math.Round(AccountDetail.deterioro / AccountDetail.cantidad) * NewCantidad;
            this.valor_residual = Math.Round(AccountDetail.valor_residual / AccountDetail.cantidad) * NewCantidad;
            this.vida_util_asignada = AccountDetail.vida_util_asignada;
            this.vida_util_residual = AccountDetail.vida_util_residual;
            this.situacion = AccountDetail.situacion;

            this.porcentaje_cm = AccountDetail.porcentaje_cm;
            this.valor_activo_cm = Math.Round(AccountDetail.valor_activo_cm / AccountDetail.cantidad) * NewCantidad;
            this.valor_activo_update = Math.Round(AccountDetail.valor_activo_update / AccountDetail.cantidad) * NewCantidad;
            this.preparacion = Math.Round(AccountDetail.preparacion / AccountDetail.cantidad) * NewCantidad;
            this.desmantelamiento = Math.Round(AccountDetail.desmantelamiento / AccountDetail.cantidad) * NewCantidad;
            this.transporte = Math.Round(AccountDetail.transporte / AccountDetail.cantidad) * NewCantidad;
            this.montaje = Math.Round(AccountDetail.montaje / AccountDetail.cantidad) * NewCantidad;
            this.honorario = Math.Round(AccountDetail.honorario / AccountDetail.cantidad) * NewCantidad;
            this.valor_activo_final = Math.Round(AccountDetail.valor_activo_final / AccountDetail.cantidad) * NewCantidad;
            this.depreciacion_acum_cm = Math.Round(AccountDetail.depreciacion_acum_inicial / AccountDetail.cantidad) * NewCantidad;
            this.depreciacion_acum_update = Math.Round(AccountDetail.depreciacion_acum_update / AccountDetail.cantidad) * NewCantidad;
            this.valor_sujeto_dep = Math.Round(AccountDetail.valor_sujeto_dep / AccountDetail.cantidad) * NewCantidad;
            this.vida_util_ocupada = AccountDetail.vida_util_ocupada;
            this.depreciacion_acum_final = Math.Round(AccountDetail.depreciacion_acum_final / AccountDetail.cantidad) * NewCantidad;
            this.depreciacion_ejercicio = Math.Round(AccountDetail.depreciacion_ejercicio / AccountDetail.cantidad) * NewCantidad;
            this.revalorizacion = Math.Round(AccountDetail.revalorizacion / AccountDetail.cantidad) * NewCantidad;
            this.valor_libro = Math.Round(AccountDetail.valor_libro / AccountDetail.cantidad) * NewCantidad ;

            this.fecha_inicio = AccountDetail.fecha_inicio;
            this.fecha_fin = AccountDetail.fecha_fin;
            this.vigencia = AccountDetail.vigencia;
            this.subzona = AccountDetail.subzona;
            this.subclase = AccountDetail.subclase;
            this.origen = AccountDetail.origen;
            this.gestion = AccountDetail.gestion;
            this.vida_util_inicial = AccountDetail.vida_util_inicial;
            this.precio_venta = AccountDetail.precio_venta;

            this.metodo_reval = AccountDetail.metodo_reval;
            this.documentos = AccountDetail.documentos;

            this.ubicacion = ArtUbicacion;
            this.entregado = ArtEntregado;
            this.codigo_inv = ArtCodInv;
            this.codigo_inv_old = ArtCordInvOld;
            this.ultimo_estado = ArtLastState;
        }

        public static DETAIL_MOVEMENT Empty()
        {
            //used to prevent no valued instance directly
            return new DETAIL_MOVEMENT();
        }
        #endregion

        #region IElemento
        public object Item(int index)
        {
            switch (index)
            {
                case 0: return sistema;
                case 1: return cod_articulo;
                case 2: return fecha_compra;
                case 3: return desc_breve;
                case 4: return cantidad;
                case 5: return (GENERIC_VALUE)zona;
                case 6: return (GENERIC_VALUE)clase;
                case 7: return valor_activo_inicial;
                case 8: return credito_monto;
                case 9: return valor_activo_final;
                case 10: return depreciacion_acum_inicial;
                case 11: return deterioro;
                case 12: return valor_residual;
                case 13: return valor_sujeto_dep;
                case 14: return vida_util_asignada;
                case 15: return vida_util_ocupada;
                case 16: return vida_util_residual;
                case 17: return depreciacion_ejercicio;
                case 18: return depreciacion_acum_final;
                case 19: return valor_libro;
                case 20: return porcentaje_cm;
                case 21: return valor_activo_cm;
                case 22: return valor_activo_update;
                case 23: return depreciacion_acum_cm;
                case 24: return depreciacion_acum_update;
                case 25: return preparacion;
                case 26: return desmantelamiento;
                case 27: return transporte;
                case 28: return montaje;
                case 29: return honorario;
                case 30: return revalorizacion;
                case 31: return fecha_inicio;
                case 32: return fecha_fin;
                case 33: return vigencia;
                case 34: return (GENERIC_VALUE)subzona;
                case 35: return subclase;
                case 36: return parte;
                case 37: return situacion;
                case 38: return gestion;
                case 39: return fecha_ingreso;
                case 40: return origen;
                case 41: return vida_util_inicial;
                case 42: return precio_venta;
                case 43: return (GENERIC_VALUE)ubicacion;
                case 44: return entregado;
                case 45: return codigo_inv;
                case 46: return codigo_inv_old;
                case 47: return ultimo_estado;
                default:
                    return null;
            }
        }
        #endregion
    }
}
