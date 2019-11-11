using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SINGLE_DETAIL : IElemento
    {
        public SV_SYSTEM fuente { get; set; }
        public int codigo_articulo { get; set; }
        public string descripcion { get; set; }
        public string proveedor { get; set; }
        public DateTime fecha_compra { get; set; }
        public string descrip_proveedor { get; set; }
        public string num_doc { get; set; }
        public decimal precio_inicial { get; set; }
        public int vida_util_inicial { get; set; }
        public bool derecho_credito { get; set; }
        public DateTime fecha_ingreso { get; set; }
        public GENERIC_VALUE origen { get; set; }
        public DateTime fecha_inicio { get; set; }
        public GENERIC_VALUE zona { get; set; }
        public int estado { get; set; }
        public DateTime fecha_fin { get; set; }
        public decimal precio_base { get; set; }
        public int cantidad { get; set; }
        public decimal depreciacion_acum { get; set; }
        public decimal deterioro { get; set; }
        public decimal valor_residual { get; set; }
        public int vida_util_base { get; set; }
        public GENERIC_VALUE clase { get; set; }
        public GENERIC_VALUE categoria { get; set; }
        public GENERIC_VALUE subzona { get; set; }
        public GENERIC_VALUE subclase { get; set; }
        public GENERIC_VALUE tipo { get; set; }
        public GENERIC_VALUE gestion { get; set; }
        public string usuario { get; set; }
        public GENERIC_VALUE metod_val { get; set; }
        public decimal preparacion { get; set; }
        public decimal transporte { get; set; }
        public decimal montaje { get; set; }
        public decimal desmantel { get; set; }
        public decimal honorario { get; set; }
        public decimal revalorizacion { get; set; }
        public bool se_deprecia { get; set; }


        public object Item(int index)
        {
            switch (index)
            {
                case 0: return fuente;
                default: return null;

            }
        }

        public bool set_values(DETAIL_PROCESS process, SV_BATCH_ARTICLE lote)
        {
            try
            {

                this.fuente = process.sistema;
                this.codigo_articulo = process.cod_articulo;
                this.descripcion = lote.descrip;
                this.proveedor = lote.documents.Select(d => d.proveedor_id).DefaultIfEmpty("SIN_PROVEED").First();
                this.descrip_proveedor = lote.documents.Select(d => d.proveedor_name).DefaultIfEmpty("SIN_PROVEED").First();
                this.num_doc = lote.documents.Select(d => d.docnumber).DefaultIfEmpty("SIN_DOCUMENTO").First();
                this.fecha_compra = lote.purchase_date;
                this.cantidad = process.cantidad;
                this.precio_inicial = lote.initial_price;
                this.vida_util_inicial = lote.initial_life_time;
                this.derecho_credito = process.derecho_credito;
                this.fecha_ingreso = process.fecha_ing;
                this.origen = process.origen;
                this.fecha_inicio = process.fecha_inicio;
                this.zona = process.zona;
                this.estado = process.aprobacion.id;
                this.fecha_fin = process.fecha_fin;
                this.precio_base = process.parametros.GetPrecioBase.value;
                this.depreciacion_acum = process.parametros.GetDepreciacionAcum.value;
                this.deterioro = process.parametros.GetDeterioro.value;
                this.valor_residual = process.parametros.GetValorResidual.value;
                this.vida_util_base = (int)process.parametros.GetVidaUtil.value;
                this.clase = process.clase;
                this.categoria = process.categoria;
                this.subzona = process.subzona;
                this.subclase = process.subclase;
                this.usuario = process.usuario;
                this.metod_val = process.metodo_reval;
                this.preparacion = process.parametros.GetPreparacion.value;
                this.transporte = process.parametros.GetTransporte.value;
                this.montaje = process.parametros.GetMontaje.value;
                this.desmantel = process.parametros.GetDesmantelamiento.value;
                this.honorario = process.parametros.GetHonorario.value;
                this.revalorizacion = process.parametros.GetRevalorizacion.value;
                this.tipo = process.tipo;
                this.gestion = process.gestion;
                this.se_deprecia = process.se_deprecia;

                return true;
            }
            catch
            {
                return false;
            }
        }


    }
}
