using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.Application;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.EntityDTOs;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Entities;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Applications;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Integrations;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace FIAP.TechChallenge.ContactsInsertProducer.Application.Applications
{
    public class ContactApplication(IContactService contactService,
                                    IContactConsultManager contactConsultManager,
                                    ILogger<ContactApplication> logger,
                                    IConfiguration configuration,
                                    IBus bus) : IContactApplication
    {
        private readonly IContactService _contactService = contactService;
        private readonly ILogger<ContactApplication> _logger = logger;
        private readonly IContactConsultManager _contactConsultManager = contactConsultManager;
        private readonly IBus _bus = bus;
        private readonly IConfiguration _configuration = configuration;

        public async Task<UpsertContactResponse> AddContactAsync(ContactCreateDto contactCreateDto)
        {
            var insertResult = new UpsertContactResponse();
            var token = await _contactConsultManager.GetToken();
            if (token == null)
            {
                insertResult.Success = false;
                insertResult.Message = $"Houve um problema ao se obter o token para chamada externa.";
            }
            else
            {
                var contactObject = await _contactConsultManager.GetContactByEmail(contactCreateDto?.Email, token);
                                
                if (contactObject != null)
                {
                    insertResult.Success = false;
                    insertResult.Message = $"Email de contato ja existente na base.";
                }
                else
                {
                    try
                    {
                        var massTransitObject = ChargeMassTransitObject();

                        var endpoint = await _bus.GetSendEndpoint(new Uri($"queue:{massTransitObject.QueueName}"));

                        await endpoint.Send(new Contact
                        {
                            Name = contactCreateDto.Name,
                            Email = contactCreateDto.Email,
                            AreaCode = contactCreateDto.AreaCode,
                            Phone = contactCreateDto.Phone
                        });

                        insertResult.Success = true;
                        insertResult.Message = "Contato inserido na FILA com sucesso.";
                    }
                    catch (Exception e)
                    {
                        insertResult.Success = false;
                        insertResult.Message = $"Ocorreu um problema ao tentar inserir o registro.";
                        _logger.LogError(insertResult.Message, e);
                    }
                }
            }

            return insertResult;
        }

        private MassTransitDTO ChargeMassTransitObject()
        {
            return new MassTransitDTO()
            {
                QueueName = _configuration.GetSection("MassTransit:QueueName").Value ?? string.Empty,

                Server = _configuration.GetSection("MassTransit:Server").Value ?? string.Empty,

                User = _configuration.GetSection("MassTransit:User").Value ?? string.Empty,

                Password = _configuration.GetSection("MassTransit:Password").Value ?? string.Empty
            };
        }
    }
}