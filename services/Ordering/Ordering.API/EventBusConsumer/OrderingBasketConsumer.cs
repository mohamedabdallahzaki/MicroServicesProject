using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Ordering.Application.Commands;


namespace Ordering.API.EventBusConsumer
{
    public class OrderingBasketConsumer : IConsumer<BasketCheckoutEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderingBasketConsumer> _logger;

        public OrderingBasketConsumer(IMediator mediator,
            IMapper mapper,
            ILogger<OrderingBasketConsumer> logger)
        {
            _mediator = mediator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            _logger.LogInformation($"basket consumer checkou event correaltionId{context.Message.CorrelationId}");

            var cmd = _mapper.Map<CheckoutOrderCommand>(context.Message);

            var res = await _mediator.Send(cmd);

            _logger.LogInformation($"basket checkout event completed!!"); 
        }
    }
}
