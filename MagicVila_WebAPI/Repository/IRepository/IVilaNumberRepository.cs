using MagicVila_WebAPI.Models;

namespace MagicVila_WebAPI.Repository.IRepository
{
    public interface IVilaNumberRepository: IRepository<VilaNumber>
    {
        Task<VilaNumber> UpdateAsync(VilaNumber entity);
    }
}
