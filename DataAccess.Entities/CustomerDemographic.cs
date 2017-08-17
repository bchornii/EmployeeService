using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class CustomerDemographic
    {
        public string CustomerTypeId { get; set; }  
        public string CustomerDesc { get; set; }           
        public ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }  

        public CustomerDemographic()
        {
            CustomerCustomerDemos = new List<CustomerCustomerDemo>();
        }
    }
}