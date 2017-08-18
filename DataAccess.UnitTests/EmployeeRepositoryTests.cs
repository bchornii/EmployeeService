using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataAccess.Entities;
using DataAccess.Infrastructure;
using DataAccess.Repositories;
using DataAccess.UnitTests.Infrastructure;
using Domain.Models.Employee;
using Moq;
using NUnit.Framework;

namespace DataAccess.UnitTests
{
    [TestFixture]
    public class EmployeeRepositoryTests
    {
        [Test]
        public async Task GetEmployees_DefaultEmployeeSearchRequest_ReturnsAllRecords()
        {
            // Arrange
            var fixture = new EmployeeRepositoryFixture()
                .InitializeEmployeeEntity()
                .InitializeOrderEntity()
                .InitializeOrderDetailsEntity()
                .InitializeContext();

            var employeeRepository = new EmployeeRepository(fixture.NorthwindContextMock);

            // Act
            var result = await employeeRepository.GetEmployees(new EmployeeSearchRequest());

            // Assert
            Assert.AreEqual(result.Items.Count(), fixture.TotalEmployees);            
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
