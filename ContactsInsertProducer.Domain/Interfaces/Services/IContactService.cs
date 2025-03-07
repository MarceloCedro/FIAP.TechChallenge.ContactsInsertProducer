using FIAP.TechChallenge.ContactsInsertProducer.Domain.Entities;

namespace FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Services
{
    public interface IContactService
    {
        Task InsertAsync(Contact contact);
    }
}