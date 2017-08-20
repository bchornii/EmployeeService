using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using DataAccess.Models;
using Domain.Models.Employee;
using Domain.Services;
using Moq;
using NUnit.Framework;
using WebServices.Controllers;

namespace WebServices.UnitTests
{    
    [TestFixture]
    public class EmployeeControllerTests
    {
        [Test]
        public void GetEmployees_EmployeeServiceNotInjected_ThrowsException()
        {
            // Arrange
            var fixture = new EmployeeControllerFixture();

            // Act
            
            // Arrange
            Assert.ThrowsAsync<NullReferenceException>(
                async () => await fixture.EmployeeServiceMock.GetEmployees(It.IsAny<EmployeeSearchRequest>()));
        }


        [Test]
        public async Task GetEmployees_DefaultEmployeeSearchRequest_ReturnsOk()
        {
            // Arrange
            var fixture = new EmployeeControllerFixture()
                .InitializeEmployeeService();            

            Mock.Get(fixture.EmployeeServiceMock)
                .Setup(es => es.GetEmployees(It.IsAny<EmployeeSearchRequest>()))
                .ReturnsAsync(It.IsAny<PagedResult<EmployeeSearchResult>>());

            var controller = new EmployeeController(fixture.EmployeeServiceMock);

            // Act
            IHttpActionResult response = await controller.EmployeeSearch(new EmployeeSearchRequest());

            // Assert
            Assert.IsInstanceOf<OkNegotiatedContentResult<PagedResult<EmployeeSearchResult>>>(response);
        }

        [Test]
        public async Task GetEmployees_RequestObjIsNull_ReturndsBadRequest()
        {
            // Arrange
            var fixture = new EmployeeControllerFixture()
                .InitializeEmployeeService();

            var controller = new EmployeeController(fixture.EmployeeServiceMock);

            // Act
            IHttpActionResult response = await controller.EmployeeSearch(null);

            // Assert
            Assert.IsInstanceOf<BadRequestResult>(response);
        }

        [Test]
        public async Task GetEmployee_EmployeeService_GetEmployee_Gets_Called_AtLeast_Once()
        {
            // Arrange
            var fixture = new EmployeeControllerFixture()
                .InitializeEmployeeService();

            var employeeSearchRequest = new EmployeeSearchRequest();
            var controller = new EmployeeController(fixture.EmployeeServiceMock);

            // Act
            await controller.EmployeeSearch(employeeSearchRequest);

            // Assert
            Mock.Get(fixture.EmployeeServiceMock).Verify(es => es.GetEmployees(employeeSearchRequest), Times.AtLeastOnce);
        }

        [Test]
        public async Task GetEmployees_EmployeeRecordsFound_ReturnsTheSameRecords()
        {
            // Arrange
            var fixture = new EmployeeControllerFixture()
                .InitializeEmployeeService();            

            var amountOfResults = 3;
            var expectedResults = fixture.GetSearchResults(amountOfResults);
            Mock.Get(fixture.EmployeeServiceMock)
                .Setup(es => es.GetEmployees(It.IsAny<EmployeeSearchRequest>()))
                .ReturnsAsync(expectedResults);            

            var controller = new EmployeeController(fixture.EmployeeServiceMock);

            // Act
            var response =
                await controller.EmployeeSearch(new EmployeeSearchRequest()) as
                    OkNegotiatedContentResult<PagedResult<EmployeeSearchResult>>;

            // Assert
            CollectionAssert.AreEqual(response.Content.Items, expectedResults.Items);
        }

        private class EmployeeControllerFixture
        {
            public IEmployeeService EmployeeServiceMock { get; private set; }            

            public EmployeeControllerFixture InitializeEmployeeService()
            {
                EmployeeServiceMock = new Mock<IEmployeeService>().Object;
                return this;
            }            

            public PagedResult<EmployeeSearchResult> GetSearchResults(int amountOfResults) =>
                GetPageResult(Enumerable.Repeat(new EmployeeSearchResult (), amountOfResults), amountOfResults);

            private PagedResult<EmployeeSearchResult> GetPageResult(IEnumerable<EmployeeSearchResult> items, int count) =>
                new PagedResult<EmployeeSearchResult>(items, count);                           
        }
    }
}
