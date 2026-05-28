using Arta.Application.Core;
using Arta.Base.Core.Exceptions;
using Restaurant.Domain.Contract.Order;
using Restaurant.Domain.Order;
using Restaurant.Domain.Order.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application
{
    public class OrderService : ApplicationService, IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapperOrder _mapper;

        public OrderService(IOrderRepository orderRepository, IMapperOrder mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(OrderDto orderDto)
        {
            // Map DTO to Domain Entity
            var order = _mapper.ToEntity(orderDto);

            return await _orderRepository.AddAsync(order);
        }

        public async Task<OrderDto> GetAsync(Guid id)
        {
             var order = await _orderRepository.GetByIdAsync(id);

            if (order == null)
                throw new NotFoundException($"Order with id {id} was not found.","OrderNotFoundError");

            return _mapper.ToDto(order);
        }
    }
}
