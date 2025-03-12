using FIAP.TechChallenge.ContactsInsertProducer.Application.Applications;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.DTOs.EntityDTOs;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Entities;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Applications;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Integrations;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Services;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System.Numerics;
using System.Xml.Linq;

namespace ContactsInsertProducer.UnitTest
{
    public class ContactApplicaitonTest
    {
        private readonly Mock<IContactService> _contactServiceMock;
        private readonly Mock<ILogger<ContactApplication>> _loggerMock;
        private readonly Mock<IContactConsultManager> _contactConsultManagerMock;
        private readonly Mock<IConfiguration> _configurationMock;
        private readonly Mock<IBus> _busMock;
        private readonly ContactApplication contactApplication;

        public ContactApplicaitonTest()
        {
            _contactServiceMock = new Mock<IContactService>();
            _loggerMock = new Mock<ILogger<ContactApplication>>();
            _contactConsultManagerMock = new Mock<IContactConsultManager>();
            _configurationMock = new Mock<IConfiguration>();
            _busMock = new Mock<IBus>();
            contactApplication = new ContactApplication(_contactServiceMock.Object, 
                                                        _contactConsultManagerMock.Object,
                                                        _loggerMock.Object, 
                                                        _configurationMock.Object, 
                                                        _busMock.Object);
        }

        private async Task SetupTokenAsync(bool validReturn)
        {
            if (validReturn)
                _contactConsultManagerMock.Setup(u => u.GetToken()).ReturnsAsync("TOKEN-TEST");
            else
                _contactConsultManagerMock.Setup(u => u.GetToken());
        }

        private async Task SetupGetContactByEmailAsync(bool validReturn)
        {
            if (!validReturn)
            {
                var contato = new Contact()
                {
                    Name = "Marcelo Cedro",
                    Email = "marcel1234ocedro@gmail.com",
                    AreaCode = "11",
                    Phone = "982840611"
                };
                _contactConsultManagerMock.Setup(u => u.GetContactByEmail(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(contato);
            }
            else
                _contactConsultManagerMock.Setup(u => u.GetContactByEmail(It.IsAny<string>(), It.IsAny<string>()));
        }

        [Fact]
        public async Task ValidateFakeTokenAsync()
        {
            await SetupTokenAsync(false);
            await SetupGetContactByEmailAsync(false);
            var contactDto = new ContactCreateDto
            {
                Name = "Marcelo Cedro",
                Email = "marcel1234ocedro@gmail.com",
                AreaCode = "11",
                Phone = "982840611"               
            };
            var retorno = await contactApplication.AddContactAsync(contactDto);

            await VerifyTokenAsync(Times.Once());
            await VerifyGetContactByEmailAsync(Times.Once());
            Assert.False(retorno.Success);
            Assert.Equal("Email de contato ja existente na base.", retorno.Message);
        }

        private async Task VerifyTokenAsync(Times times)
        {
            _contactConsultManagerMock.Verify(u => u.GetToken(), times);
        }

        private async Task VerifyGetContactByEmailAsync(Times times)
        {
            _contactConsultManagerMock.Verify(u => u.GetContactByEmail(It.IsAny<string>(), It.IsAny<string>()), times);
        }
    }
}