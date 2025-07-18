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

        [HttpGet("GetById/{Id}")]
        [Authorize(Policy = "AdminOrCandidate")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var res = await _coursesService.GetByIdAsync(id);
                if (!res.Succedded)
                    return BadRequest(res);

                return Ok(res);
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
            if (!res.Succedded)
                return BadRequest(res);

            return Ok(res);
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
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCoursesModel update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var responce = await _coursesService.UpdateAsync(id, update);
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
                var result = await _coursesService.DeleteAsync(id);
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
