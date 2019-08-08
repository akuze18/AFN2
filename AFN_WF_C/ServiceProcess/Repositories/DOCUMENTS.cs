using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class DOCUMENTS
    {
        private List<DOCUMENT> _source;
        public DOCUMENTS(ObjectSet<DOCUMENT> source) { _source = source.Include("DOCS_BATCH").Include("DOCS_OBC").ToList(); }
        //public DOCUMENTS(List<DOCUMENT> source) { _source = source; }

        public DOCUMENT ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
        public List<DOCUMENT> ByBatch(BATCH_ARTICLE b)
        {
            return ByBatch(b.id);
        }
        public List<DOCUMENT> ByBatch(int batch_id)
        {
            return _source.Where(
                doc => doc.DOCS_BATCH.Where(dc => dc.batch_id == batch_id).Count() > 0
               ).ToList();
        }

        public DOCUMENT ByNumProv(string numero, string proveedor_id)
        {
            if (numero != "SIN_DOCUMENTO" && proveedor_id != "SIN_PROVEED")
            {
                return _source.Where(
                    doc =>
                        doc.docnumber == numero &&
                        doc.proveedor_id == proveedor_id
                    ).FirstOrDefault();
            }
            else
                return null;
        }
    }
}
