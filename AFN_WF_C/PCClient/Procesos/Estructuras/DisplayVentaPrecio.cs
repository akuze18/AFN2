using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using PD = AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.PCClient.Procesos.Estructuras
{
    public class DisplayVentaPrecio
    {
        public int rowIndex;        //0
        public int CodArticulo;     //1 Cod Artículo
        public int Parte;           //2
        public string DescripArt;   //3 Descripción Artículo
        public int Cantidad;        //4
        public decimal CostoUnitario;//5 Costo_Unitario
        public decimal CostoExtend;  //6 Costo_Extend
        public decimal PrecioUnitario;//7 Precio Unitario
        public decimal PrecioTotal;  //8 Precio Total
        public PD.GENERIC_VALUE Zona;   //9
        public PD.GENERIC_VALUE Subzona;    //10
        public PD.GENERIC_VALUE Clase;  //11
        public string UoE;          //12
    }
}
