using Microsoft.AspNetCore.Http;

namespace Stylo_Spin.Dtos
{
    public class SliderDto
    {
        public IFormFile? Image { get; set; }
        public string Description { get; set; } 
    }
}
