using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.Application;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.EntityDTOs;

namespace FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Applications
{
    public interface IContactApplication
    {
        Task<UpsertContactResponse> AddContactAsync(ContactCreateDto contactCreateDto);
    }
}