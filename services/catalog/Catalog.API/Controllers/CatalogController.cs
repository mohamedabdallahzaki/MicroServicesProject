using Catalog.Application.Commond;
using Catalog.Application.Handler.Query;
using Catalog.Application.Query;
using Catalog.Application.Responses;
using Catalog.Core.Specs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.API.Controllers
{
 
    public class CatalogController : BaseApiController
    {

        private readonly IMediator _mediator;

        public CatalogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("{id}",Name ="GetProductById")]
        [ProducesResponseType(typeof(ProductResponseDTO) , (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<ProductResponseDTO>> GetProductById (string id)
        {
            var query = new GetProductByIdQuery(id);
            var res = await _mediator.Send(query);

            return Ok(res);

        }

        [HttpGet]
        [Route("{productName}", Name = "GetProductByName")]
        [ProducesResponseType(typeof(IList<ProductResponseDTO>), (int)HttpStatusCode.OK)]
        
        public async Task<ActionResult<IList<ProductResponseDTO>>> GetProductByName(string Name)
        {
            var query = new GetAllProductsByNameQuery(Name);
            var res = await _mediator.Send(query);

            return Ok(res);

        }

        [HttpGet]
        [Route("GetAllProduct")]
        [ProducesResponseType(typeof(Pagantion<ProductResponseDTO>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<Pagantion<ProductResponseDTO>>> GetAllProduct([FromQuery] CatalogSpecParams spec)
        {
            var query = new GetAllProductQuery(spec);

            var res = await _mediator.Send(query);

            return Ok(res);
        }

        [HttpPost]
        [Route("CreateProduct")]
        [ProducesResponseType(typeof(ProductResponseDTO), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<ProductResponseDTO>> CreateProduct([FromBody] CreateProductCommond productCommond)
        {
            var res = await _mediator.Send<ProductResponseDTO>(productCommond);

            return Ok(res);
        }

        [HttpPut]
        [Route("UpdateProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<bool>> UpdateProduct([FromBody] UpdateProductCommond productCommond)
        {
            var res = await _mediator.Send<bool>(productCommond);

            return Ok(res);
        }

        [HttpDelete]
        [Route("{id}" , Name ="DeleteProduct")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<bool>> DeleteProduct(string id)
        {
            var commond = new DeleteProductCommond(id);
            var res = await _mediator.Send<bool>(commond);

            return Ok(res);
        }

        [HttpGet]
        [Route("GetAllTyps")]
        [ProducesResponseType(typeof(IList<TypeResponseDTO>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IList<TypeResponseDTO>>> GetAllTypes()
        {
            var query = new GetAllTypesQuery();
            var res = await _mediator.Send(query);

            return Ok(res);

        }
        [HttpGet]
        [Route("GetAllBrands")]
        [ProducesResponseType(typeof(IList<ProductBrandDTO>), (int)HttpStatusCode.OK)]

        public async Task<ActionResult<IList<ProductBrandDTO>>> GetAllBrands()
        {
            var query = new GetAllBrandsQuery();
            var res = await _mediator.Send(query);

            return Ok(res);

        }
    }
}
