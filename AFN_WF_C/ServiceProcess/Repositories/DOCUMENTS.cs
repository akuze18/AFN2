using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class DOCUMENTS
    {
        private List<SV_DOCUMENT> _source;
        public DOCUMENTS(ObjectSet<DOCUMENT> source) {
            _source = source
                .Include("DOCS_BATCH")
                .Include("DOCS_OBC")
                .ToList()
                .ConvertAll(d => (SV_DOCUMENT)d); }

        public SV_DOCUMENT ById(int idFind)
        {
            return _source.Where(z => z.id == idFind).FirstOrDefault();
        }
        
        public List<SV_DOCUMENT> ByBatch(SV_BATCH_ARTICLE b)
        {
            return ByBatch(b.id);
        }
        public List<SV_DOCUMENT> ByBatch(int batch_id)
        {
            return _source.Where(
                doc => doc.DOCS_BATCH.Contains(batch_id)
               ).ToList();
        }

        public SV_DOCUMENT ByNumProv(string numero, string proveedor_id)
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
