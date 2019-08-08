using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.DataContract
{
    public class LIST_CORR_MON : IList<CORRECTION_MONETARY_VALUE>
    {
        private List<CORRECTION_MONETARY_VALUE> _int_list;

        public LIST_CORR_MON()
        {
            _int_list = new List<CORRECTION_MONETARY_VALUE>();
        }

        #region Implementacion IList
        public int IndexOf(CORRECTION_MONETARY_VALUE item)
        {
            return _int_list.IndexOf(item);
        }

        public void Insert(int index, CORRECTION_MONETARY_VALUE item)
        {
            _int_list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _int_list.RemoveAt(index);
        }

        public CORRECTION_MONETARY_VALUE this[int index]
        {
            get
            {
                return _int_list[index];
            }
            set
            {
                _int_list[index] = value;
            }
        }

        public void Add(CORRECTION_MONETARY_VALUE item)
        {
            _int_list.Add(item);
        }

        public void Clear()
        {
            _int_list.Clear();
        }

        public bool Contains(CORRECTION_MONETARY_VALUE item)
        {
            return _int_list.Contains(item);
        }

        public void CopyTo(CORRECTION_MONETARY_VALUE[] array, int arrayIndex)
        {
            CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _int_list.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(CORRECTION_MONETARY_VALUE item)
        {
            return _int_list.Remove(item);
        }

        public IEnumerator<CORRECTION_MONETARY_VALUE> GetEnumerator()
        {
            return _int_list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        public static implicit operator LIST_CORR_MON (List<CORRECTION_MONETARY_VALUE> c){
            var result = new LIST_CORR_MON();
            foreach (var elem in c) {
                result.Add(elem);
            }
            return result;
        }

        public CORRECTION_MONETARY_VALUE byAplica(DateTime fecha_compra, ACode.Vperiodo Periodo) {

            if (Periodo.last.Year == fecha_compra.Year)
            {
                var r = _int_list.Where(x => x.applyTo == fecha_compra.Month).FirstOrDefault();
                if (r != null)
                    return r;
                else
                    return new CORRECTION_MONETARY_VALUE() { amount = 0 };
            }
            else {
                var r = _int_list.Where(x => x.applyTo == 0).FirstOrDefault();
                if (r != null)
                    return r;
                else
                    return new CORRECTION_MONETARY_VALUE() { amount = 0 };
            }
        }
    }
}
