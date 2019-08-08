﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.HelperServiceAFN
{
    public class CABECERA
    {
        public List<TRANSACTION_HEADER> ByParte(int p)
        {
            using (AFN2Entities context = new AFN2Entities())
            {
                var th = new Repositories.TRANSACTIONS_HEADERS(context.TRANSACTIONS_HEADERS);
                return th.ByParte(p);
            }
        }
    }
}
