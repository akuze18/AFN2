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
                this.proveedor = lote.documents.Select(d => d.proveedor_id).DefaultIfEmpty(Repositories.DOCUMENTS.defaultProveed).First();
                this.descrip_proveedor = lote.documents.Select(d => d.proveedor_name).DefaultIfEmpty(Repositories.DOCUMENTS.defaultProveed).First();
                this.num_doc = lote.documents.Select(d => d.docnumber).DefaultIfEmpty(Repositories.DOCUMENTS.defaultDocument).First();
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
        public bool set_values(List<DETAIL_PROCESS> ListProcess, SV_BATCH_ARTICLE lote)
        {
            try
            {

                this.fuente = ListProcess.Select(a => a.sistema).First();// .sistema;
                this.codigo_articulo = ListProcess.Select(a => a.cod_articulo).First();
                this.descripcion = lote.descrip;
                this.proveedor = lote.documents.Select(d => d.proveedor_id).DefaultIfEmpty(Repositories.DOCUMENTS.defaultProveed).First();
                this.descrip_proveedor = lote.documents.Select(d => d.proveedor_name).DefaultIfEmpty(Repositories.DOCUMENTS.defaultProveed).First();
                this.num_doc = lote.documents.Select(d => d.docnumber).DefaultIfEmpty(Repositories.DOCUMENTS.defaultDocument).First();
                this.fecha_compra = lote.purchase_date;
                this.cantidad = ListProcess.Sum(a => a.cantidad);
                this.precio_inicial = lote.initial_price;
                this.vida_util_inicial = lote.initial_life_time;
                this.derecho_credito = ListProcess.Select(a => a.derecho_credito).First();
                this.fecha_ingreso = ListProcess.Select(a => a.fecha_ing).First();
                this.origen = ListProcess.Select(a => a.origen).First();
                this.fecha_inicio = ListProcess.Select(a => a.fecha_inicio).First();
                this.zona = ListProcess.Select(a => a.zona).First();
                this.estado = ListProcess.Select(a => a.aprobacion.id).First();
                this.fecha_fin = ListProcess.Select(a => a.fecha_fin).First();
                this.precio_base = ListProcess.Select(a => a.parametros.GetPrecioBase.value).First();
                this.depreciacion_acum = ListProcess.Select(a => a.parametros.GetDepreciacionAcum.value).First();
                this.deterioro = ListProcess.Select(a => a.parametros.GetDeterioro.value).First();
                this.valor_residual = ListProcess.Select(a => a.parametros.GetValorResidual.value).First();
                this.vida_util_base = (int)ListProcess.Select(a => a.parametros.GetVidaUtil.value).First();
                this.clase = ListProcess.Select(a => a.clase).First();
                this.categoria = ListProcess.Select(a => a.categoria).First();
                this.subzona = ListProcess.Select(a => a.subzona).First();
                this.subclase = ListProcess.Select(a => a.subclase).First();
                this.usuario = ListProcess.Select(a => a.usuario).First();
                this.metod_val = ListProcess.Select(a => a.metodo_reval).First();
                this.preparacion = ListProcess.Select(a => a.parametros.GetPreparacion.value).First();
                this.transporte = ListProcess.Select(a => a.parametros.GetTransporte.value).First();
                this.montaje = ListProcess.Select(a => a.parametros.GetMontaje.value).First();
                this.desmantel = ListProcess.Select(a => a.parametros.GetDesmantelamiento.value).First();
                this.honorario = ListProcess.Select(a => a.parametros.GetHonorario.value).First();
                this.revalorizacion = ListProcess.Select(a => a.parametros.GetRevalorizacion.value).First();
                this.tipo = ListProcess.Select(a => a.tipo).First();
                this.gestion = ListProcess.Select(a => a.gestion).First();
                this.se_deprecia = ListProcess.Select(a => a.se_deprecia).First();

                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
