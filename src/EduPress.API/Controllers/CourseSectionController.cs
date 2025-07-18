using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.CourseSectionModel;
using EduPress.Application.Services.Implement;
using EduPress.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPress.API.Controllers
{
    public class CourseSectionController : ApiController
    {
        private readonly ICourseSectionService _courseSectionService;

        public CourseSectionController(ICourseSectionService courseSectionService)
        {
            _courseSectionService = courseSectionService;
        }

        [HttpGet("GetById/{Id}")]
        [Authorize(Policy = "AdminOrCandidate")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var res = await _courseSectionService.GetByIdAsync(id);
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
            var res = await _courseSectionService.GetAllAsync();
            if (!res.Succedded)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpPost("Create")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateCourseSectionModel create)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var responce = await _courseSectionService.CreateAsync(create);
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
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateCourseSectionModel update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var responce = await _courseSectionService.UpdateAsync(id, update);
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
                var result = await _courseSectionService.DeleteAsync(id);
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
