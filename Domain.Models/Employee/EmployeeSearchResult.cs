namespace Domain.Models.Employee
{
    public class EmployeeSearchResult
    {
        public int EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public string ReffersTo { get; set; }
        public long TotalSoldProducts { get; set; }
    }
}
