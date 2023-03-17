using MagicVila_WebAPI.Data;
using MagicVila_WebAPI.Models;
using MagicVila_WebAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace MagicVila_WebAPI.Repository
{
    public class VilaRepository :Repository<Vila>, IVilaRepository
    {
        private readonly ApplicationDbContext _db;
        public VilaRepository(ApplicationDbContext db): base(db)
        {
            _db = db;
        }      
       

        public async Task<Vila> UpdateAsync(Vila entity)
        {
            entity.UpdatedDate = DateTime.Now;
            _db.Vilas.Update(entity);
            await _db.SaveChangesAsync();

            return entity;
        }
    }
}

