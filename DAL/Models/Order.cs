using DAL.Enums;
using System;

namespace DAL.Models
{
    public class Order
    {
        public string Id { get; set; }

        public OrderStatus Status { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime UpdatedDate { get; set; }

        public string ProductId { get; set; }
    }
}
