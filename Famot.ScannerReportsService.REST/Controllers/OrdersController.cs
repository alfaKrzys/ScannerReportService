using Famot.ScannerReportsService.DtoEntities;
using Famot.ScannerReportsService.SrsServices.InterfacesServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Famot.ScannerReportsService.REST.Controllers
{
    [RoutePrefix("api/orders")]
    public class OrdersController : ApiController
    {
        private readonly IOrderServices _orderServices;

        public OrdersController(IOrderServices orderServices)
        {
            _orderServices = orderServices;
        }
        // GET: api/Orders
        public IHttpActionResult Get()
        {
            var orders = _orderServices.GetAllOrders();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        // GET: api/Orders/5
        [Route("{id:int}", Name = "GetOrderById")]
        public IHttpActionResult Get(int id)
        {
            var order = _orderServices.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }
            return Ok(order);
        }

        // POST: api/Orders
        public IHttpActionResult Post([FromBody]OrderDto order)
        {
            if (order != null)
            {
                order.OrderID = _orderServices.CreateOrder(order);
                var location = new Uri(Url.Link("GetOrderById", new { id = order.OrderID }));
                return Created(location, order);
            }
            return BadRequest("Nie utworzono zlecenia");
        }

        // PUT: api/Orders/5
        [Route("{id:int}")]
        public IHttpActionResult Put(int id, [FromBody]OrderDto order)
        {
            if (_orderServices.UpdateOrder(id, order))
            {
                return Ok();
            }
            return BadRequest("Nie zaktualizowano zlecenia");
        }

        // DELETE: api/Orders/5
        [Route("{id:int}")]
        public IHttpActionResult Delete(int id)
        {
            if (_orderServices.DeleteOrder(id))
            {
                return Ok();
            }
            return BadRequest("Nie znaleziono zlecenia do usunięcia");
        }
    }
}
