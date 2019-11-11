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
        public DOCS_BATCH DOC_BATCH_NEW(int batch_id, AFN_LOTE_ARTICULOS lote_old)
        {
            var findDocs = documentos.ByBatch(lote_old.cod);
            if (lote_old.num_doc != "SIN_DOCUMENTO" && findDocs.Count() == 0)
            {
                var new_doc = DOCUMENT_NEW_PREV(lote_old);
                var new_rel = new DOCS_BATCH();
                new_rel.DOCUMENT = new_doc;
                new_rel.batch_id = batch_id;

                _context.DOCS_BATCH.AddObject(new_rel);
                _context.SaveChanges();

                return new_rel;
            }
            return null;
        }
    }
}
