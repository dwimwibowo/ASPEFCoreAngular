﻿using System.ComponentModel.DataAnnotations.Schema;

namespace DutchTreat.Data.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "NUMERIC(18,4)")]
        public decimal UnitPrice { get; set; }
    }
}