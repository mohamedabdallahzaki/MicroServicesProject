using Basket.Applicaation.Commonds;
using Basket.Applicaation.Queries;
using Basket.Applicaation.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Basket.APi.Controllers
{
 
    public class BasketController : BaseApiController
    {
        private readonly IMediator _mediator;

        public BasketController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("[action]/{UserName}",Name ="GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartRespones) , (int) HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartRespones>> GetBasket(string UserName)
        {

            var query = new GetBasketByUserNameQuery(UserName);
            var basket = await _mediator.Send(query);

            return Ok(basket);


        }

        [HttpPost]
        [Route("UpdataBasket")]
        [ProducesResponseType(typeof(ShoppingCartRespones), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartRespones>> UpdateBasket([FromBody] CreateBasketCommond basketCommond)
        {
            var basket = await _mediator.Send<ShoppingCartRespones>(basketCommond);

            return Ok(basket);
        }

        [HttpDelete]
        [Route("{UserName}", Name ="Deletebasket")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<ActionResult<Unit>> Deletebasket (string UserName)
        {
            var commond = new DeleteBasketCommond(UserName);

            var res = await _mediator.Send(commond);

            return Ok(res);
            
        }
    }
}
