using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using DataAccess.Repositories;
using Domain.Models.Employee;
using Moq;
using NUnit.Framework;

namespace Domain.Services.UnitTests
{
    [TestFixture]
    public class EmployeeServiceTests
    {
        [Test]
        public async Task Repository_GetEmployees_Called_At_LeastOnce()
        {
            // Arrange
            var fixture = new EmployeeServiceFixture()
                .InitializeEmployeeRepository()
                .InitializeUnitOfWork()
                .InitializeEmployeeService();            

            // Act
            await fixture.EmployeeService.GetEmployees(It.IsAny<EmployeeSearchRequest>());

            // Assert
            Mock.Get(fixture.EmployeeRepositoryMock).Verify(r => r.GetEmployees(It.IsAny<EmployeeSearchRequest>()), Times.AtLeastOnce);
        }

        [Test]        
        public void GetEmployees_UnitOfWorkNotInjected_ThrowsException()
        {
            // Arrange
            var fixture = new EmployeeServiceFixture()
                .InitializeEmployeeRepository()
                .InitializeEmployeeService();

            // Act            

            // Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await fixture.EmployeeService.GetEmployees(It.IsAny<EmployeeSearchRequest>()));
        }

        [Test]        
        public void GetEmployees_EmployeeRepositoryNotInjected_ThrowsException()
        {
            // Arrange
            var fixture = new EmployeeServiceFixture()
                .InitializeUnitOfWork()
                .InitializeEmployeeService();

            // Act           

            // Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await fixture.EmployeeService.GetEmployees(It.IsAny<EmployeeSearchRequest>()));
        }

        [Test]       
        public void GetEmployees_NullEmployeeSearchRequest_ThrowsException()
        {
            // Arrange
            var fixture = new EmployeeServiceFixture()
                .InitializeEmployeeRepository()
                .InitializeUnitOfWork()
                .InitializeEmployeeService();

            Mock.Get(fixture.EmployeeRepositoryMock)
                .Setup(r => r.GetEmployees(null))
                .Throws<NullReferenceException>();

            // Act            

            // Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await fixture.EmployeeService.GetEmployees(It.IsAny<EmployeeSearchRequest>()));
        }

        [Test]
        public async Task GetEmployees_AnyNotNullSearchRequest_NoExeptions()
        {
            // Arrange
            var fixture = new EmployeeServiceFixture()
                .InitializeEmployeeRepository()
                .InitializeUnitOfWork()
                .InitializeEmployeeService();

            Mock.Get(fixture.EmployeeRepositoryMock)
                .Setup(r => r.GetEmployees(It.IsAny<EmployeeSearchRequest>()))
                .ReturnsAsync(fixture.GetPageResult(It.IsAny<IEnumerable<EmployeeSearchResult>>(), It.IsAny<int>()));                

            // Act
            var employees = await fixture.EmployeeService.GetEmployees(It.IsAny<EmployeeSearchRequest>());

            // Assert
            Assert.IsNotNull(employees);
        }

        [Test]
        public async Task GetEmployees_3EmployeeRecordsFound_Returns3Records()
        {
            // Arrange
            var fixture = new EmployeeServiceFixture()
                .InitializeEmployeeRepository()
                .InitializeUnitOfWork()
                .InitializeEmployeeService();

            var amountOfResults = 3;
            Mock.Get(fixture.EmployeeRepositoryMock)
                .Setup(r => r.GetEmployees(It.IsAny<EmployeeSearchRequest>()))
                .ReturnsAsync(fixture.GetSearchResults(amountOfResults));

            // Act
            var employees = await fixture.EmployeeService.GetEmployees(It.IsAny<EmployeeSearchRequest>());

            // Assert
            Assert.AreEqual(amountOfResults, employees.Items.Count());            
        }

        private class EmployeeServiceFixture
        {
            private IUnitOfWork UnitOfWorkMock { get; set; }
            public IEmployeeRepository EmployeeRepositoryMock { get; private set; }
            public IEmployeeService EmployeeService { get; private set; }

            public EmployeeServiceFixture InitializeUnitOfWork()
            {
                UnitOfWorkMock = new Mock<IUnitOfWork>().Object;
                Mock.Get(UnitOfWorkMock).Setup(uow => uow.EmployeeRepository).Returns(EmployeeRepositoryMock);
                return this;
            }

            public EmployeeServiceFixture InitializeEmployeeRepository()
            {
                EmployeeRepositoryMock = new Mock<IEmployeeRepository>().Object;                
                return this;
            }

            public EmployeeServiceFixture InitializeEmployeeService()
            {
                EmployeeService = new EmployeeService(UnitOfWorkMock);
                return this;
            }

            public PagedResult<EmployeeSearchResult> GetSearchResults(int amountOfResults) =>
                GetPageResult(Enumerable.Repeat(new EmployeeSearchResult(), amountOfResults), amountOfResults);                

            public PagedResult<EmployeeSearchResult> GetPageResult(IEnumerable<EmployeeSearchResult> items, int count) =>
                new PagedResult<EmployeeSearchResult>(items, count);
        }
    }
}
