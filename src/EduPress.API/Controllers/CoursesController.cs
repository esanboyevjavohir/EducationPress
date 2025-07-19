using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.CoursesModel;
using EduPress.Application.Services.Implement;
using EduPress.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPress.API.Controllers
{
    public class CoursesController : ApiController
    {
        private readonly ICoursesService _coursesService;

        public CoursesController(ICoursesService coursesService)
        {
            _coursesService = coursesService;
        }

        [HttpGet("GetById/{id}")]
        [Authorize(Policy = "AdminOrCandidate")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var res = await _coursesService.GetByIdAsync(id);
                if (!res.IsValid)
                    return BadRequest(res.Errors);

                return Ok(res.Result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpGet("GetAll")]
        [Authorize(Policy = "AdminOrCandidate")]
        public async Task<IActionResult> GetAllAsync()
        {
            var res = await _coursesService.GetAllAsync();
            if (!res.IsValid)
                return BadRequest(res.Errors);

            return Ok(res.Result);
        }

        [HttpPost("Create")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCoursesModel create)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var responce = await _coursesService.CreateAsync(create);
                if (!responce.IsValid)
                    return BadRequest(responce.Errors);

                return Ok(responce.Result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpPut("Update")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCoursesModel update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var responce = await _coursesService.UpdateAsync(id, update);
                if (!responce.IsValid)
                    return BadRequest(responce.Errors);

                return Ok(responce.Result);
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
                var result = await _coursesService.DeleteAsync(id);
                if (!result.IsValid)
                    return BadRequest(result.Errors);

                return Ok(result.Result);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
