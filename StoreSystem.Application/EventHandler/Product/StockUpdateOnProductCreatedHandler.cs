using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using StoreSystem.Core.Events.Product;

namespace StoreSystem.Application.EventHandler.Product
{
    public class StockUpdateOnProductCreatedHandler : INotificationHandler<ProductCreatedEvent>
    {
        private ILogger<StockUpdateOnProductCreatedHandler> _logger;
        public StockUpdateOnProductCreatedHandler(ILogger<StockUpdateOnProductCreatedHandler> logger) => _logger = logger;  

            public Task Handle(ProductCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("RaiseProductCreated invoked for product ID: {Id}", notification.Id);
            return Task.CompletedTask;
        }
    }
}