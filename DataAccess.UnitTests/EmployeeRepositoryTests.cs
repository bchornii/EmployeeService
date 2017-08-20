using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Infrastructure;
using DataAccess.Repositories;
using DataAccess.UnitTests.Infrastructure;
using Domain.Models.Employee;
using Domain.Models.Enums;
using Moq;
using NUnit.Framework;

namespace DataAccess.UnitTests
{
    [TestFixture]
    public class EmployeeRepositoryTests
    {
        private EmployeeRepositoryFixture _fixture;

        [SetUp]
        public void Init()
        {
            _fixture = new EmployeeRepositoryFixture()
                .InitializeEmployeeEntity()
                .InitializeOrderEntity()
                .InitializeOrderDetailsEntity()
                .InitializeContext();
        }

        [Test]
        public void GetEmployees_NullRequestObj_ThrowsException()
        {
            // Arrange
            
            // Act

            // Assert
            Assert.ThrowsAsync<NullReferenceException>(async () => await _fixture.EmployeeRepository.GetEmployees(null));
        }

        [Test]
        public async Task GetEmployees_DefaultEmployeeSearchRequest_ReturnsAllRecords()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository(_fixture.NorthwindContextMock);

            // Act
            var result = await employeeRepository.GetEmployees(new EmployeeSearchRequest());

            // Assert
            Assert.AreEqual(result.Items.Count(), _fixture.TotalEmployees);            
        }

        [Test]
        public async Task GetEmployees_SearchBy_an_Predicate_Returns6Records()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository(_fixture.NorthwindContextMock);
            var employeeSearchRequest = new EmployeeSearchRequest { SearchKeyWord  = "An" };

            // Act
            var result = await employeeRepository.GetEmployees(employeeSearchRequest);            

            // Assert
            Assert.AreEqual(result.Items.Count(), 6);
        }

        [Test]
        public async Task GetEmployees_PageSize3_Returns3()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository(_fixture.NorthwindContextMock);
            var employeeSearchRequest = new EmployeeSearchRequest { SearchKeyWord = "An", PageSize = 3};

            // Act
            var result = await employeeRepository.GetEmployees(employeeSearchRequest);

            // Assert
            Assert.AreEqual(result.Items.Count(), 3);
        }

        [Test]
        public async Task GetEmployees_PageSize3_OrderByFirstNameAscending_Returns_Andrew_Anne_Janet()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository(_fixture.NorthwindContextMock);
            var employeeSearchRequest = new EmployeeSearchRequest
            {
                SearchKeyWord = "An",
                PageSize = 3,
                SortingFieldName = "FirstName",
                SortDirection = SortDirection.Acending                
            };
            var expectedEmloyeesNames = new[] {"Andrew", "Anne", "Janet"};

            // Act
            var result = await employeeRepository.GetEmployees(employeeSearchRequest);

            // Assert
            CollectionAssert.AreEqual(result.Items.Select(e => e.FirstName), expectedEmloyeesNames);            
        }

        [Test]
        public async Task GetEmployees_PageSize3_OrderByFirstNameDescending_Returns_Steven_Nancy_Laura()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository(_fixture.NorthwindContextMock);
            var employeeSearchRequest = new EmployeeSearchRequest
            {
                SearchKeyWord = "An",
                PageSize = 3,
                SortingFieldName = "FirstName",
                SortDirection = SortDirection.Descending
            };
            var expectedEmloyeesNames = new[] { "Steven", "Nancy", "Laura" };

            // Act
            var result = await employeeRepository.GetEmployees(employeeSearchRequest);

            // Assert
            CollectionAssert.AreEqual(result.Items.Select(e => e.FirstName), expectedEmloyeesNames);
        }

        [Test]
        public async Task GetEmployees_PageSize3_Page2_Returns_Laura_Nancy_Steven()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository(_fixture.NorthwindContextMock);
            var employeeSearchRequest = new EmployeeSearchRequest
            {
                SearchKeyWord = "An",
                PageNumber = 2,
                PageSize = 3
            };
            var expectedEmloyeesNames = new[] { "Laura", "Nancy", "Steven" };

            // Act
            var result = await employeeRepository.GetEmployees(employeeSearchRequest);

            // Assert
            CollectionAssert.AreEqual(result.Items.Select(e => e.FirstName), expectedEmloyeesNames);
        }

        [Test]
        public async Task GetEmployees_PageSize3_OrderByLastNameAscending_Returns_Buchanan_Callahan_Davolio()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository(_fixture.NorthwindContextMock);
            var employeeSearchRequest = new EmployeeSearchRequest
            {
                SearchKeyWord = "An",
                PageSize = 3,
                SortingFieldName = "LastName",
                SortDirection = SortDirection.Acending
            };
            var expectedEmloyeesNames = new[] { "Buchanan", "Callahan", "Davolio" };

            // Act
            var result = await employeeRepository.GetEmployees(employeeSearchRequest);

            // Assert
            CollectionAssert.AreEqual(result.Items.Select(e => e.LastName), expectedEmloyeesNames);
        }

        [Test]
        public async Task GetEmployees_DefaultEmployeeSearchRequest_VerifyTotalSoldProductsPerEmployee()
        {
            // Arrange
            var employeeRepository = new EmployeeRepository(_fixture.NorthwindContextMock);
            var employeeSearchRequest = new EmployeeSearchRequest();
            var expectedResult = TestDataGenerator.GetTotalSoldProductsCalculated();

            // Act
            var result = await employeeRepository.GetEmployees(employeeSearchRequest);
            var totalSoldProductsResult = from r in result.Items
                join er in expectedResult on 
                    new {r.EmployeeId, r.TotalSoldProducts} equals 
                    new {er.EmployeeId, er.TotalSoldProducts}
                select 1;

            // Assert
            Assert.AreEqual(totalSoldProductsResult.Count(), expectedResult.Count);
        }

        public class EmployeeRepositoryFixture
        {
            public INorthwindContext NorthwindContextMock { get; set; }
            public DbSet<Employee> EmployeesMock { get; set; }
            public DbSet<Order> OrdersMock { get; set; }
            public DbSet<OrderDetail> OrderDetailsMock { get; set; }
            public IEmployeeRepository EmployeeRepository { get; set; }

            public int TotalEmployees { get; } =
                EntityMock.GetEntityMock(TestDataGenerator.GetEmployees()).Object.Count();

            public EmployeeRepositoryFixture InitializeContext()
            {
                NorthwindContextMock = new Mock<INorthwindContext>().Object;

                Mock.Get(NorthwindContextMock).Setup(c => c.Employees).Returns(EmployeesMock);
                Mock.Get(NorthwindContextMock).Setup(c => c.Orders).Returns(OrdersMock);
                Mock.Get(NorthwindContextMock).Setup(c => c.OrderDetails).Returns(OrderDetailsMock);

                Mock.Get(NorthwindContextMock).Setup(c => c.Set<Employee>()).Returns(EmployeesMock);
                Mock.Get(NorthwindContextMock).Setup(c => c.Set<Order>()).Returns(OrdersMock);
                Mock.Get(NorthwindContextMock).Setup(c => c.Set<OrderDetail>()).Returns(OrderDetailsMock);

                return this;
            }

            public EmployeeRepositoryFixture InitializeEmployeeEntity()
            {
                EmployeesMock = EntityMock.GetEntityMock(TestDataGenerator.GetEmployees()).Object;
                return this;
            }

            public EmployeeRepositoryFixture InitializeOrderEntity()
            {
                OrdersMock = EntityMock.GetEntityMock(TestDataGenerator.GetOrders()).Object;
                return this;
            }

            public EmployeeRepositoryFixture InitializeOrderDetailsEntity()
            {
                OrderDetailsMock = EntityMock.GetEntityMock(TestDataGenerator.GetOrderDetails()).Object;
                return this;
            }
        }            
    }
}
