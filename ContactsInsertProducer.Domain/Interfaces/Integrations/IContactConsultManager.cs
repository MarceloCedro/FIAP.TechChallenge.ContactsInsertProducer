using FIAP.TechChallenge.ContactsInsertProducer.Domain.Entities;

namespace FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Integrations
{
    public interface IContactConsultManager
    {
        Task<string> GetToken();
        Task<Contact> GetContactByEmail(string email, string token);
    }
}
