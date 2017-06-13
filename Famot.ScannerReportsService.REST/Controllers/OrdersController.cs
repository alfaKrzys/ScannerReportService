using Famot.ScannerReportsService.DtoEntities;
using Famot.ScannerReportsService.REST.App_Start;
using Famot.ScannerReportsService.REST.Extensions;
using Famot.ScannerReportsService.SrsServices.InterfacesServices;
using Ninject;
using System;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Famot.ScannerReportsService.REST.Controllers
{
    [RoutePrefix("api/orders")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class OrdersController : ApiController
    {
        [Inject]
        public IOrderServices OrderServices { private get; set; }

        public OrdersController()
        {
            NinjectWebCommon.Kernel.Inject(this);
        }
        // GET: api/Orders
        [CustomAuthorize("ScannerReports", "read")]
        public IHttpActionResult Get()
        {
            var orders = OrderServices.GetAllOrders();
            if (orders == null)
            {
                return NotFound();
            }
            return Ok(orders);
        }

        // GET: api/Orders/5
        [Route("{id:int}", Name = "GetOrderById")]
        [CustomAuthorize("ScannerReports", "read")]
        public IHttpActionResult Get(int id)
        {
            var order = OrderServices.GetOrderById(id);
            if (order == null)
            {
                return NotFound();
            }            
            return Ok(order);
        }

        // POST: api/Orders
        [CustomAuthorize("ScannerReports", "change")]
        public IHttpActionResult Post([FromBody]OrderDto order)
        {
            if (order != null && OrderServices.GetOrderById(order.OrderID) == null)
            {
                order.OrderID = OrderServices.CreateOrder(order);
                var location = new Uri(Url.Link("GetOrderById", new { id = order.OrderID }));
                return Created(location, order);
            }
            return BadRequest("Nie utworzono zlecenia");
        }

        // PUT: api/Orders/5
        [Route("{id:int}")]
        [CustomAuthorize("ScannerReports", "change")]
        public IHttpActionResult Put(int id, [FromBody]OrderDto order)
        {
            if (OrderServices.UpdateOrder(id, order))
            {
                return Ok();
            }
            return BadRequest("Nie zaktualizowano zlecenia");
        }

        // DELETE: api/Orders/5
        [Route("{id:int}")]
        [CustomAuthorize("ScannerReports", "change")]
        public IHttpActionResult Delete(int id)
        {
            if (OrderServices.DeleteOrder(id))
            {
                return Ok();
            }
            return BadRequest("Nie znaleziono zlecenia do usunięcia");
        }
    }
}
