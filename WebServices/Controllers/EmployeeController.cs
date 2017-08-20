using System.Threading.Tasks;
using System.Web.Http;
using Domain.Models.Employee;
using Domain.Services;

namespace WebServices.Controllers
{
    [RoutePrefix("api/employee")]
    public class EmployeeController : ApiController
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost]        
        [Route("search")]
        public async Task<IHttpActionResult> EmployeeSearch(EmployeeSearchRequest request)
        {
            if (request == null)
            {
                return BadRequest();
            }
            var result = await _employeeService.GetEmployees(request);            
            return Ok(result);
        }
    }
}
