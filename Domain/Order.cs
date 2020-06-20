namespace Domain
{
    using System;

    public class Order
    {
        public Guid Id { get; set; }
        public string SenderDocument { get; set; }
        public string ReceiverDocument { get; set; }
        public string From { get; set; }
        public string Destination { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public State State { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }

    public enum State
    {
        Pending,
        Collected,
        Sending,
        Delivery
    }
}
