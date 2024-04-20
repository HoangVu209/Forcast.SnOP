using Forcast.Data.Entities;
using Forcast.Data.Infrastructure;
using Forcast.Models.Requests.MaterialMasterReq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forcast.Service.Constracts
{
    public interface IMaterialMasterService
    {
        string GetErrorMessage();
        Task<MaterialMaster[]?> GetAllAsync();

        Task<PagedList<MaterialMaster>?> GetPagedAsync(int pageNumber, int pageSize);

        Task<MaterialMaster ?> GetMaterialMasterByIdAsync(int id);

        Task<int> UpdateMaterialMasterAsync(MaterialMaster materialMaster);

        public Task<int> AddMaterialMasterAsync(MaterialMaster materialMaster);

        public Task<int> RemoveMaterialMasterAsync(int id);

    }
}
