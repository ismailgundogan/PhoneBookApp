using Microsoft.EntityFrameworkCore;
using PhoneBook.Core.Entity;
using PhoneBook.DataAccess.Context;
using PhoneBook.DataAccess.Interfaces;

namespace PhoneBook.DataAccess.Repo
{
    public class PersonelRepository: IPersonelRepository
    {
        private readonly AppDbContext _context;
        
        public PersonelRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Personel personel)
        {
            await _context.Personels.AddAsync(personel);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var personel = await _context.Personels.FindAsync(id);
            if (personel != null)
            {
                _context.Personels.Remove(personel);
                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<Personel>> GetAllAsync()
        {
            return await _context.Personels.ToListAsync();
        }

        public async Task<Personel> GetByIdAsync(int id)
        {
            return await _context.Personels.FindAsync(id);
        }

        public async Task UpdateAsync(Personel personel)
        {
            _context.Personels.Update(personel);
            await _context.SaveChangesAsync();
        }
    }
}
