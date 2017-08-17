using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Shipper
    {
        public int ShipperId { get; set; }  
        public string CompanyName { get; set; }  
        public string Phone { get; set; }  
         
        public ICollection<Order> Orders { get; set; }  

        public Shipper()
        {
            Orders = new List<Order>();
        }
    }
}