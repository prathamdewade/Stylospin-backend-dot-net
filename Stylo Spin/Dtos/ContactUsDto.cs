namespace Stylo_Spin.Dtos
{
    public class ContactUsDto
    {
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? ContactNumber { get; set; }
        public string Query { get; set; } = null!;
        public string ? Subject { get; set; }
    }
}
