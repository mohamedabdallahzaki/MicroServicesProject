using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Commands;
using Ordering.Application.Exceptions;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Handlers.Commands
{
    public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, Unit>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;
        public UpdateOrderCommandHandler(
            IOrderRepository orderRepository,
            IMapper mapper,
            ILogger<CheckoutOrderCommandHandler> logger
            )
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<Unit> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToUpdate = await _orderRepository.GetByIdAsync(request.Id);

            if (orderToUpdate == null)
            {
                throw new OrderNotFoundException(nameof(Order), request.Id);
            }

            orderToUpdate.State = request.State;
            orderToUpdate.AddressLine = request.AddressLine;
            orderToUpdate.UserName = request.UserName;
            orderToUpdate.FirstName = request.FirstName;
            orderToUpdate.LastName = request.LastName;
            orderToUpdate.CardNumber = request.CardNumber;
            orderToUpdate.CardName = request.CardName;
            orderToUpdate.Country = request.Country;
            orderToUpdate.Cvv = request.Cvv;
            orderToUpdate.Expiration = request.Expiration;
            orderToUpdate.EmailAddress = request.EmailAddress;
            orderToUpdate.PaymentMethod = request.PaymentMethod;
            orderToUpdate.ZipCode = request.ZipCode;
            orderToUpdate.TotalPrice = request.TotalPrice;


            await _orderRepository.UpdateAsync(orderToUpdate);
            _logger.LogInformation($"Order with id {orderToUpdate.Id} was updated successfully");
            return Unit.Value;
        }
    }
}
