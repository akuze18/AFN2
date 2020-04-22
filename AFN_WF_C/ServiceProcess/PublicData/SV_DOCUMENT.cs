using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class SV_DOCUMENT
    {
        private int _id;
        private string _docnumber;
        private string _comment;
        private string _proveedor_id;
        private string _proveedor_name;

        public int id { get { return _id; } }
        public string docnumber { get { return _docnumber; } }
        public string comment { get { return _comment; } }
        public string proveedor_id { get { return _proveedor_id; } }
        public string proveedor_name { get { return _proveedor_name; } }

        #region Related

        private int[] _DOCS_BATCH;
        private int[] _DOCS_OBC;

        public int[] DOCS_BATCH { get { return _DOCS_BATCH; } }
        public int[] DOCS_OBC { get { return _DOCS_OBC; } }

        #endregion


        #region Convertions
        public static implicit operator SV_DOCUMENT(DataContract.DOCUMENT od)
        {
            return new SV_DOCUMENT()
            {
                _id = od.id,
                _docnumber = od.docnumber,
                _comment = od.comment,
                _proveedor_id = od.proveedor_id,
                _proveedor_name = od.proveedor_name,
                _DOCS_BATCH = od.DOCS_BATCH.Select(db => db.batch_id).ToArray(),
                _DOCS_OBC = od.DOCS_OBC.Select(dobc => dobc.obc_id).ToArray(),
            };
        }
        //public static implicit operator C.GENERIC_VALUE(SV_DOCUMENT sv)
        //{
        //    return new C.GENERIC_VALUE()
        //    {
        //        id = sv.id,
        //        code = sv.docnumber,
        //        description = sv.docnumber,
        //        type = sv.GetType().Name.Substring(3),
        //    };
        //}
        #endregion
    }
}
