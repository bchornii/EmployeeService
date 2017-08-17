using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Common.Extensions;
using DataAccess.Entities;
using DataAccess.Infrastructure;
using DataAccess.Models;
using Domain.Models.Employee;

namespace DataAccess.Repositories
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        Task<PagedResult<EmployeeSearchResult>> GetEmployees(EmployeeSearchRequest searchRequest);
    }

    public class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(INorthwindContext context) : base(context)
        {
        }

        public async Task<PagedResult<EmployeeSearchResult>> GetEmployees(EmployeeSearchRequest searchRequest)
        {

            var query = Find(e => e.FirstName.Contains(searchRequest.SearchKeyWord) ||
                                  e.LastName.Contains(searchRequest.SearchKeyWord) ||
                                  string.IsNullOrEmpty(searchRequest.SearchKeyWord));

            var page = query.Select(e => new EmployeeSearchResult
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Title = e.Title,
                ReffersTo = e.EmployeeManager.FirstName + "  " + e.EmployeeManager.LastName,
                TotalSoldProducts = (from o in Context.Orders
                                     join od in Context.OrderDetails on o.OrderId equals od.OrderId
                                     where e.EmployeeId == o.EmployeeId
                                     group od by o.EmployeeId into g
                                     select g.Sum(t => t.Quantity)).FirstOrDefault()
                })
                .Order(searchRequest.SortingFieldName, searchRequest.SortDirection)
                .Skip((searchRequest.PageNumber - 1) * searchRequest.PageSize)
                .Take(searchRequest.PageSize);

            var count = await query.CountAsync();
            var pageResult = await page.ToListAsync();
            return new PagedResult<EmployeeSearchResult>(pageResult, count);
        }
    }
}