using FIAP.TechChallenge.ContactsInsertProducer.Domain.Entities;

namespace FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Applications
{
    public interface ITokenApplication
    {
        public string GetToken(User usuario);
    }
}
