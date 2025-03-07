using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Services;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TechChallenge.ContactsInsertProducer.Domain
{
    public static class ServicesDependency
    {
        public static IServiceCollection AddServicesDependency(this IServiceCollection service)
        {
            service.AddScoped<IContactService, ContactService>();

            return service;
        }
    }
}
