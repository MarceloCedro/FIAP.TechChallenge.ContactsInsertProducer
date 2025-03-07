using FIAP.TechChallenge.ContactsInsertProducer.Domain.Entities;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Services;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChallenge.ContactsInsertProducer.Domain.Services
{
    public class ContactService(IContactRepository contactRepository, ILogger<ContactService> logger) : IContactService
    {
        private readonly IContactRepository _contactRepository = contactRepository;
        private readonly ILogger<ContactService> _logger = logger;

        public async Task InsertAsync(Contact contact)
        {
            try
            {
                await _contactRepository.AddAsync(contact);
            }
            catch (Exception)
            {
                var message = "Some error occour when trying to insert new Contact.";
                _logger.LogError(message);
                throw new Exception(message);
            }
        }
    }
}