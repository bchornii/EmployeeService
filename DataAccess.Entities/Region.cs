using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Region
    {
        public int RegionId { get; set; }  
        public string RegionDescription { get; set; }  
         
        public ICollection<Territory> Territories { get; set; }  

        public Region()
        {
            Territories = new List<Territory>();
        }
    }
}