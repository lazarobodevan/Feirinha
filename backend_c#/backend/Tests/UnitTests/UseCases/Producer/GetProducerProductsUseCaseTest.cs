﻿using backend.Contexts;
using backend.DTOs.Producer;
using backend.DTOs.Product;
using backend.Enums;
using backend.Models;
using backend.Repositories;
using backend.UseCases.Producer;
using backend.UseCases.Product;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Factories;

namespace Tests.UnitTests.UseCases.Producer {
    public class GetProducerProductsUseCaseTest {

        private readonly Mock<IProducerRepository> _producerRepository;

        public GetProducerProductsUseCaseTest() {
            _producerRepository = new Mock<IProducerRepository>();
        }

        [Fact]
        [Trait("OP", "GetProducerProducts")]
        public void GetProducerProducts_GivenProducerId_ReturnsProducerProducts() {
            //Arrange
            _producerRepository.Setup(x => x.GetProducts(It.IsAny<Guid>())).Returns(new List<backend.Models.Product>());
            GetProducerProductsUseCase getProducerProductsUseCase = new GetProducerProductsUseCase(_producerRepository.Object);

            //Act
            var foundProducts = getProducerProductsUseCase.Execute(Guid.NewGuid());

            //Assert
            Assert.NotNull(foundProducts);
        }

        [Fact]
        [Trait("OP", "GetProducerProducts")]
        public async Task GetProducerProducts_GivenNotExistantProducerId_ThrowsError() {

            //Arrange
            _producerRepository.Setup(x => x.FindById(It.IsAny<Guid>())).Returns(Task.FromResult<backend.Models.Producer?>(null));
            GetProducerProductsUseCase getProducerProductsUseCase = new GetProducerProductsUseCase(_producerRepository.Object);

            //Act
            async Task Act() {
                var foundProducts = await getProducerProductsUseCase.Execute(Guid.NewGuid());
            }

            //Assert
            var exception = await Assert.ThrowsAsync<Exception>(async () => await Act());
            Assert.Equal("Produtor não existe", exception.Message);
        }

    }
}
