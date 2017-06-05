using AutoMapper;
using Famot.ScannerReportsService.DtoEntities;
using Famot.ScannerReportsService.Entities;

namespace Famot.ScannerReportsService.SrsServices.Mapping
{
    public class OrderDtoToOrder : Profile
    {
        public OrderDtoToOrder()
        {
            CreateMap<OrderDto, Order>();
        }
    }
}
