using Forcast.Data.Entities;
using Forcast.Models.Requests;
using Forcast.Models.Views;
using Forcast.Service.Constracts;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Forcast.API.Controllers
{
    [Route("api/materialmaster")]
    [ApiController]
    public class MaterialMasterController : ControllerBase
    {
        private readonly IMaterialMasterService _materialMasterService;

        public MaterialMasterController(IMaterialMasterService materialMasterService)
        {
            _materialMasterService = materialMasterService;
        }

        [HttpGet("GetAll")]
        public async Task<RequestResponse> GetAll()
        {
                try
                {
                    var materials = await _materialMasterService.GetAllAsync();
                    if (materials == null || !materials.Any())
                    {
                        string errors = _materialMasterService.GetErrorMessage();
                        return new RequestResponse
                        {
                            StatusCode = Code.NotFound,
                            Content = !String.IsNullOrEmpty(errors) ? errors : "No materials found.",
                            Message = "Get material failed!"
                        };
                    }
                return new RequestResponse
                {
                    StatusCode = Code.OK,
                    Content = JsonConvert.SerializeObject(materials)
                    };
            }
                catch (Exception ex)
                {
                    return new RequestResponse
                    {
                        StatusCode = Code.BadRequest,
                        Content = ex.Message,
                        Message = "Get failed!"
                    };
                }
        }
        // GET: api/MaterialMaster/GetAll?pageNumber=1&pageSize=10
        [HttpGet("GetPaged")]
        public async Task<RequestResponse> GetPaged(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var pagedMaterials = await _materialMasterService.GetPagedAsync(pageNumber, pageSize);
                // Check both for null reference on pagedMaterials and pagedMaterials.Items
                if (pagedMaterials == null || pagedMaterials.Items == null || !pagedMaterials.Items.Any())
                {
                    string errors = _materialMasterService.GetErrorMessage();
                    return new RequestResponse
                    {
                        StatusCode = Code.NotFound,
                        Content = !String.IsNullOrEmpty(errors) ? errors : "No materials found.",
                        Message = "Get material failed!"
                    };
                }

                return new RequestResponse
                {
                    StatusCode = Code.OK,
                    Content = JsonConvert.SerializeObject(pagedMaterials),
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new RequestResponse
                {
                    StatusCode = Code.BadRequest,
                    Content = ex.Message,
                    Message = "Get failed!"
                };
            }
        }

        [HttpPut("UpdateMaterialMaster")]
        public async Task<IActionResult> UpdateMaterialMaster([FromBody] MaterialMaster materialMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _materialMasterService.UpdateMaterialMasterAsync(materialMaster);
                if (result == 0)
                {
                    return NotFound($"Material with ID {materialMaster.Id} not found.");
                }
                return Ok($"Material with ID {materialMaster.Id} updated successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while updating the material: {ex.Message}");
            }
        }
        [HttpGet("GetMaterialMasterById/{id}")]
        public async Task<IActionResult> GetMaterialMasterById(int id)
        {
            try
            {
                var materialMaster = await _materialMasterService.GetMaterialMasterByIdAsync(id);
                if (materialMaster == null)
                {
                    return NotFound($"Material with ID {id} not found.");
                }
                return Ok(materialMaster);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while retrieving material master: {ex.Message}");
            }
        }

        [HttpPost("AddMaterialMaster")]
        public async Task<IActionResult> AddMaterialMaster([FromBody] MaterialMaster materialMaster)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _materialMasterService.AddMaterialMasterAsync(materialMaster);
                if (result == 0)
                {
                    return NotFound("Add failed.");
                }
                return Ok($"Material was added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while add the material: {ex.Message}");
            }
        }

        [HttpDelete("DeleteMaterialMaster/{id}")]
        public async Task<IActionResult> DeleteMaterialMaster(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _materialMasterService.RemoveMaterialMasterAsync(id);
                if (result == 0)
                {
                    return NotFound("Remove failed.");
                }
                return Ok($"Material was removed successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred while remove the material: {ex.Message}");
            }
        }
    }
}
