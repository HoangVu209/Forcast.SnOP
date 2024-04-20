using Forcast.Data.Entities;
using Forcast.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Forcast.Data.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMaterialMasterRepo MaterialMasterRepo { get; }
        int Complete();
    }
}
