using System.Net;
using Basket.Application.Commands;
using Basket.Application.GrpcService;
using Basket.Application.Mappers;
using Basket.Application.Queries;
using Basket.Application.Responses;
using Basket.Core.Entities;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers
{
    public class BasketController : ApiController
    {
        public IMediator _mediator;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IMediator mediator, IPublishEndpoint publishEndpoint, ILogger<BasketController> logger)
        {
            _mediator  = mediator;
            _publishEndpoint = publishEndpoint;
            _logger = logger;
        }

        [HttpGet]
        [Route("[action]/{userName}", Name = "GetBasketByUserName")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> GetBasket(string userName)
        {
            var query = new GetBasketByUserNameQuery(userName);
            var basket = await _mediator.Send(query);
            return Ok(basket);
        }
        
        [HttpPost("CreateBasket")]
        [ProducesResponseType(typeof(ShoppingCartResponse), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<ShoppingCartResponse>> UpdateBasket([FromBody] CreateShoppingCardCommand createShoppingCardCommand)
        {
            var basket = await _mediator.Send(createShoppingCardCommand);
            return Ok(basket);
        }

        [HttpDelete]
        [Route("[action]/{userName}", Name = "DeleteBasketByUserName")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<bool>> DeleteBasket(string userName)
        {
            var query = new DeleteBasketByUserNameCommand(userName);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [Route("[action]", Name = "Checkout")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Accepted)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            //Get the existing basket with username
            var query = new GetBasketByUserNameQuery(basketCheckout.UserName);
            var basket = await _mediator.Send(query);
            if (basket == null)
            {
                return BadRequest();
            }
            var eventMsg = BasketMapper.Mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMsg.TotalPrice = basket.TotalPrice;
            await _publishEndpoint.Publish(eventMsg);
            _logger.LogInformation($"Basket Published for {basket.UserName}");
            //remove the basket after checkout
            var deleteBasketCommand = new DeleteBasketByUserNameCommand(basketCheckout.UserName);
            await _mediator.Send(deleteBasketCommand);
            return Accepted();
        }
    }
}

