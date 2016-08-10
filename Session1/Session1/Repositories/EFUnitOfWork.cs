using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Session1.Models;
using Session1.Models.DbModel;

namespace Session1.Repositories
{
    public class EFUnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; set; }

        public EFUnitOfWork()
        {
            Context = new DbModel();
        }

        public void Save()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }
    }
}