using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;

namespace DutchTreat.Controllers
{
    [Route("api/[Controller]")]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;

        public OrdersController(IDutchRepository repository, ILogger<ProductsController> logger)
        {
            this._repository = repository;
            this._logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_repository.GetAllOrders());
            }
            catch (Exception ex)
            {
                var errMsg = $"Failed to get orders: {ex}";
                _logger.LogError(errMsg);
                return BadRequest(errMsg);
            }
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _repository.GetOrderById(id);

                if (order != null)
                    return Ok(order);
                else
                    return NotFound();
            }
            catch (Exception ex)
            {
                var errMsg = $"Failed to get orders by Id: {ex}";
                _logger.LogError(errMsg);
                return BadRequest(errMsg);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] OrderViewModel model)
        {
            // Insert to DB
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = new Order {
                        OrderDate = model.OrderDate,
                        OrderNumber = model.OrderNumber,
                        Id = model.OrderId
                    };

                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    _repository.AddEntity(newOrder);

                    if (_repository.SaveAll())
                    {
                        var vm = new OrderViewModel()
                        {
                            OrderId = newOrder.Id,
                            OrderDate = newOrder.OrderDate,
                            OrderNumber = newOrder.OrderNumber
                        };

                        return Created($"/api/orders/{vm.OrderId}", vm);
                    }
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                var errMsg = $"Failed to save a new order: {ex}";
                _logger.LogError(errMsg);
            }
            return BadRequest("Failed to save a new order");
        }
    }
}
