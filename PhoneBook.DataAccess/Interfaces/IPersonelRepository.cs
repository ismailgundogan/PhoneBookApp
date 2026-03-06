using PhoneBook.Core.Entity;

namespace PhoneBook.DataAccess.Interfaces
{
    public interface IPersonelRepository
    {
        Task<List<Personel>> GetAllAsync();
        Task<Personel> GetByIdAsync(int id);
        Task AddAsync(Personel personel);
        Task UpdateAsync(Personel personel);
        Task DeleteAsync(int id);
    }
}
