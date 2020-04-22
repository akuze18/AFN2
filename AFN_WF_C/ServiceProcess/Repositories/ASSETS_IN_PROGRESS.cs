using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.Objects;
using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public class ASSETS_IN_PROGRESS
    {
        private List<SV_ASSET_CONSTRUCTION> _source;
        public ASSETS_IN_PROGRESS(ObjectSet<ASSET_IN_PROGRESS_HEAD> source)
        {
            _source = source
                .Include("ASSETS_IN_PROGRESS_DETAIL")
                .Include("ASSETS_IN_PROGRESS_DETAIL.CURRENCY")
                .Include("ZONE")
                .Include("APROVAL_STATES")
                .ToList()
                .ConvertAll(s => (SV_ASSET_CONSTRUCTION)s); 
        }

        public decimal TotalYen(int batch_id)
        {
            var values = _source.Where(obc => obc.batch_id == batch_id)
                .Select(obc => obc.TotalByCurrency("YEN")
                    ).ToList();
            var total = values.Sum();
            return total;
        }

        public SV_ASSET_CONSTRUCTION IngresoById(int headId)
        {
            return _source.Where(sa => sa.id == headId && sa.tipo == Main._InOBC).FirstOrDefault();
        }

        public SV_ASSET_CONSTRUCTION EgresoById(int headId)
        {
            return _source.Where(sa => sa.id == headId && sa.tipo == Main._OutOBC).FirstOrDefault();
        }

        public decimal SaldoDisponible(int EntradaId, SV_CURRENCY moneda)
        {
            decimal TotalEntrada = _source.Where(sa => sa.id == EntradaId &&
                                        sa.tipo == Main._InOBC &&
                                        sa.aproval_state.id == 2)
                                    .Select(sa => sa.TotalByCurrency(moneda))
                                    .Sum();
            decimal TotalConsumido = _source.Where(
                                        sa => sa.entrada_id == EntradaId &&
                                        sa.tipo == Main._OutOBC &&
                                        sa.aproval_state.id == 2)
                                    .Select(sa => sa.TotalByCurrency(moneda))
                                    .Sum();

            return TotalEntrada - TotalConsumido;
        }
        public decimal SaldoDisponible(int EntradaId, string moneda)
        {
            decimal TotalEntrada = _source.Where(
                                        sa => sa.id == EntradaId &&
                                        sa.tipo == Main._InOBC &&
                                        sa.aproval_state.id == 2)
                                    .Select(sa => sa.TotalByCurrency(moneda))
                                    .Sum();
            decimal TotalConsumido = _source.Where(
                                        sa => sa.entrada_id == EntradaId &&
                                        sa.tipo == Main._OutOBC &&
                                        sa.aproval_state.id == 2
                                    )
                                    .Select(sa => sa.TotalByCurrency(moneda))
                                    .Sum();

            return TotalEntrada - TotalConsumido;
        }

        public List<SV_ASSET_CONSTRUCTION> SaldoEntradas(DateTime moment, string CurrCode)
        {
            var entradas = _source.Where(sa => 
                                sa.tipo == Main._InOBC && 
                                sa.post_date <= moment &&
                                sa.aproval_state.id == 2)
                            .ToList();
            var conSaldo = new List<SV_ASSET_CONSTRUCTION>();
            decimal FindSaldo;
            for (int i = 0; i < entradas.Count; i++)
            {
                var entrada = entradas[i];
                FindSaldo = SaldoDisponible(entrada.id, CurrCode);
                if (FindSaldo != 0)
                {
                    entrada.ocupado = FindSaldo;
                    conSaldo.Add(entrada);
                }
                
                //entrada.ocupado = SaldoDisponible(entrada.id, CurrCode);
                //entradas[i] = entrada;
            }
            return conSaldo;
        
        }
        public List<SV_ASSET_CONSTRUCTION> EntradasAbiertas()
        {
            var entradas = _source.Where(sa => 
                                sa.tipo == Main._InOBC && 
                                sa.aproval_state.id == 1)
                            .ToList();
            return entradas;
        }
        public List<SV_ASSET_CONSTRUCTION> SalidasAbiertas()
        {
            var entradas = _source.Where(sa =>
                                sa.tipo == Main._OutOBC &&
                                sa.aproval_state.id == 1)
                            .ToList();
            return entradas;
        }
        
    }
}
