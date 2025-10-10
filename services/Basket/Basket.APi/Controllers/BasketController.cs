using AutoMapper;
using Basket.Application.Commands;
using Basket.Application.GrpcServices;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entites;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.API.Controllers
{

    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IPublishEndpoint _publish;
        private readonly IMapper _mapper;
        private readonly ILogger<BasketController> _logger;
        

        public BasketController(
            IMediator mediator, 
            IMapper mapper,
            ILogger<BasketController> logger,
            IPublishEndpoint publish

            )
        {
            _mediator = mediator;     
            _mapper = mapper;
            _logger = logger;
            _publish = publish;
        }

        [HttpGet]
        [Route("[action]/{userName}" , Name ="GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartItemResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
           var query = new GetBasketByUserNameQuery(userName);
            var basket = await _mediator.Send(query);
            return Ok(basket);

        }

        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartItemResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCartCommand command)
        {
         
          var basket = await _mediator.Send(command);
          return Ok(basket);
        }


        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> DeleteBasket(string userName)
        {
            var command = new DeleteBasketByUserNameCommand(userName);
            return Ok(await _mediator.Send(command));

        }

        [Route("[action]")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        
        public async Task<ActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var query = new GetBasketByUserNameQuery(basketCheckout.UserName);

            var basket = await _mediator.Send(query);

            if(basket == null)
            {
                return BadRequest();
            }

            var enevtMsg = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            enevtMsg.TotalPrice = basket.TotalPrice;
            await _publish.Publish(enevtMsg);
            // after send msg delete basket from basket db 
            var deletedBasket = new DeleteBasketByUserNameCommand(enevtMsg.UserName!);

            await _mediator.Send(deletedBasket);



            return Accepted();


        }
 

    }
}
