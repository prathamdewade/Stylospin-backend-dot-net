using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stylo_Spin.Dtos;
using Stylo_Spin.Models;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AboutUsController : ControllerBase
    {
        private readonly IAboutUsService _service;

        public AboutUsController(IAboutUsService service)
        {
            _service = service;
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetAll()
        {
            var data = await _service.GetAllAsync();
            if(data.Count>0)
               data= data.OrderByDescending(ob => ob.Id).ToList();
            
            return Ok(ApiResponse<List<AboutU>>.SuccessResponse(data, "All AboutUs records retrieved successfully."));
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            return Ok(ApiResponse<AboutU>.SuccessResponse(item, "AboutUs record retrieved successfully."));
        }

        [HttpPost("add")]
        public async Task<IActionResult> Create([FromForm] AboutuUsDto dto)
        {
            var created = await _service.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById),
                new { id = created.Id },
                ApiResponse<AboutU>.SuccessResponse(created, "AboutUs record created successfully."));
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] AboutuUsDto dto)
        {
            var updated = await _service.UpdateAsync(id, dto);
            return Ok(ApiResponse<AboutU>.SuccessResponse(updated, "AboutUs record updated successfully."));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.ErrorResponse("AboutUs record not found for deletion."));
            return Ok(ApiResponse<string>.SuccessResponse("AboutUs record deleted successfully."));
        }
    }
}
