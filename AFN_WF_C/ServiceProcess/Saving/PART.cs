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
        private void _load_parts()
        {
            _parts = new PARTS(_context.PARTS);
        }
        public PART PART_NEW(int batch_id, int part_index, int quantity)
        {
            var nuevo = new PART();
            nuevo.article_id = batch_id;
            nuevo.part_index = part_index;
            nuevo.quantity = quantity;
            _context.PARTS.AddObject(nuevo);
            _context.SaveChanges();
            _load_parts();
            return nuevo;
        }

        public RespuestaAccion PART_UPDATE(int part_id, int quantity)
        {
            var resultado = new RespuestaAccion();
            var busca = _context.PARTS.Where(p => p.id == part_id).FirstOrDefault();
            if (busca != null)
            {
                try
                {
                    busca.quantity = quantity;
                    _context.SaveChanges();
                    _load_parts();
                    resultado.set_ok();
                }
                catch (Exception ex)
                {
                    resultado.set(-2, ex.Message);
                }
            }
            else
            {
                resultado.set(-1,"No existe id en PART para modificar");
            }
            return resultado;
        }
    }
}
