using Domain.Models.Enums;

namespace Domain.Models.Employee
{
    public class EmployeeSearchRequest
    {
        public string SearchKeyWord { get; set; }
        public string SortingFieldName { get; set; } = Constants.SearchEmployeeConstants.DefaultEmployeeTableSortingFieldName;
        public SortDirection SortDirection { get; set; } = SortDirection.Acending;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}