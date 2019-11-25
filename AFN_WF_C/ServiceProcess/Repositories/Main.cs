﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

using AFN_WF_C.ServiceProcess.DataContract;
using AFN_WF_C.ServiceProcess.PublicData;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    public partial class Main : IDisposable
    {
        
        private string[] _def_apro;
        private bool _def_with_param;
        private bool _def_check_post;

        private AFN2Entities _context;
        private ZONES _zones;
        private VALIDATIES _validaties;
        private KINDS _kinds;
        private CATEGORIES _categories;
        private SUBZONES _subzones;
        private SUBKINDS _subkinds;
        private MANAGEMENTS _managements;
        private APROVALS_STATES _aprovals_states;
        private ORIGINS _origins;
        private TYPES_ASSETS _type_assets;
        private CURRENCIES _currencies;
        private PARTS _parts;
        private SITUATIONS _situation;
        private PACKAGE_KINDS _package_kinds;
        private PARAMETERS _parameters;
        private DOCUMENTS _documents;
        private SYSTEMS _systems;
        private SYSTEMS_PARAMETERS _sys_params;
        private CORRECTIONS_MONETARIES_VALUES _corr_monets;
        private BATCHES_ARTICLES _batches_articles;
        private TRANSACTIONS_HEADERS _transactions_headers;
        private TRANSACTIONS_DETAILS _transactions_details;
        private TRANSACTIONS_PARAM_DET _transactions_parameters;
        private METHOD_REVALUES _method_revalues;
        private GP_SY40 _gp_sy40;
        private GP_PM _gp_pm;
        private ASSETS_IN_PROGRESS _OBC;
        private GP_MultiCurrency _gp_MC;
        private ADM_IFRS_DEFAULT _adm_idef;
        private INVENTORY _inventory;

        public Main(AFN2Entities context)
        {
            _context = context;
            _def_apro = this.EstadoAprobacion.Default.Select(x => x.code).ToArray();
            _def_with_param = false;
            _def_check_post = true;
        }

        #region Repositorios Especificos

        public ZONES zonas
        {
            get
            {
                if (_zones == null) { _zones = new ZONES(_context.ZONES); }
                return _zones;
            }
        }
        public VALIDATIES Vigencias
        {
            get
            {
                if (_validaties == null) { _validaties = new VALIDATIES(_context.VALIDATIES); }
                return _validaties;
            }
        }
        public KINDS Clases
        {
            get
            {
                if (_kinds == null) { _kinds = new KINDS(_context.KINDS); }
                return _kinds;
            }
        }
        public CATEGORIES categorias
        {
            get
            {
                if (_categories == null) { _categories = new CATEGORIES(_context.CATEGORIES); }
                return _categories;
            }
        }
        public SUBZONES subzonas
        {
            get
            {
                if (_subzones == null) { _subzones = new SUBZONES(_context.SUBZONES); }
                return _subzones;
            }
        }
        public SUBKINDS subclases
        {
            get
            {
                if (_subkinds == null) { _subkinds = new SUBKINDS(_context.SUBKINDS); }
                return _subkinds;
            }
        }
        public MANAGEMENTS gestiones
        {
            get
            {
                if (_managements == null) { _managements = new MANAGEMENTS(_context.MANAGEMENTS); }
                return _managements;
            }
        }
        public APROVALS_STATES EstadoAprobacion
        {
            get
            {
                if (_aprovals_states == null) { _aprovals_states = new APROVALS_STATES(_context.APROVAL_STATES); }
                return _aprovals_states;
            }
        }
        public ORIGINS origenes
        {
            get
            {
                if (_origins == null) { _origins = new ORIGINS(_context.ORIGINS); }
                return _origins;
            }
        }
        public TYPES_ASSETS Tipos
        {
            get
            {
                if (_type_assets == null) { _type_assets = new TYPES_ASSETS(_context.TYPES_ASSETS); }
                return _type_assets;
            }
        }
        public CURRENCIES Monedas
        {
            get {
                if (_currencies == null) { _currencies = new CURRENCIES(_context.CURRENCIES); }
                return _currencies;
            }
        }
        public PARTS Partes
        {
            get { if (_parts == null) { _load_parts(); } return _parts; }
        }
        public SITUATIONS Situaciones
        {
            get { if (_situation == null) { _situation = new SITUATIONS(_context.SITUATIONS); } return _situation; }
        }
        public PACKAGE_KINDS PackageClases
        {
            get { if (_package_kinds == null) { _package_kinds = new PACKAGE_KINDS(_context.PACKAGE_KINDS); } return _package_kinds; }
        }
        public PARAMETERS parametros
        {
            get
            {
                if (_parameters == null) { _parameters = new PARAMETERS(_context.PARAMETERS); }
                return _parameters;
            }
        }
        public DOCUMENTS documentos
        {
            get
            {
                if (_documents == null) { _documents = new DOCUMENTS(_context.DOCUMENTS); }
                return _documents;
            }
        }
        public SYSTEMS sistemas
        {
            get
            {
                if (_systems == null) { _systems = new SYSTEMS(_context.SYSTEMS); }
                return _systems;
            }
        }
        public SYSTEMS_PARAMETERS parametros_sistemas
        {
            get
            {
                if (_sys_params == null) { _sys_params = new SYSTEMS_PARAMETERS(_context.SYSTEMS_PARAMETERS); }
                return _sys_params;
            }
        }
        public CORRECTIONS_MONETARIES_VALUES correcciones_monetarias
        {
            get
            {
                if (_corr_monets == null) { _corr_monets = new CORRECTIONS_MONETARIES_VALUES(_context.CORRECTIONS_MONETARIES_VALUES); }
                return _corr_monets;
            }
        }
        public void set_correccion_monetaria(string periodo)
        {
            _corr_monets = new CORRECTIONS_MONETARIES_VALUES(_context.CORRECTIONS_MONETARIES_VALUES, periodo);
        }

        public METHOD_REVALUES MetodosRev
        {
            get
            {
                if (_method_revalues == null) { _method_revalues = new METHOD_REVALUES(_context.METHOD_REVALUES); }
                return _method_revalues;
            }
        }

        public TRANSACTIONS_HEADERS cabeceras
        {
            get
            {
                if (_transactions_headers == null) _load_transactions_headers();
                return _transactions_headers;
            }
        }
        public TRANSACTIONS_DETAILS detalles
        {
            get
            {
                if (_transactions_details == null) _transactions_details = new TRANSACTIONS_DETAILS(_context.TRANSACTIONS_DETAILS);
                return _transactions_details;
            }
        }
        public BATCHES_ARTICLES lotes
        {
            get
            {
                if (_batches_articles == null) _load_batches_articles();
                return _batches_articles;
            }
        }
        public TRANSACTIONS_PARAM_DET DetallesParametros
        {
            get
            {
                if (_transactions_parameters == null) _transactions_parameters = new TRANSACTIONS_PARAM_DET(_context.TRANSACTIONS_PARAMETERS_DETAILS);
                return _transactions_parameters;
            }
        }
        public void set_detalle_parametros(int SystemId, int[] HeadsIdSelected)
        {
            _transactions_parameters = new TRANSACTIONS_PARAM_DET(_context.TRANSACTIONS_PARAMETERS_DETAILS, SystemId, HeadsIdSelected);
        }

        public ASSETS_IN_PROGRESS ObrasConstruccion
        {
            get
            {
                if (_OBC == null) _OBC = new ASSETS_IN_PROGRESS(_context.ASSETS_IN_PROGRESS_HEAD);
                return _OBC;
            }
        }

        public GP_SY40 PeriodoContable
        {
            get
            {
                if (_gp_sy40 == null) _gp_sy40 = new GP_SY40(_context.SY40100, _context.SY40101);
                return _gp_sy40;
            }
        }
        public GP_PM Proveedor
        {
            get
            {
                if (_gp_pm == null) _gp_pm = new GP_PM(_context.PM00200A);
                return _gp_pm;
            }

        }
        public GP_MultiCurrency TipoCambio
        {
            get
            {
                if (_gp_MC == null) _gp_MC = new GP_MultiCurrency(_context.MC00101);
                return _gp_MC;
            }
        }
        public ADM_IFRS_DEFAULT predetIFRS
        {
            get
            {
                if (_adm_idef == null) _adm_idef = new ADM_IFRS_DEFAULT(_context.IFRS_DEFAULT);
                return _adm_idef;
            }
        }

        public INVENTORY inventario
        {
            get 
            {
                if (_inventory == null) _inventory = new INVENTORY(_context.ARTICLES);
                return _inventory;
            }
        }


        #endregion

        #region Extra Data para Migracion

        internal List<PM00200> proveedor_master;

        #endregion

        #region Modificacion Datos
        //public test test_add()
        //{

        //    return test_add("hola", 1, new DateTime(2019, 3, 3));
        //}
        //public test test_add(string texto, int numero, DateTime fecha)
        //{
        //    var row = new test();
        //    row.texto = texto;
        //    row.numero = numero;
        //    row.fecha = fecha;

        //    var rel = new rel_test();
        //    rel.texto_rel = texto + numero.ToString();
        //    rel.test = row;
        //    //_context.tests.AddObject(row);
        //    _context.rel_test.AddObject(rel);
        //    _context.SaveChanges();
        //    return row;
        //}
        #endregion

        #region Datos Generales
        public List<DETAIL_PROCESS> get_detailed(SV_SYSTEM sistema, DateTime corte, int codigo, string[] aprobados, bool WithParameters, bool CheckPost, GENERIC_VALUE clase, GENERIC_VALUE zona)
        {
            var salida = new List<DETAIL_PROCESS>();
            int default_clase = 1;
            int default_zona = 0;
            int rq_cl = default_clase, rq_zn = default_zona, rq_type = 0;
            if (clase != null)
            {
                rq_cl = clase.id;
                if (clase.code == "10")
                {
                    rq_type = 1;
                    rq_cl = default_clase;
                }
                if (clase.code == "20")
                {
                    rq_type = 2;
                    rq_cl = default_clase;
                }

            }
            if (zona != null)
                rq_zn = zona.id;

            var datos = (from A in this._context.BATCHS_ARTICLES
                         join B in this._context.PARTS on A.id equals B.article_id
                         join C in this._context.TRANSACTIONS_HEADERS
                         on B.id equals C.article_part_id
                         join D in this._context.TRANSACTIONS_DETAILS
                         on C.id equals D.trx_head_id
                         where
                             (C.trx_ini <= corte &&
                             C.trx_end > corte) &&
                             (A.id == codigo || codigo == 0) &&
                             (C.kind_id == rq_cl || rq_cl == default_clase) &&
                             (C.zone_id == rq_zn || rq_zn == default_zona) &&
                             (A.type_asset_id == rq_type || rq_type == 0) &&
                             (A.account_date <= corte || !CheckPost ) &&
                             aprobados.Contains(A.APROVAL_STATE.code) &&
                             D.system_id == sistema.id
                         select new {
                             //Batch = A, 
                             BatchId = A.id,
                             BatchDescription = A.descrip,
                             BatchPurchaseDate = A.purchase_date,
                             BatchAprovalStateId = A.aproval_state_id,
                             BatchInitialPrice = A.initial_price,
                             BatchInitialLifeTime = A.initial_life_time,
                             BatchAccountDate = A.account_date,
                             BatchOriginId = A.origin_id,
                             BatchAssetId = A.type_asset_id,
                             //Part = B, 
                             PartIndex = B.part_index,
                             PartQuantity = B.quantity,
                             PartId = B.id,
                             //Head = C, 
                             HeadTrxIni = C.trx_ini,
                             HeadTrxEnd = C.trx_end,
                             HeadZoneId = C.zone_id,
                             HeadKindId = C.kind_id,
                             HeadCategoryId = C.category_id,
                             HeadSubZoneId = C.subzone_id,
                             HeadSubkindId = C.subkind_id,
                             HeadManageId = C.manage_id,
                             HeadUserOwn = C.user_own,
                             HeadId = C.id,
                             HeadRefSource = C.ref_source,
                             MethodRevalId = C.method_revalue_id,
                             //Detail = D 
                             DetailValidityId = D.validity_id,
                             DetailDepreciate = D.depreciate,
                             DetailAllowCredit = D.allow_credit
                         });

            var selectedHeads = datos.Select(d => d.HeadId).Distinct().ToArray();
            set_detalle_parametros(sistema.id, selectedHeads);
            var all_params = this.parametros_sistemas.BySystem(sistema.id);

            foreach (var d in datos)
            {
                var line = new DETAIL_PROCESS();
                line.sistema = sistema;
                line.cod_articulo = d.BatchId;
                line.parte = d.PartIndex;
                line.fecha_inicio = d.HeadTrxIni;
                line.fecha_fin = d.HeadTrxEnd;
                line.zona = this.zonas.ById(d.HeadZoneId);
                line.vigencia = this.Vigencias.ById(d.DetailValidityId);
                line.cantidad = d.PartQuantity;
                line.clase = this.Clases.ById(d.HeadKindId);
                line.categoria = this.categorias.ById(d.HeadCategoryId);
                line.subzona = this.subzonas.ById(d.HeadSubZoneId);
                line.subclase = this.subclases.ById(d.HeadSubkindId);
                line.gestion = this.gestiones.ById(d.HeadManageId);
                line.usuario = d.HeadUserOwn;
                line.se_deprecia = d.DetailDepreciate;
                line.aprobacion = this.EstadoAprobacion.ById(d.BatchAprovalStateId);
                line.dscrp = d.BatchDescription;
                line.dsc_extra = d.BatchDescription;
                line.fecha_compra = d.BatchPurchaseDate;
                line.documentos = this.documentos.ByBatch(d.BatchId);
                line.precio_inicial = d.BatchInitialPrice;
                if (sistema.ENVIORMENT.code == "IFRS")
                    line.vida_util_inicial = (int)(Math.Round((double)(d.BatchInitialLifeTime / 12 * 365), 0));
                else
                    line.vida_util_inicial = d.BatchInitialLifeTime;
                line.derecho_credito = d.DetailAllowCredit;
                line.fecha_ing = d.BatchAccountDate;
                line.origen = this.origenes.ById(d.BatchOriginId);
                line.tipo = this.Tipos.ById(d.BatchAssetId);
                line.metodo_reval = this.MetodosRev.ById(d.MethodRevalId);

                line.PartId = d.PartId;
                line.HeadId = d.HeadId;
                line.RefSource = d.HeadRefSource;
                if (WithParameters)
                {
                    var valores = new LIST_PARAM_VALUE();
                    var curr_vals = this.DetallesParametros.ByHead_Sys(d.HeadId, sistema.id);
                    foreach (var par in all_params)
                    {
                        SV_PARAMETER meta_param = this.parametros.ById(par.parameter_id);
                        PARAM_VALUE det;
                        var act_val = curr_vals.Find(x => x.code == meta_param.code);
                        if (act_val == null)
                            det = PARAM_VALUE.NoValue(meta_param);
                        else
                            det = act_val;
                        valores.Add(det);
                    }
                    line.parametros = valores;
                }
                salida.Add(line);
            }

            return salida;
        }
        public List<DETAIL_PROCESS> get_detailed(SV_SYSTEM sistema, DateTime corte, int codigo, string[] aprobados, bool WithParameters, bool CheckPost)
        {
            return get_detailed(sistema, corte, codigo, aprobados, WithParameters, CheckPost, null, null);
        }
        public List<DETAIL_PROCESS> get_detailed(SV_SYSTEM sistema, DateTime corte, int codigo, string[] aprobados)
        {

            return this.get_detailed(sistema, corte, codigo, aprobados, _def_with_param,_def_check_post);
        }
        public List<DETAIL_PROCESS> get_detailed(SV_SYSTEM sistema, DateTime corte, int codigo, bool WithParameters)
        {
            return this.get_detailed(sistema, corte, codigo, _def_apro, WithParameters, _def_check_post);
        }
        public List<DETAIL_PROCESS> get_detailed(SV_SYSTEM sistema, DateTime corte, int codigo)
        {
            return this.get_detailed(sistema, corte, codigo, _def_apro, _def_with_param,_def_check_post);
        }
        public List<DETAIL_PROCESS> get_detailed(SV_SYSTEM sistema, DateTime corte, string[] aprobados, bool WithParameters)
        {
            return this.get_detailed(sistema, corte, 0, aprobados, WithParameters, _def_check_post);
        }
        public List<DETAIL_PROCESS> get_detailed(SV_SYSTEM sistema, DateTime corte, string[] aprobados)
        {
            return this.get_detailed(sistema, corte, 0, aprobados, _def_with_param, _def_check_post);
        }
        public List<DETAIL_PROCESS> get_detailed(SV_SYSTEM sistema, DateTime corte, bool WithParameters)
        {
            return this.get_detailed(sistema, corte, 0, _def_apro, WithParameters,_def_check_post);
        }
        public List<DETAIL_PROCESS> get_detailed(SV_SYSTEM sistema, DateTime corte)
        {
            return this.get_detailed(sistema, corte, 0, _def_apro, _def_with_param,_def_check_post);
        }

        #endregion

        #region Dispose
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        // Public implementation of Dispose pattern callable by consumers.
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
                _zones = null;
                _validaties = null;
                _kinds = null;
                _categories = null;
                _subzones = null;
                _subkinds = null;
                _managements = null;
                _aprovals_states = null;
                _origins = null;
                _type_assets = null;
                _parameters = null;
                _documents = null;
                _systems = null;
                _sys_params = null;

                _context.Dispose();//  = null;
            }

            disposed = true;
        }
        #endregion

        
    }
}
