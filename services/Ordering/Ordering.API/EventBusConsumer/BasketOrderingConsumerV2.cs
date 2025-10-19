﻿using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;

namespace Ordering.API.EventBusConsumer
{
    public class BasketOrderingConsumerV2 : IConsumer<BasketCheckoutEventV2>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketOrderingConsumer> _logger;

        public BasketOrderingConsumerV2(
            IMediator mediator,
            IMapper mapper,
            ILogger<BasketOrderingConsumer> logger
            )
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task Consume(ConsumeContext<BasketCheckoutEventV2> context)
        {
            using var scope = _logger.BeginScope("consuming basket checkout event for {correlationid}", context.Message.CorrelationId);
            var cmd = _mapper.Map<CheckoutOrderCommand>(context.Message);
            var result = await _mediator.Send(cmd);
            _logger.LogInformation("Basket checkout v2 event completed !!");
        }
    }
}
