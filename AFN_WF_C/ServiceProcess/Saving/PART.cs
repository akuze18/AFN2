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

        public PART PART_NEW(int batch_id, int part_index, int quantity, DateTime first_date) //para migracion
        {
            var nuevo = new PART();
            nuevo.article_id = batch_id;
            nuevo.part_index = part_index;
            nuevo.quantity = quantity;
            nuevo.first_date = first_date;
            _context.PARTS.AddObject(nuevo);
            _context.SaveChanges();
            _load_parts();
            return nuevo;
        }

        public RespuestaAccion REGISTER_PURCHASE(int batch_id, DateTime fecha_compra, decimal valor_total, int cantidad_total)
        {
            var res = new RespuestaAccion();
            try
            {
                decimal valor_unitario = Math.Floor(valor_total / cantidad_total);
                int residuo = (int)(valor_total - (valor_unitario * cantidad_total));
                if (residuo == 0)
                {
                    //solo requiero 1 parte
                    PART part1 = new PART();
                    part1.article_id = batch_id;
                    part1.part_index = 0;
                    part1.quantity = cantidad_total;
                    part1.first_date = fecha_compra;

                    _context.PARTS.AddObject(part1);
                    _context.SaveChanges();

                    res.result_objs.Add((SV_PART)part1);
                }
                else
                {
                    PART part1 = new PART();
                    part1.article_id = batch_id;
                    part1.part_index = 0;
                    part1.quantity = cantidad_total - residuo;
                    part1.first_date = fecha_compra;
                    _context.PARTS.AddObject(part1);

                    PART part2 = new PART();
                    part2.article_id = batch_id;
                    part2.part_index = 1;
                    part2.quantity = residuo;
                    part2.first_date = fecha_compra;
                    _context.PARTS.AddObject(part2);
                    _context.SaveChanges();
                    res.result_objs.Add((SV_PART)part1);
                    res.result_objs.Add((SV_PART)part2);
                }
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
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
