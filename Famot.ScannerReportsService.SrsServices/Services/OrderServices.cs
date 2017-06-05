using Famot.ScannerReportsService.SrsServices.InterfacesServices;
using System.Collections.Generic;
using System.Linq;
using Famot.ScannerReportsService.DtoEntities;
using AutoMapper;
using Famot.ScannerReportsService.UnitOfWork;
using Famot.ScannerReportsService.SrsServices.Mapping;
using System.Transactions;
using Famot.ScannerReportsService.Entities;

namespace Famot.ScannerReportsService.SrsServices.Services
{
    public class OrderServices : IOrderServices
    {
        private readonly IMapper _mapper;
        private readonly RepositoryManager _repositoryManager;

        public OrderServices()
        {
            _repositoryManager = RepositoryManager.Instance;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<OrderDtoToOrder>();
                cfg.AddProfile<OrderToOrderDto>();
            });
            _mapper = new Mapper(config);
        }
        public int CreateOrder(OrderDto orderDto)
        {
            using (var scope = new TransactionScope())
            {
                var order = _mapper.Map<OrderDto, Order>(orderDto);
                _repositoryManager.Repositories<Order>().Insert(order);
                _repositoryManager.Save();
                scope.Complete();
                return order.OrderID;
            }
        }

        public bool DeleteOrder(int orderId)
        {
            var success = false;
            if (orderId > 0)
            {
                using (var scope = new TransactionScope())
                {
                    var order = _repositoryManager.Repositories<Order>().GetByID(orderId);
                    if (order != null)
                    {
                        _repositoryManager.Repositories<Order>().Delete(order);
                        _repositoryManager.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }

        public IEnumerable<OrderDto> GetAllOrders()
        {
            var orders = _repositoryManager.Repositories<Order>().GetAll().ToList();
            if (orders.Any())
            {
                var ordersDto = _mapper.Map<List<Order>, List<OrderDto>>(orders);
                return ordersDto;
            }
            return null;
        }

        public OrderDto GetOrderById(int orderId)
        {
            var order = _repositoryManager.Repositories<Order>().GetByID(orderId);
            if (order != null)
            {
                var orderDto = _mapper.Map<Order, OrderDto>(order);
                return orderDto;
            }
            return null;
        }

        public bool UpdateOrder(int orderId, OrderDto orderDto)
        {
            bool success = false;
            if (orderDto != null)
            {
                using (var scope = new TransactionScope())
                {
                    var order = _repositoryManager.Repositories<Order>().GetByID(orderId);
                    if (order != null)
                    {
                        order = _mapper.Map<OrderDto, Order>(orderDto);
                        _repositoryManager.Repositories<Order>().Update(order);
                        _repositoryManager.Save();
                        scope.Complete();
                        success = true;
                    }
                }
            }
            return success;
        }
    }
}
