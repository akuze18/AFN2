using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class GENERIC_RELATED : GENERIC_VALUE
    {   
        //private int related_id {get;set;}
        public GENERIC_VALUE related { get; set; }

    #region Constructors
        public GENERIC_RELATED() { }
        //public GENERIC_RELATED(int id, string description, string type, int related_id) {
        //    this.id = id;
        //    this.code = id.ToString();
        //    this.description = description;
        //    this.type = type;
        //    this.related_id = related_id;
        //    this.related = new GENERIC_VALUE() { id = related_id };
        //}
        public GENERIC_RELATED(DataContract.PACKAGE_KIND pk)
        {
            if (pk != null)
            {
                this.id = pk.id;
                this.code = pk.type_asset_id.ToString();
                this.description = pk.descrip;
                this.type = pk.GetType().Name;
                //this.related_id = pk.PACKAGE_PAIR_KINDS.FirstOrDefault().kind_id;
                var find_rel = pk.PACKAGE_PAIR_KINDS.FirstOrDefault();
                int indice = (find_rel != null? find_rel.kind_id:0);
                this.related = new GENERIC_VALUE()
                {
                    id = indice,
                    type = "KIND"
                };
                
            }
        }
    #endregion

        #region Convertions
        public static implicit operator GENERIC_RELATED(DataContract.PACKAGE_KIND pk)
        {
            return new GENERIC_RELATED(pk);
        }
        #endregion
    }
}
