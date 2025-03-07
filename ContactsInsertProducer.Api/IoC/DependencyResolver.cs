using FIAP.TechChallenge.ContactsInsertProducer.Application;
using FIAP.TechChallenge.ContactsInsertProducer.Domain;
using FIAP.TechChallenge.ContactsInsertProducer.Infrastructure;

namespace FIAP.TechChallenge.ContactsInsertProducer.Api.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services, string connectionString)
        {
            services.AddRepositoriesDependency();
            services.AddDbContextDependency(connectionString);
            services.AddServicesDependency();
            services.AddApplicationDependency();
            services.AddAuthenticationDependency();
            services.AddIntegrationsDependency();
        }
    }
}
