﻿using backend.Contexts;
using backend.Models;
using backend.Producer.DTOs;
using backend.Producer.Repository;
using backend.Producer.UseCases;
using backend.Utils.Errors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Factories.Producer;

namespace Tests.UnitTests.UseCases.Producer
{

    public class CreateProducerUseCaseTest {

        private ProducerDTOFactory producerFactory = new ProducerDTOFactory();
        private Mock<IProducerRepository> producerRepository;

        public CreateProducerUseCaseTest() {
            producerRepository = new Mock<IProducerRepository>();
        }


        [Fact]
        [Trait("OP", "Create")]
        public async Task Save_GivenProducer_ReturnsCreatedProducer() {

            //Arrange
            producerRepository.Setup(x => x.Save(It.IsAny<backend.Models.Producer>())).ReturnsAsync(new backend.Models.Producer());
            producerRepository.Setup(x => x.FindByEmail(It.IsAny<string>())).Returns(Task.FromResult<backend.Models.Producer?>(null));

            CreateProducerUseCase usecase = new CreateProducerUseCase(producerRepository.Object);

            var producer = producerFactory.Build();

            //Act
            var createdProducer = await usecase.Execute(producer);

            //Assert
            Assert.NotNull(createdProducer);
        }

        [Fact]
        [Trait("OP", "Create")]
        public async Task Save_GivenAlreadyExistentProducer_ThrowsError() {
            //Arrange
            producerRepository.Setup(x => x.Save(It.IsAny<backend.Models.Producer>())).ReturnsAsync(new backend.Models.Producer());
            producerRepository.Setup(x => x.FindByEmail(It.IsAny<string>())).ReturnsAsync(new backend.Models.Producer());

            CreateProducerUseCase usecase = new CreateProducerUseCase(producerRepository.Object);

            var producer = producerFactory.Build();

            //Act
            async Task Act(CreateProducerDTO producer) {
                var createdProducer = await usecase.Execute(producer);
            }

            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await Act(producer));
            Assert.Equal("Usuário já cadastrado", exception.Message);
        }
    }
}
