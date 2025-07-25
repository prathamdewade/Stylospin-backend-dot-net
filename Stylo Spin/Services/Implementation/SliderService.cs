using Stylo_Spin.Dtos;
using Stylo_Spin.Helper;
using Stylo_Spin.Models;
using Stylo_Spin.Repository.Defination;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Services.Implementation
{
    public class SliderService : ISliderService
    {
        private readonly ISliderRepository _repo;

        public SliderService(ISliderRepository repo)
        {
            _repo = repo;
        }

        public async Task<TblSlider> AddSliderAsync(SliderDto dto)
        {
            byte[] imageData = await ImageHelper.ConvertToBytesAsync(dto.Image);
            var slider = new TblSlider
            {
                ImageData = imageData,
                ImageName = dto.Image?.FileName,
                Description = dto.Description

            };

            var result = await _repo.AddAsync(slider);
            if (!result) throw new Exception("Failed to add slider");

            return slider;
        }

        public async Task<bool> DeleteSliderAsync(int id)
        {
            var slider = await _repo.GetByIdAsync(id);
            if (slider == null) return false;

            return await _repo.DeleteAsync(slider);
        }

        public async Task<List<TblSlider>> GetAllSlidersAsync()
        {
            return await _repo.GetAllAsync();
        }

        public async Task<TblSlider> GetSliderByIdAsync(int id)
        {
            var slider = await _repo.GetByIdAsync(id);
            if (slider == null) throw new KeyNotFoundException("Slider not found");
            return slider;
        }

        public async Task<bool> UpdateSlider(int id, SliderDto dto)
        {
            var slider = await _repo.GetByIdAsync(id);
            if (slider == null) throw new KeyNotFoundException("Slider not found");
            if (dto.Image != null)
            {
                slider.ImageData = await ImageHelper.ConvertToBytesAsync(dto.Image);
                slider.ImageName = dto.Image.FileName;
            }
            slider.Description = dto.Description;
            var result = await _repo.UpdateSliderAsync(slider);
            if (!result) throw new Exception("Failed to update slider");
            return result;

        }
    }
}
