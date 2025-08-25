using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Basket.APi.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{versio:apiVersion}/[controller]")]
    [ApiController]
    public class BaseApiController:ControllerBase
    {
    }
}
