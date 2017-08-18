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
        public async Task GetEmployees_AnyNotNullRequestObj_ReturnsOk()
        {
            // Arrange
            var fixture = new EmployeeControllerFixture()
                .InitializeEmployeeService();

            var employeeSearchRequest = new EmployeeSearchRequest();

            Mock.Get(fixture.EmployeeServiceMock)
                .Setup(es => es.GetEmployees(employeeSearchRequest))
                .ReturnsAsync(It.IsAny<PagedResult<EmployeeSearchResult>>());

            var controller = new EmployeeController(fixture.EmployeeServiceMock);

            // Act
            IHttpActionResult response = await controller.EmployeeSearch(employeeSearchRequest);

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
            IHttpActionResult response = await controller.EmployeeSearch(employeeSearchRequest);

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
            var employeeSearchRequest = new EmployeeSearchRequest
            {
                SearchKeyWord = EmployeeControllerFixture.EmployeeFirstName

            };
            var expectedResults = fixture.GetSearchResults(amountOfResults, employeeSearchRequest.SearchKeyWord);
            Mock.Get(fixture.EmployeeServiceMock)
                .Setup(es => es.GetEmployees(employeeSearchRequest))
                .ReturnsAsync(expectedResults);            

            var controller = new EmployeeController(fixture.EmployeeServiceMock);

            // Act
            var response =
                await controller.EmployeeSearch(employeeSearchRequest) as
                    OkNegotiatedContentResult<PagedResult<EmployeeSearchResult>>;

            // Assert
            CollectionAssert.AreEqual(response.Content.Items, expectedResults.Items);
        }

        private class EmployeeControllerFixture
        {
            public const string EmployeeFirstName = "Margaret";
            public const string EmployeeLastName = "Peacock";

            public IEmployeeService EmployeeServiceMock { get; private set; }            

            public EmployeeControllerFixture InitializeEmployeeService()
            {
                EmployeeServiceMock = new Mock<IEmployeeService>().Object;
                return this;
            }            

            public PagedResult<EmployeeSearchResult> GetSearchResults(int amountOfResults, string firstName) =>
                GetPageResult(Enumerable.Repeat(new EmployeeSearchResult { FirstName = firstName }, amountOfResults), amountOfResults);

            public PagedResult<EmployeeSearchResult> GetPageResult(IEnumerable<EmployeeSearchResult> items, int count) =>
                new PagedResult<EmployeeSearchResult>(items, count);                           
        }
    }
}
