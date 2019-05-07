using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

using AFN_WF_C.ServiceProcess.DataContract;

namespace AFN_WF_C.ServiceProcess.Repositories
{
    class ALL : IDisposable
    {
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

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
        private PARAMETERS _parameters;
        private DOCUMENTS _documents;
        private SYSTEMS _systems;
        private SYSTEMS_PARAMETERS _sys_params;

        public ALL(AFN2Entities context)
        {
            _context = context;
        }

        public ZONES zonas
        {
            get
            {
                if (_zones == null) { _zones = new ZONES(_context.ZONES); }
                return _zones;
            }
        }
        public VALIDATIES validaciones
        {
            get
            {
                if (_validaties == null) { _validaties = new VALIDATIES(_context.VALIDATIES); }
                return _validaties;
            }
        }
        public KINDS clases
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
        public APROVALS_STATES estados
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
        public TYPES_ASSETS tipos
        {
            get
            {
                if (_type_assets == null) { _type_assets = new TYPES_ASSETS(_context.TYPES_ASSETS); }
                return _type_assets;
            }
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
    }
}
