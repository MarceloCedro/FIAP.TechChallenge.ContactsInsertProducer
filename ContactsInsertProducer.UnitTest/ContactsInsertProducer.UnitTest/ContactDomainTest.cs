﻿using FIAP.TechChallenge.ContactsInsertProducer.Domain.Entities;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Interfaces.Repositories;
using FIAP.TechChallenge.ContactsInsertProducer.Domain.Services;
using Microsoft.Extensions.Logging;
using Moq;

namespace FIAP.TechChallenge.ContactsInsertProducer.UnitTest
{
    public class ContactDomainTest
    {
        private readonly Mock<IContactRepository> _contactRepository;

        private readonly Mock<ILogger<ContactService>> _loggerMock;

        private readonly ContactService _contactService;

        public ContactDomainTest()
        {
            _contactRepository = new Mock<IContactRepository> ();
            _loggerMock = new Mock<ILogger<ContactService>>();

            _contactService = new ContactService(_contactRepository.Object, _loggerMock.Object);
        }

        [Fact]
        public async Task InsertContactExceptionAsync()
        {
            _contactRepository.Setup(u => u.AddAsync(It.IsAny<Contact>())).ThrowsAsync(new Exception());


            _contactService.InsertAsync(null);
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((o, t) => string.Equals("Some error occour when trying to insert new Contact.", o.ToString(), StringComparison.InvariantCultureIgnoreCase)),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }

        [Fact]
        public async Task InsertContactSuccessAsync()
        {
            _contactRepository.Setup(u => u.AddAsync(It.IsAny<Contact>()));

            _contactService.InsertAsync(null);

            _contactRepository.Verify(u => u.AddAsync(It.IsAny<Contact>()), Times.Once());
        }
    }
}
