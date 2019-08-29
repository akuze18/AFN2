using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AFN_WF_C.ServiceProcess.PublicData
{
    public class LIST_PARAM_VALUE : IList<PARAM_VALUE>
    {
        private List<PARAM_VALUE> _int_list;

        public LIST_PARAM_VALUE() { 
            _int_list = new List<PARAM_VALUE>();
        }

        #region Implementacion IList
        public int IndexOf(PARAM_VALUE item)
        {
            return _int_list.IndexOf(item);
        }

        public void Insert(int index, PARAM_VALUE item)
        {
            _int_list.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _int_list.RemoveAt(index);
        }

        public PARAM_VALUE this[int index]
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

        public void Add(PARAM_VALUE item)
        {
            _int_list.Add(item);
        }

        public void Clear()
        {
            _int_list.Clear();
        }

        public bool Contains(PARAM_VALUE item)
        {
            return _int_list.Contains(item);
        }

        public void CopyTo(PARAM_VALUE[] array, int arrayIndex)
        {
            _int_list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _int_list.Count; }
        }

        bool ICollection<PARAM_VALUE>.IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(PARAM_VALUE item)
        {
            return _int_list.Remove(item);
        }

        public IEnumerator<PARAM_VALUE> GetEnumerator()
        {
            return _int_list.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _int_list.GetEnumerator();
        }
        #endregion

        public PARAM_VALUE GetPrecioBase {
            get {
                return _int_list.Where(x => x.code == "PB").FirstOrDefault();
            }
        }
        public PARAM_VALUE GetDepreciacionAcum {
            get {
                return _int_list.Where(x => x.code == "DA").FirstOrDefault();
            }
        }
        public PARAM_VALUE GetDeterioro
        {
            get
            {
                return _int_list.Where(x => x.code == "DT").FirstOrDefault();
            }
        }
        public PARAM_VALUE GetValorResidual
        {
            get
            {
                return _int_list.Where(x => x.code == "VR").FirstOrDefault();
            }
        }
        public PARAM_VALUE GetVidaUtil
        {
            get
            {
                return _int_list.Where(x => x.code == "VUB").FirstOrDefault();
            }
        }
        public PARAM_VALUE GetCredito
        {
            get
            {
                return _int_list.Where(x => x.code == "CRED").FirstOrDefault();
            }
        }

        public PARAM_VALUE GetPreparacion
        {
            get
            {
                string code = "PREP";
                var found = _int_list.Where(x => x.code == code).FirstOrDefault();
                if (found == null)
                {
                    return new PARAM_VALUE() { value = 0, code = code };
                }
                return found;
            }
        }
        public PARAM_VALUE GetDesmantelamiento
        {
            get
            {
                string code = "DESM";
                var found = _int_list.Where(x => x.code == code).FirstOrDefault();
                if (found == null)
                {
                    return new PARAM_VALUE() { value = 0, code = code };
                }
                return found;
            }
        }
        public PARAM_VALUE GetTransporte
        {
            get
            {
                string code = "TRAN";
                var found = _int_list.Where(x => x.code == code).FirstOrDefault();
                if (found == null)
                {
                    return new PARAM_VALUE() { value = 0, code = code };
                }
                return found;
            }
        }
        public PARAM_VALUE GetMontaje
        {
            get
            {
                string code = "MON";
                var found = _int_list.Where(x => x.code == code).FirstOrDefault();
                if (found == null)
                {
                    return new PARAM_VALUE() { value = 0, code = code };
                }
                return found;
            }
        }
        public PARAM_VALUE GetHonorario
        {
            get
            {
                string code = "HON";
                var found = _int_list.Where(x => x.code == code).FirstOrDefault();
                if (found == null)
                {
                    return new PARAM_VALUE() { value = 0, code = code };
                }
                return found;
            }
        }
        public PARAM_VALUE GetRevalorizacion
        {
            get
            {
                string code = "REVAL";
                var found = _int_list.Where(x => x.code == code).FirstOrDefault();
                if (found == null)
                {
                    return new PARAM_VALUE() { value = 0, code = code };
                }
                return found;
            }
        }

        public override string ToString()
        {
            var x = "";
            foreach (var element in _int_list) {
                x += element.code+" - ";
            }
            return _int_list.Count.ToString()+ " : " + x;
        }
    }
}
