using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class SALES
    {
        private List<SALES_HEAD> _heads;
        private List<SALES_DETAIL> _details;
        private GP_MultiCurrency _multi_currency;

        public SALES(ObjectSet<SALES_HEAD> source1, ObjectSet<SALES_DETAIL> source2, GP_MultiCurrency multicurr)
        {
            _heads = source1.ToList();
            _details = source2.ToList();
            _multi_currency = multicurr;
        }

        public decimal GetPriceSalesByPart(int PartId, SV_CURRENCY moneda)
        {
            //int x = 0;
            //if(PartId == 204)
            //    x = 1;
            var find = _details.Where(d => d.part_id == PartId);
            if (find.Count() > 0)
            {
                decimal total = find.Sum(d => d.ext_price);
                if (moneda == "YEN")
                {
                    int headId = find.First().head_id;
                    DateTime docDate = _heads.Where(h => h.id == headId).First().docdate;
                    var TC = _multi_currency.YEN(docDate);
                    if (TC == 0)
                        return -1;
                    else
                        return total / TC;
                }
                else
                    return total;
            }
            else
                return 0;
        }

        public int CheckDocName(string usedName)
        {
            return _heads.Where(h => h.docventa.ToUpper() == usedName.ToUpper()).Count();
        }

        public int CheckArticlePartUsed(int PartId)
        {
            return _details.Where(d => d.part_id == PartId).Count();
        }
    }
}
