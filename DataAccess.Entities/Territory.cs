using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Territory
    {
        public string TerritoryId { get; set; }  
        public string TerritoryDescription { get; set; }  
        public int RegionId { get; set; }  
         
        public ICollection<EmployeeTerritory> EmployeeTerritories { get; set; }           
        public Region Region { get; set; }  

        public Territory()
        {
            EmployeeTerritories = new List<EmployeeTerritory>();
        }
    }
}