using MagicVila_WebAPI.Models;
using System.Linq.Expressions;

namespace MagicVila_WebAPI.Repository.IRepository
{
    public interface IVilaRepository:IRepository<Vila>
    {
        
        Task<Vila> UpdateAsync(Vila entity);
        
    }
}
