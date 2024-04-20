using Forcast.Data.Entities;
using Forcast.Data.Infrastructure;
using Forcast.Models.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forcast.Intergration.Contracts
{
    public interface IMaterialMasterApiClient
    {
        public Task<PagedList<MaterialMaster>?> GetPagedMaterialMastersAsync(int pageNumber, int pageSize);
        public Task<MaterialMaster?> GetMaterialMasterByIdAsync(int id);
        public Task<bool> UpdateMaterialMasterAsync(int id, MaterialMaster materialMaster);
        public Task<bool> AddMaterialMasterAsync(MaterialMaster materialMaster);
        public Task<bool> RemoveMaterialMasterAsync(int id);
    }
}
