using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class DETAIL_OBC : IElemento
    {
        public GENERIC_VALUE moneda;
        public int codMov;
        public string description;
        public DateTime txFecha;
        public DateTime glFecha;
        public GENERIC_VALUE zona;
        public decimal monto;
        public decimal ocupado;
        public decimal saldo { get { return monto - ocupado; } }
        
        //Detalle Documento
        public string num_documento;
        public string id_proveedor;
        public string nombre_proveedor;

        //Datos de Entrada
        public int codEntrada;

        //Detalle Salida
        public int codigoArticulo;
        public string descripcionArticulo;
        public GENERIC_VALUE zonaArticulo;
        public GENERIC_VALUE claseArticulo;
        public decimal montoTotalArticulo;
 

        public DETAIL_OBC()
        {
            num_documento = string.Empty;
            id_proveedor = string.Empty;
            nombre_proveedor = string.Empty;

            codEntrada = 0;

            codigoArticulo = 0;
            descripcionArticulo = string.Empty;
            zonaArticulo = GENERIC_VALUE.EmptyText;
            claseArticulo = GENERIC_VALUE.EmptyText;
            montoTotalArticulo = 0;
        }

        public object Item(int index)
        {
            switch (index)
            {
                case 0: return moneda;
                case 1: return codMov;
                case 2: return description;
                case 3: return txFecha;
                case 4: return glFecha;
                case 5: return zona;
                case 6: return monto;
                case 7: return ocupado;
                case 8: return saldo;
                case 9: return num_documento;
                case 10: return id_proveedor;
                case 11: return nombre_proveedor;
                case 12: return codigoArticulo;
                case 13: return descripcionArticulo;
                case 14: return zonaArticulo;
                case 15: return claseArticulo;
                case 16: return codEntrada;
                case 17: return montoTotalArticulo;
                default:
                    return null;
            }
        }
    }
}
