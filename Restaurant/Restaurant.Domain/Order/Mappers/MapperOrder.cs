using Restaurant.Domain.Contract.Order;
using Restaurant.Domain.Order.Mappers;
using Riok.Mapperly.Abstractions;

namespace Restaurant.Domain.Order.Mappers
{
    [Mapper]
    public partial class MapperOrder : IMapperOrder
    {
        // Auto-generated mapping for simple properties
        private partial OrderDto MapToDto(Order order);

        public OrderDto ToDto(Order order) => MapToDto(order);

        public Order ToEntity(OrderDto dto)
        {
            // English comments: 
            // 1. Create the aggregate root
            var order = new Order(dto.CustomerId, dto.TableId);

            // 2. Map items using a dedicated mapper or logic
            foreach (var itemDto in dto.Items)
            {
                order.AddItem(itemDto.ProductId, itemDto.Quantity, itemDto.UnitPrice);
            }

            return order;
        }
    }
}
