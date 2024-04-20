using Forcast.Data.Entities;
using Forcast.Data.Infrastructure;
using Forcast.Data.Infrastructure.Interfaces;
using Forcast.Service.Constracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forcast.Service.Services
{
    public class MaterialMasterService : AbsService, IMaterialMasterService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MaterialMasterService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<MaterialMaster[]?> GetAllAsync()
        {
            var result = await _unitOfWork.MaterialMasterRepo.GetAllAsync();

            return result?.ToArray();
        }

        public string GetErrorMessage()
        {
            return GetError();
        }

        public async Task<MaterialMaster?> GetMaterialMasterByIdAsync(int id)
        {
            var result = await _unitOfWork.MaterialMasterRepo.GetByIdAsync(id);
            return result;
        }

        public async Task<PagedList<MaterialMaster>?> GetPagedAsync(int pageNumber, int pageSize)
        {
            var result = await _unitOfWork.MaterialMasterRepo.GetPagedAsync(pageNumber, pageSize);
            return result; 
        }

        public async Task<int> UpdateMaterialMasterAsync(MaterialMaster materialMaster)
        {
            var result = await _unitOfWork.MaterialMasterRepo.UpdateAsync(materialMaster);
            return result;
        }

        public async Task<int> AddMaterialMasterAsync(MaterialMaster materialMaster)
        {
            var result = await _unitOfWork.MaterialMasterRepo.AddAsync(materialMaster);
            return result;  
        }

        public async Task<int> RemoveMaterialMasterAsync(int id)
        {
         
           return await _unitOfWork.MaterialMasterRepo.RemoveAsync(id);

        }
    }
}
