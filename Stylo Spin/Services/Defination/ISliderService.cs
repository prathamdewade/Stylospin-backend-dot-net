using Stylo_Spin.Dtos;
using Stylo_Spin.Models;

namespace Stylo_Spin.Services.Defination
{
    public interface ISliderService
    {
        Task<List<TblSlider>> GetAllSlidersAsync();
        Task<TblSlider> GetSliderByIdAsync(int id);
        Task<TblSlider> AddSliderAsync(SliderDto dto);
        Task<bool> UpdateSlider(int id, SliderDto dto);
        Task<bool> DeleteSliderAsync(int id);
    }
}
