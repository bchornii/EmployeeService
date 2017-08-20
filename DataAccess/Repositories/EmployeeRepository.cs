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
    public interface IEmployeeRepository : IRepository<Employee, int>
    {
        Task<PagedResult<EmployeeSearchResult>> GetEmployees(EmployeeSearchRequest searchRequest);
    }

    public class EmployeeRepository : Repository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(INorthwindContext context) : base(context)
        {
        }

        public async Task<PagedResult<EmployeeSearchResult>> GetEmployees(EmployeeSearchRequest searchRequest)
        {
            var searchRequestKeyword = searchRequest.SearchKeyWord?.ToUpper();
            var query = Find(e => string.IsNullOrEmpty(searchRequest.SearchKeyWord) ||
                                  e.FirstName.ToUpper().Contains(searchRequestKeyword) ||
                                  e.LastName.ToUpper().Contains(searchRequestKeyword));

            var page = query.Select(e => new EmployeeSearchResult
            {
                EmployeeId = e.EmployeeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                Title = e.Title,
                ReffersTo = e.EmployeeManager == null 
                            ? string.Empty 
                            : e.EmployeeManager.FirstName + "  " + e.EmployeeManager.LastName,
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