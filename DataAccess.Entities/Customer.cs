using System.Collections.Generic;

namespace DataAccess.Entities
{
    public class Customer
    {
        public string CustomerId { get; set; }  
        public string CompanyName { get; set; }  
        public string ContactName { get; set; }  
        public string ContactTitle { get; set; }  
        public string Address { get; set; }  
        public string City { get; set; }  
        public string Region { get; set; }  
        public string PostalCode { get; set; }  
        public string Country { get; set; }  
        public string Phone { get; set; }  
        public string Fax { get; set; }  
                 
        public ICollection<CustomerCustomerDemo> CustomerCustomerDemos { get; set; }  
        public ICollection<Order> Orders { get; set; }  

        public Customer()
        {
            CustomerCustomerDemos = new List<CustomerCustomerDemo>();
            Orders = new List<Order>();
        }
    }
}