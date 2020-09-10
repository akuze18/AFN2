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

        public RespuestaAccion CREATE_PART_NEW(int batch_id,int part_index,int quantity, DateTime movement_date)
        {
            var res = new RespuestaAccion();
            try
            {
                var nuevo = new PART();
                nuevo.article_id = batch_id;
                nuevo.part_index = part_index;
                nuevo.quantity = quantity;
                nuevo.first_date = movement_date;
                _context.PARTS.AddObject(nuevo);
                _context.SaveChanges();
                _load_parts();
                res.result_objs.Add((SV_PART)nuevo);
                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }

        public RespuestaAccion REGISTER_PURCHASE(int batch_id, DateTime fecha_compra, decimal valor_total, int cantidad_total)
        {
            var res = new RespuestaAccion();
            try
            {
                decimal valor_unitario =(valor_total / cantidad_total);
                //siempre se generara solo 1 registro por compra
                //int residuo = (int)(valor_total - (valor_unitario * cantidad_total));
                //if (residuo == 0)
                //{
                    //solo requiero 1 parte
                    PART part1 = new PART();
                    part1.article_id = batch_id;
                    part1.part_index = 0;
                    part1.quantity = cantidad_total;
                    part1.first_date = fecha_compra;

                    _context.PARTS.AddObject(part1);
                    _context.SaveChanges();

                    res.result_objs.Add((SV_PART)part1);
                //}
                //else
                //{
                    //PART part1 = new PART();
                    //part1.article_id = batch_id;
                    //part1.part_index = 0;
                    //part1.quantity = cantidad_total - residuo;
                    //part1.first_date = fecha_compra;
                    //_context.PARTS.AddObject(part1);

                    //PART part2 = new PART();
                    //part2.article_id = batch_id;
                    //part2.part_index = 1;
                    //part2.quantity = residuo;
                    //part2.first_date = fecha_compra;
                    //_context.PARTS.AddObject(part2);
                    //_context.SaveChanges();
                    //res.result_objs.Add((SV_PART)part1);
                    //res.result_objs.Add((SV_PART)part2);
                //}
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

        public RespuestaAccion CREATE_NEW_PART_FROM(int batch_id, int source_part_id, int quantity, DateTime movement_date)
        {
            var res = new RespuestaAccion();
            try
            {
                var source_part = Partes.ById(source_part_id);
                int current_max_index = Partes.ByLote(batch_id).Max(p => p.part_index);

                //creo nueva parte
                var nueva_parte = new PART();
                nueva_parte.article_id = batch_id;
                nueva_parte.part_index = current_max_index + 1;
                nueva_parte.quantity = quantity;
                nueva_parte.first_date = movement_date;
                _context.PARTS.AddObject(nueva_parte);
                
                //actualizo cantidad parte antigua
                var busca = _context.PARTS.Where(p => p.id == source_part_id).FirstOrDefault();
                busca.quantity = source_part.quantity - quantity;
                
                //copio transacciones a la nueva parte y sus relacionados desde la parte origen
                //copio cabeceras de transacciones
                var source_trx_heads = cabeceras.ByParte(source_part_id);
                foreach (SV_TRANSACTION_HEADER s_trx_head in source_trx_heads)
                {
                    TRANSACTION_HEADER nueva_head = new TRANSACTION_HEADER();
                    nueva_head.PART = nueva_parte;      //con este relacionamos a la nueva parte
                    nueva_head.head_index = s_trx_head.head_index;
                    nueva_head.trx_ini = s_trx_head.trx_ini;
                    nueva_head.trx_end = s_trx_head.trx_end;
                    nueva_head.ref_source = s_trx_head.ref_source;
                    nueva_head.zone_id = s_trx_head.zone_id;
                    nueva_head.subzone_id = s_trx_head.subzone_id;
                    nueva_head.kind_id = s_trx_head.kind_id;
                    nueva_head.subkind_id = s_trx_head.subkind_id;
                    nueva_head.category_id = s_trx_head.category_id;
                    nueva_head.user_own = s_trx_head.user_own;
                    nueva_head.manage_id = s_trx_head.manage_id;
                    nueva_head.method_revalue_id = s_trx_head.method_revalue_id;
                    _context.TRANSACTIONS_HEADERS.AddObject(nueva_head);
                    
                    //copio detalles de transacciones
                    var source_trx_details = detalles.GetByHead(s_trx_head.id);
                    foreach (SV_TRANSACTION_DETAIL s_trx_detail in source_trx_details)
                    {
                        TRANSACTION_DETAIL nuevo_detail = new TRANSACTION_DETAIL();
                        nuevo_detail.TRANSACTION_HEADER = nueva_head; //con este relacionamos a la nueva cabecera
                        nuevo_detail.system_id = s_trx_detail.system_id;
                        nuevo_detail.validity_id = s_trx_detail.validity_id;
                        nuevo_detail.depreciate = s_trx_detail.depreciate;
                        nuevo_detail.allow_credit = s_trx_detail.allow_credit;
                        _context.TRANSACTIONS_DETAILS.AddObject(nuevo_detail);
                    }
                    //copio detalle de parametros de transaccion
                    var source_trx_params = DetallesParametros.ByHead(s_trx_head.id);
                    foreach (SV_TRANSACTION_PARAMETER_DETAIL s_trx_param in source_trx_params)
                    {
                        TRANSACTION_PARAMETER_DETAIL nuevo_param = new TRANSACTION_PARAMETER_DETAIL();
                        nuevo_param.TRANSACTION_HEADER = nueva_head; //con este relacionamos a la nueva cabecera
                        nuevo_param.system_id = s_trx_param.system_id;
                        nuevo_param.paratemer_id = s_trx_param.paratemer_id;
                        nuevo_param.parameter_value = s_trx_param.parameter_value;
                        _context.TRANSACTIONS_PARAMETERS_DETAILS.AddObject(nuevo_param);
                    }
                    
                }
                _context.SaveChanges();
                _load_parts();
                _load_transactions_headers();
                _load_transactions_details();
                _load_transactions_param_details();
                res.result_objs.Add((SV_PART)nueva_parte);

                res.set_ok();
            }
            catch (Exception ex)
            {
                res.set(-1, ex.StackTrace);
            }
            return res;
        }
    }
}
