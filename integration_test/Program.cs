using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Infrastructure;
using Domain.Models.Employee;
using Domain.Models.Enums;

namespace integration_test
{
    internal class Program
    {
        static INorthwindContext context = new NorthwindDbContextFactory().CreateContext();
        private static void Main()
        {            

            var id = 9;

            //var employeeProductSoldAmount = GetOrdersSum(id);
            //var emp = context.Employees.Where(e => e.EmployeeId == id)
            //    .Select(e => new
            //    {
            //        FirstName = e.FirstName,
            //        LastName = e.LastName,
            //        Title = e.Title,
            //        TotalQty = employeeProductSoldAmount.Sum(od => od.Quantity)       
            //    }).FirstOrDefault();            

            //Console.WriteLine(emp);
            using (var uow = new UnitOfWork(new NorthwindDbContextFactory()))
            {
                Task.Run(() =>
                {
                    var r = uow.EmployeeRepository.GetEmployees(new EmployeeSearchRequest
                    {
                        SearchKeyWord = string.Empty, SortDirection = SortDirection.Descending
                        
                    }).Result;

                }).Wait();
            }

            Console.Read();
        }


        private static void GetOrdersSum(IEnumerable<int> employeeIds)
        {
            var r = from o in context.Orders
                join e in employeeIds on o.EmployeeId equals e
                join od in context.OrderDetails on o.OrderId equals od.OrderId      
                group od by o.EmployeeId into g          
                select new { EmployeeId = g.Key, TotalSold = g.Sum(t => t.Quantity)};

            var k = r.ToList();
        }



        // private static string GetFullName(string firstName, string lastName) =>

    }
}
