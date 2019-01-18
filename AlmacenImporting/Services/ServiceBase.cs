using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AlmacenImporting.Models;
using System.Data.Entity;

namespace AlmacenImporting.Services
{
    public abstract class ServiceBase<T> where T : class
    {
        private AlmacenImportingContext db;
        protected readonly IDbSet<T> dbset;

        protected AlmacenImportingContext DBContext
        {
            get { return db ?? (db = new AlmacenImportingContext()); }
        }

        protected ServiceBase()
        {
            db = new AlmacenImportingContext();
            dbset = DBContext.Set<T>();
        }

        protected ServiceBase(AlmacenImportingContext dbContext)
        {
            this.db = dbContext;
            dbset = DBContext.Set<T>();
        }

        public void Dispose()
        {
            this.db.Dispose();
        }
    }
}