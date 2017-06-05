using Famot.ScannerReportsService.DtoEntities;
using System.Collections.Generic;

namespace Famot.ScannerReportsService.SrsServices.InterfacesServices
{
    public interface IOrderServices
    {
        OrderDto GetOrderById(int orderId);
        IEnumerable<OrderDto> GetAllOrders();
        int CreateOrder(OrderDto orderDto);
        bool UpdateOrder(int orderId, OrderDto orderDto);
        bool DeleteOrder(int orderId);
    }
}
