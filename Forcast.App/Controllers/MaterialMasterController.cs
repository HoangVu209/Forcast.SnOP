using Forcast.Data.Entities;
using Forcast.Intergration.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Forcast.App.Controllers
{
    public class MaterialMasterController : Controller
    {
        private readonly IMaterialMasterApiClient _materialMasterApiClient;
        private readonly IConfiguration _configuration;


        public MaterialMasterController(IMaterialMasterApiClient materialMasterApiClient,
                                                  IConfiguration configuration)
        {
            _materialMasterApiClient = materialMasterApiClient;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("GetPagedMaterials")]
        public async Task<IActionResult> GetPagedMaterials(int page, int pageSize)
        {
            try
            {
                var materials = await _materialMasterApiClient.GetPagedMaterialMastersAsync(page, pageSize);
                return Json(materials);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Action để lấy thông tin chi tiết material
        [HttpGet] // Ensure to define the route parameter
        public async Task<IActionResult> GetMaterialById(int id)
        {
            try
            {
                var material = await _materialMasterApiClient.GetMaterialMasterByIdAsync(id);
                if (material == null)
                {
                    return Json(new { success = false, message = "Material not found." });
                }
                return Json(new { success = true, data = material });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMaterialMaster(int id, [FromBody] MaterialMaster material)
        {
            try
            {
                var updatedMaterial = await _materialMasterApiClient.UpdateMaterialMasterAsync(id, material);
                if (updatedMaterial == false)
                {
                    return NotFound(new { message = "Material not found." });
                }
                return Ok(updatedMaterial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        // Trong MaterialMasterController
        [HttpPost]
        public async Task<IActionResult> AddMaterial([FromBody] MaterialMaster material)
        {
            try
            {
                var result = await _materialMasterApiClient.AddMaterialMasterAsync(material);
                if (result)
                {
                    return Json(new { success = true, message = "Material added successfully" });
                }
                else
                {
                    return Json(new { success = false, message = "Failed to add material" });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }

        // Trong MaterialMasterController
        [HttpDelete]  // Cấu hình route cho yêu cầu DELETE
        public async Task<IActionResult> DeleteMaterial(int id)
        {
            try
            {
                var success = await _materialMasterApiClient.RemoveMaterialMasterAsync(id);
                if (success)
                {
                    return Json(new { success = true, message = "Material deleted successfully." });
                }
                else
                {
                    return NotFound(new { success = false, message = "Material not found." });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = ex.Message });
            }
        }


    }
}

