using Azure;
using Forcast.Data.Entities;
using Forcast.Data.Infrastructure;
using Forcast.Intergration.Contracts;
using Forcast.Models.Requests;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forcast.Intergration.ApiClients
{
    public class MaterialMasterApiClient: BaseApiClient, IMaterialMasterApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public MaterialMasterApiClient(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PagedList<MaterialMaster>?> GetPagedMaterialMastersAsync(int pageNumber, int pageSize)
        {
            try
            {
                var response = await GetPagedAsync("api/materialmaster/GetPaged", pageNumber, pageSize);
                if (response.StatusCode == Code.OK && response.Content != null)
                {
                    // Deserialize the outer ApiResponse to access the inner JSON content
                    var apiResponse = JsonConvert.DeserializeObject<RequestResponse>(response.Content);
                    if (apiResponse != null && !string.IsNullOrEmpty(apiResponse.Content))
                    {
                        // Deserialize the actual PagedList<MaterialMaster> from the inner content
                        var pagedList = JsonConvert.DeserializeObject<PagedList<MaterialMaster>>(apiResponse.Content);
                        return pagedList;
                    }
                }
                else
                {
                    // Handle error or null response
                    Console.WriteLine(response.Message ?? "No error message provided.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetPagedMaterialMastersAsync: " + ex.Message);
            }
            return null;
        }

        public async Task<MaterialMaster ?> GetMaterialMasterByIdAsync(int id)
        {
            try {
                var response = await GetByIdAsync<MaterialMaster>(id, "api/materialmaster/GetMaterialMasterById");
                if(response.StatusCode == Code.OK && response.Content != null)
                {
                    var materialMaster = JsonConvert.DeserializeObject<MaterialMaster>(response.Content);
                    if (materialMaster != null)
                    {
                        return materialMaster;
                    }
                    else
                    {
                        Console.WriteLine(response.Message ?? "No error message provided.");
                    }

                }

            }
            catch(Exception ex) { Console.WriteLine("Error in GetPagedMaterialMastersAsync: " + ex.Message); }
            return null;
        }


        public async Task<bool> UpdateMaterialMasterAsync(int id, MaterialMaster materialMaster)
        {
            try
            {
                return await UpdateAsync(id, materialMaster, "api/materialmaster/UpdateMaterialMaster");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateMaterialMasterAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> AddMaterialMasterAsync(MaterialMaster materialMaster)
        {
            try
            {
                return await AddAsync(materialMaster, "api/materialmaster/AddMaterialMaster");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in AddMaterialMasterAsync: {ex.Message}");
                return false;
            }
        }
        public async Task<bool> RemoveMaterialMasterAsync(int id)
        {
            try
            {
                return await RemoveAsync(id, "api/materialmaster/DeleteMaterialMaster");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RemoveMaterialMasterAsync: {ex.Message}");
                return false;
            }
        }
    }
}
