using System.Threading.Tasks;
using DataAccess;
using DataAccess.Models;
using Domain.Models.Employee;

namespace Domain.Services
{
    public interface IEmployeeService
    {
        Task<PagedResult<EmployeeSearchResult>> GetEmployees(EmployeeSearchRequest searchRequest);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedResult<EmployeeSearchResult>> GetEmployees(EmployeeSearchRequest searchRequest)
        {
            return await _unitOfWork.EmployeeRepository.GetEmployees(searchRequest);
        }
    }
}
