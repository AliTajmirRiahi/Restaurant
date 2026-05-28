using Arta.Base.Core.ApiResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application;
using Restaurant.Domain.Contract.Order;
using Restaurant.Domain.Order.Mappers;
using Restaurant.Domain.Order.Validators;
using Restaurant.Presentation.Configs.ApiResults;
using Restaurant.Presentation.Validators;
using System.Threading.Tasks;

// Assuming the namespace of your project
namespace Restaurant.Presentation.Controllers.Orders
{
    public class OrderController : PresentationControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        /// <summary>
        /// Creates a new order and returns 201 Created status.
        /// </summary>
        [HttpPost(Name = "AddOrder")]
        [Validator(typeof(OrderBasicValidator), "orderDto")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddOrder([FromBody] OrderDto orderDto)
        {
            // Execute business logic via Application Service
            var orderId = await _orderService.AddAsync(orderDto);

            // Return 201 Created with the location of the resource
            // Note: 'GetOrderById' is the name of GET endpoint method
            return CreatedAtAction(
                nameof(GetOrderById),
                new { id = orderId },
                ApiResult<GuidEntity>.Ok(new GuidEntity(orderId))
            );
        }

        /// <summary>
        /// Gets an order by Id.
        /// </summary>
        [HttpGet("{id}", Name = "GetOrderById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetOrderById(Guid id)
        {
            // Gets domain/application DTO
            var order = await _orderService.GetAsync(id);

            // Here service throws NotFoundException when not found,
            // and middleware automatically returns 404.
            // So no need to check "order == null" here.

            return Ok(ApiResult<OrderDto>.Ok(order));
        }
    }
}
