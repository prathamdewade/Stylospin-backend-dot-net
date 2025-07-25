using Stylo_Spin.Dtos;
using Stylo_Spin.Helper;
using Stylo_Spin.Models;
using Stylo_Spin.Repository.Defination;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Services.Implementation
{
    public class AboutUsService : IAboutUsService
    {
        private readonly IAboutUsRepository _repo;

        public AboutUsService(IAboutUsRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<AboutU>> GetAllAsync()
        {
            return await _repo.GetAboutUsAsync();
        }

        public async Task<AboutU> GetByIdAsync(int id)
        {
            var about = await _repo.GetAboutUSByIdAsync(id);
            if (about == null) throw new KeyNotFoundException("AboutUs entry not found.");
            return about;
        }

        public async Task<AboutU> CreateAsync(AboutuUsDto dto)
        {
            byte[]? imageData = await ImageHelper.ConvertToBytesAsync(dto.Image);

            var entity = new AboutU
            {
                Heading = dto.Heading,
                Paragraph = dto.Paragraph,
                SubHeading = dto.SubHeading,
                // Ensure required fields are not null
                ImageData = imageData,
                ImageName = dto.Image?.FileName
            };

            var result = await _repo.CreateAbobutUs(entity); // fixed name
            if (!result) throw new Exception("Failed to create AboutUs entry.");

            return entity;
        }


        public async Task<AboutU> UpdateAsync(int id, AboutuUsDto dto)
        {
            var existing = await _repo.GetAboutUSByIdAsync(id);
            if (existing == null) throw new KeyNotFoundException("AboutUs entry not found.");

            byte[]? imageData = await ImageHelper.ConvertToBytesAsync(dto.Image);

            existing.Heading = dto.Heading;
            existing.Paragraph = dto.Paragraph;
            existing.SubHeading = dto.SubHeading;
          //  existing.SubParagraph = dto.SubParagraph;
            if (imageData != null)
            {
                existing.ImageData = imageData;
                existing.ImageName = dto.Image?.FileName;
            }

            var result = await _repo.UpdateAysnc(existing);
            if (!result) throw new Exception("Failed to update AboutUs entry.");

            return existing;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var existing = await _repo.GetAboutUSByIdAsync(id);
            if (existing == null) return false;
            return await _repo.DeleteAysnc(existing);
        }
    }
}
