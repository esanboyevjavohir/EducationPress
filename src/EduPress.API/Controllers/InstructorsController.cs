using EduPress.Application.Models.CategoriesModel;
using EduPress.Application.Models.InstructorsModel;
using EduPress.Application.Services.Implement;
using EduPress.Application.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EduPress.API.Controllers
{
    public class InstructorsController : ApiController
    {
        private readonly IInstructorsService _instructorsService;

        public InstructorsController(IInstructorsService instructorsService)
        {
            _instructorsService = instructorsService;
        }

        [HttpGet("GetById/{Id}")]
        [Authorize(Policy = "AdminOrCandidate")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            try
            {
                var res = await _instructorsService.GetByIdAsync(id);
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
            var res = await _instructorsService.GetAllAsync();
            if (!res.Succedded)
                return BadRequest(res);

            return Ok(res);
        }

        [HttpPost("Create")]
        [Authorize(Policy = "RequireAdminRole")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateInstructorsModel create)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var responce = await _instructorsService.CreateAsync(create);
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
        public async Task<IActionResult> UpdateAsync(Guid id, UpdateInstructorsModel update)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var responce = await _instructorsService.UpdateAsync(id, update);
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
                var result = await _instructorsService.DeleteAsync(id);
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
