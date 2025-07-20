using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPress.API.Controllers
{
    public class CategoriesController : ApiController
    {
        private readonly ICategoryService _categoryService;
        private readonly ICategoryExcelImportService _categoryExcelImportService;

        public CategoriesController(ICategoryService categoryService, 
            ICategoryExcelImportService categoryExcelImportService)
        {
            _categoryService = categoryService;
            _categoryExcelImportService = categoryExcelImportService;
        }

        [HttpPost("Import-Category")]
        public async Task<IActionResult> ImportCategoryExcelAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Fayl yuborilmadi");

            await _categoryExcelImportService.ImportFromExcelAsync(file);
            return Ok("Import muvaffaqiyatli tugadi");
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Policy = "AdminOrCandidate")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var res = await _categoryService.GetByIdAsync(id);
                if (!res.Succedded)
                    return BadRequest(res);

                return Ok(res);
            }
            catch(Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        [Authorize(Policy = "AdminOrCandidate")]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _categoryService.GetAllAsync();
            if(!res.Succedded)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpPost("Create")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCategoriesModel create)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var responce = await _categoryService.CreateAsync(create);
                if (!responce.Succedded)
                    return BadRequest(responce);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("Update")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateAsync(UpdateCategoriesModel update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var responce = await _categoryService.UpdateAsync(update);
                if (!responce.Succedded)
                    return BadRequest(responce);

                return Ok(responce);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("Delete/{id:guid}")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            try
            {
                var result = await _categoryService.DeleteAsync(id);
                if (!result.Succedded)
                    return BadRequest(result);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
