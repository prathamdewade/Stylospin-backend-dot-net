using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stylo_Spin.Dtos;
using Stylo_Spin.Models;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ContactUsController : ControllerBase
    {
        private readonly IContactUsService _service;

        public ContactUsController(IContactUsService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateContact([FromBody] ContactUsDto dto)
        {
            var contact = await _service.CreateAsync(dto);
            return Ok(ApiResponse<TblContactU>.SuccessResponse(contact, "Contact submitted successfully"));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var result = await _service.DeleteAsync(id);
            if (!result)
                return NotFound(ApiResponse<string>.ErrorResponse("Contact not found"));

            return Ok(ApiResponse<string>.SuccessResponse("Contact deleted successfully"));
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllContacts()
        {
            var contacts = await _service.GetAllAsync();
            return Ok(ApiResponse<List<TblContactU>>.SuccessResponse(contacts, "Contacts retrieved successfully"));
        }
    }
}
