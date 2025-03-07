using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Integrations;
using FIAP.TechChallenge.ContactsInsertProducer.Integrations;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TechChallenge.ContactsInsertProducer.Infrastructure
{
    public static class IntegrationsDependency
    {
        public static IServiceCollection AddIntegrationsDependency(this IServiceCollection service)
        {
            service.AddScoped<IContactConsultManager, ContactConsultManager>();

            return service;
        }
    }
}
