using FIAP.TechChallenge.ContactsInsertProducer.Domain.Entities;

namespace FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Repositories
{
    public interface IContactRepository
    {
        Task AddAsync(Contact contact);
        Task UpdateAsync(Contact contact);
        Task<Contact> GetByIdAsync(int id);
        Task<IEnumerable<Contact>> GetAllAsync();
        Task<IEnumerable<Contact>> GetByAreaCodeAsync(string areaCode);
        Task DeleteAsync(int id);
    }
}
