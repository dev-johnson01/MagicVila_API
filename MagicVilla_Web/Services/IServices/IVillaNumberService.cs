using MagicVilla_Web.Models.Dto;

namespace MagicVilla_Web.Services.IServices
{
    public interface IVillaNumberService
    {
        Task<T> GetAllAsync<T>();
        Task<T> GetAsync<T>(int id);
        Task<T> UpdateAsync<T>(VilaNumberUpdateDTO Dto);
        Task<T> CreateAsync<T>(VilaNumberCreateDTO Dto);
        Task<T> DeleteAsync<T>(int id);
    }
}
