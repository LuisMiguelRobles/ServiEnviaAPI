namespace Domain
{
    using System.Collections.Generic;
    using System;

    public class Customer
    {
        public Guid Id { get; set; }
        public string Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Email { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
