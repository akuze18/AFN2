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
        public static string defaultDocument { get { return "SIN_DOCUMENTO"; } }
        public static string defaultProveed { get { return "SIN_PROVEED"; } }

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
            if (numero != defaultDocument && proveedor_id != defaultProveed)
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
