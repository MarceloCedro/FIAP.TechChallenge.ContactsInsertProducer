using FIAP.TechChallenge.ContactsInsertProducer.Api.Logging;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.EntityDTOs;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Applications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FIAP.TechChallenge.ContactsInsertProducer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController(IContactApplication contactService, ILogger<ContactsController> logger) : ControllerBase
    {
        private readonly IContactApplication _contactService = contactService;
        private readonly ILogger<ContactsController> _logger = logger;

        /// <summary>
        /// Metodo para adicionar um novo contato de forma assíncrona.
        /// </summary>
        /// <param name="contact">Json com os dados do contato: Nome, DDD, Telefone e Email</param>
        /// <returns>Adiciona um novo contato no banco de dados</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddContact(ContactCreateDto contact)
        {
            _logger.LogInformation("Adicionando novo contato");
            var insertedObject = await _contactService.AddContactAsync(contact);

            if (insertedObject.Success)
                return Ok(insertedObject.Message);
            else
                return BadRequest(insertedObject.Message);
        }
    }
}
