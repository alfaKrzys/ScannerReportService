using AutoMapper;
using Famot.ScannerReportsService.DtoEntities;
using Famot.ScannerReportsService.Entities;

namespace Famot.ScannerReportsService.SrsServices.Mapping
{
    public class OrderToOrderDto : Profile
    {
        public OrderToOrderDto()
        {
            CreateMap<Order, OrderDto>();
        }
    }
}
