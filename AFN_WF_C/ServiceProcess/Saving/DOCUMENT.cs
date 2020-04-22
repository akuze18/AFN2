using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public partial class Main
    {
        private DOCUMENT DOCUMENT_NEW_PREV(AFN_LOTE_ARTICULOS lote_old)
        {
            var new_doc = new DOCUMENT();
            new_doc.docnumber = lote_old.num_doc;
            new_doc.comment = string.Empty;
            new_doc.proveedor_id = lote_old.proveedor;
            new_doc.proveedor_name = proveedor_master
                .Where(pm => pm.COD == lote_old.proveedor)
                .Select(pm => pm.VENDNAME)
                .DefaultIfEmpty(string.Empty)
                .First();
            return new_doc;
        }
    }
}
