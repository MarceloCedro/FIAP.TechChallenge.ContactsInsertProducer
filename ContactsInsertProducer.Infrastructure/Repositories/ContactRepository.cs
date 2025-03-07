using FIAP.TechChallenge.ContactsInsertProducer.Domain.Entities;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsInsertProducer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FIAP.TechChallenge.ContactsInsertProducer.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository

    {
        private readonly ContactsDbContext _context;

        public ContactRepository(ContactsDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Contact contact)
        {
            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Contact contact)
        {
            _context.Contacts.Update(contact);
            await _context.SaveChangesAsync();
        }

        public async Task<Contact> GetByIdAsync(int id)
            => await _context.Contacts.FindAsync(id);

        public async Task<IEnumerable<Contact>> GetAllAsync()
            => await _context.Contacts.ToListAsync();

        public async Task<IEnumerable<Contact>> GetByAreaCodeAsync(string areaCode)
            => await _context.Contacts.Where(c => c.AreaCode == areaCode).ToListAsync();

        public async Task DeleteAsync(int id)
        {
            var contact = await GetByIdAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
        }
    }
}