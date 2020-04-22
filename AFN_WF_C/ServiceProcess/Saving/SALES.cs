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
        public RespuestaAccion CREATE_SALES_DOC(string DocNumber, decimal ExtendedCost, DateTime DocDate,List<C.DisplayVentaPrecio> DetailsList)
        {
            var res = new RespuestaAccion();
            try
            {
                var SalesHeadNew = new SALES_HEAD();
                SalesHeadNew.docventa = DocNumber;
                SalesHeadNew.cost_ext = ExtendedCost;
                SalesHeadNew.docdate = DocDate;

                _context.SALES_HEAD.AddObject(SalesHeadNew);

                foreach (var d in DetailsList)
                {
                    decimal diferencia = d.PrecioTotal - (d.PrecioUnitario * d.Cantidad);
                    if (diferencia == 0)
                    {
                        var NewSaleDetail = new SALES_DETAIL();
                        NewSaleDetail.SALES_HEAD = SalesHeadNew;
                        NewSaleDetail.part_id = d.rowIndex;
                        NewSaleDetail.quantity = d.Cantidad;
                        NewSaleDetail.unit_cost = d.CostoUnitario;
                        NewSaleDetail.ext_cost = d.CostoExtend;
                        NewSaleDetail.unit_price = d.PrecioUnitario;
                        NewSaleDetail.ext_price = d.PrecioTotal;
                        NewSaleDetail.zone_id = d.Zona.id;
                        NewSaleDetail.subzone_id = d.Subzona.id;
                        NewSaleDetail.kind_id = d.Clase.id;
                        NewSaleDetail.docline = DetailsList.IndexOf(d);

                        _context.SALES_DETAIL.AddObject(NewSaleDetail);
                    }
                    else
                    {
                        var NewSaleDetailA = new SALES_DETAIL();
                        NewSaleDetailA.SALES_HEAD = SalesHeadNew;
                        NewSaleDetailA.part_id = d.rowIndex;
                        NewSaleDetailA.quantity = 1;
                        NewSaleDetailA.unit_cost = d.CostoUnitario;
                        NewSaleDetailA.ext_cost = d.CostoUnitario;
                        NewSaleDetailA.unit_price = d.PrecioUnitario+diferencia;
                        NewSaleDetailA.ext_price = d.PrecioUnitario+diferencia;
                        NewSaleDetailA.zone_id = d.Zona.id;
                        NewSaleDetailA.subzone_id = d.Subzona.id;
                        NewSaleDetailA.kind_id = d.Clase.id;
                        NewSaleDetailA.docline = DetailsList.IndexOf(d);

                        _context.SALES_DETAIL.AddObject(NewSaleDetailA);

                        var NewSaleDetailB = new SALES_DETAIL();
                        NewSaleDetailB.SALES_HEAD = SalesHeadNew;
                        NewSaleDetailB.part_id = d.rowIndex;
                        NewSaleDetailB.quantity = d.Cantidad -1;
                        NewSaleDetailB.unit_cost = d.CostoUnitario;
                        NewSaleDetailB.ext_cost = d.CostoUnitario * NewSaleDetailB.quantity;
                        NewSaleDetailB.unit_price = d.PrecioUnitario;
                        NewSaleDetailB.ext_price = d.PrecioUnitario * NewSaleDetailB.quantity;
                        NewSaleDetailB.zone_id = d.Zona.id;
                        NewSaleDetailB.subzone_id = d.Subzona.id;
                        NewSaleDetailB.kind_id = d.Clase.id;
                        NewSaleDetailB.docline = DetailsList.IndexOf(d);

                        _context.SALES_DETAIL.AddObject(NewSaleDetailB);
                    }
                }

                _context.SaveChanges();
                res.AddResultObj(SalesHeadNew.id, SalesHeadNew.GetType());
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
