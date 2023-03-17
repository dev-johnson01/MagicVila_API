using MagicVila_WebAPI.Data;
using MagicVila_WebAPI.Models;
using MagicVila_WebAPI.Repository.IRepository;

namespace MagicVila_WebAPI.Repository
{
    public class VilaNumberRepository : Repository<VilaNumber>, IVilaNumberRepository
    {
        private readonly ApplicationDbContext _db;
        public VilaNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public async Task<VilaNumber> UpdateAsync(VilaNumber entity)
        {
           _db.VilaNumbers.Update(entity);
           await  _db.SaveChangesAsync();
            return entity;

        }

    }

}
