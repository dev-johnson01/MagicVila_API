using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> UpdateAsync<T>(VilaUpdateDTO Dto);
        Task<T> CreateAsync<T>(VilaCreateDTO Dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
