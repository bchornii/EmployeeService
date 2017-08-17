namespace DataAccess.Entities
{
    public class EmployeeTerritory
    {
        public int EmployeeId { get; set; }  
        public string TerritoryId { get; set; }  
         
        public Employee Employee { get; set; }  
        public Territory Territories { get; set; }  
    }
}