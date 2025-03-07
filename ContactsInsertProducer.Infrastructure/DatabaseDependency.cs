using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsInsertProducer.Infrastructure.Repositories;
using FIAP.TechChallenge.ContactsInsertProducer.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TechChallenge.ContactsInsertProducer.Infrastructure
{
    public static class DatabaseDependency
    {
        public static IServiceCollection AddRepositoriesDependency(this IServiceCollection service)
        {
            service.AddScoped<IContactRepository, ContactRepository>();

            return service;
        }

        public static IServiceCollection AddDbContextDependency(this IServiceCollection service, string connectionString)
        {
            service.AddDbContext<ContactsDbContext>(options => options.UseMySql(connectionString,
                                                               new MySqlServerVersion(new Version(8, 0, 21)),
                                                               mySqlOptions => mySqlOptions.MigrationsAssembly("FIAP.TechChallenge.ContactsInsertProducer.Infrastructure")));

            return service;
        }
    }
}
