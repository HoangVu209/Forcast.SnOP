using Forcast.Data.Entities;
using Forcast.Data.Infrastructure;
using Forcast.Data.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forcast.Data.Repositories
{
    public interface IMaterialMasterRepo : IGenericRepository<MaterialMaster>
    {
    }
    public class MaterialMasterRepo : GenericRepository<MaterialMaster>, IMaterialMasterRepo
    {
        public MaterialMasterRepo(Task01Context context) : base(context) { }
    }
}
