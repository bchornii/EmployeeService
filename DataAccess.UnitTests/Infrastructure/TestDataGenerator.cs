using System.Collections.Generic;
using System.Linq;
using DataAccess.Entities;

namespace DataAccess.UnitTests.Infrastructure
{
    public class TestDataGenerator
    {
        public static IQueryable<Employee> GetEmployees()
        {
            var employee4 = new Employee
            {
                EmployeeId = 4,
                FirstName = "Margaret",
                LastName = "Peacock",
                Title = "Sales Representative"
            };
            var employee2 = new Employee
            {
                EmployeeId = 2,
                FirstName = "Andrew",
                LastName = "Fuller",
                Title = "Vice President, Sales"
            };

            employee4.EmployeeManager = employee2;

            return new List<Employee>
                {
                    employee2,
                    employee4
                }.AsQueryable();
        }

        public static IQueryable<Order> GetOrders()
        {
            return new List<Order>
                {
                    new Order
                    {
                        OrderId = 10250,
                        EmployeeId = 4
                    }
                }.AsQueryable();
        }

        public static IQueryable<OrderDetail> GetOrderDetails()
        {
            return new List<OrderDetail>
                {
                    new OrderDetail
                    {
                        OrderId = 10250,
                        ProductId = 41,
                        Quantity = 10
                    },
                    new OrderDetail
                    {
                        OrderId = 10250,
                        ProductId = 51,
                        Quantity = 35
                    },
                    new OrderDetail
                    {
                        OrderId = 10250,
                        ProductId = 65,
                        Quantity = 15
                    }
                }.AsQueryable();
        }
    }
}
