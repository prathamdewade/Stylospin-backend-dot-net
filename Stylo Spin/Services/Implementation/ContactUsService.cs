using Stylo_Spin.Dtos;
using Stylo_Spin.Models;
using Stylo_Spin.Repository.Defination;
using Stylo_Spin.Services.Defination;

namespace Stylo_Spin.Services.Implementation
{
    public class ContactUsService : IContactUsService
    {
        private readonly IContactUsRepository _repo;

        public ContactUsService(IContactUsRepository repo)
        {
            _repo = repo;
        }

        public async Task<TblContactU> CreateAsync(ContactUsDto dto)
        {
            var contact = new TblContactU
            {
                Name = dto.Name,
                Email = dto.Email,
                ContactNumber = dto.ContactNumber,
                Query = dto.Query,
                Subject = dto.Subject // Ensure Subject is not null
            };

            var success = await _repo.CreateAsync(contact);
            if (!success)
                throw new Exception("Failed to save contact");

            return contact;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var contact = await _repo.GetByIdAsync(id);
            if (contact == null) return false;
            return await _repo.DeleteAsync(contact);
        }

        public async Task<List<TblContactU>> GetAllAsync()
        {
            return await _repo.GetAllAsync();
        }
    }
}
