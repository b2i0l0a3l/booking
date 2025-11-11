using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace StoreSystem.Core.Events.Product
{
    public class ProductCreatedEvent : INotification
    {
        public int Id { get; set; }
        public DateTime UpdateAt { get; set; }
        public int StockQuantity { get; set; }
        
        public ProductCreatedEvent(int id , DateTime updateAt, int stockQuantity)
        {
            Id = id;
            UpdateAt = updateAt;
            StockQuantity = stockQuantity;
        }
    }
}