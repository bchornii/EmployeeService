namespace DataAccess.Entities
{
    public class CustomerCustomerDemo
    {
        public string CustomerId { get; set; }  
        public string CustomerTypeId { get; set; }  
         
        public Customer Customers { get; set; }  
        public CustomerDemographic CustomerDemographic { get; set; }  
    }
}