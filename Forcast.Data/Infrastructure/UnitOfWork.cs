using Forcast.Data.Entities;
using Forcast.Data.Infrastructure.Interfaces;
using Forcast.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forcast.Data.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Task01Context _context;


        public IMaterialMasterRepo MaterialMasterRepo { get; private set; }


        public UnitOfWork(Task01Context context)
        {
            _context = context;
            MaterialMasterRepo = new MaterialMasterRepo(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
