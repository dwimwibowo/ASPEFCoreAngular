using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using AutoMapper;

using DutchTreat.Data;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;

namespace DutchTreat.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[Controller]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private readonly IDutchRepository _repository;
        private readonly ILogger<ProductsController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<StoreUser> _userManager;

        public OrdersController(IDutchRepository repository, 
                                ILogger<ProductsController> logger,
                                IMapper mapper,
                                UserManager<StoreUser> userManager)
        {
            this._repository = repository;
            this._logger = logger;
            this._mapper = mapper;
            this._userManager = userManager;
        }

        [HttpGet]
        public IActionResult Get(bool includeItems = true)
        {
            try
            {
                var username = User.Identity.Name;

                var results = _repository.GetAllOrdersByUser(username, includeItems);

                return Ok(_mapper.Map<IEnumerable<Order>, IEnumerable<OrderViewModel>>(results));
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
                var username = User.Identity.Name;

                var order = _repository.GetOrderById(username, id);

                if (order != null)
                    return Ok(_mapper.Map<Order, OrderViewModel>(order));
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
        public async Task<IActionResult> Post([FromBody] OrderViewModel model)
        {
            // Insert to DB
            try
            {
                if (ModelState.IsValid)
                {
                    var newOrder = _mapper.Map<OrderViewModel, Order>(model);

                    if(newOrder.OrderDate == DateTime.MinValue)
                    {
                        newOrder.OrderDate = DateTime.Now;
                    }

                    var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                    newOrder.User = currentUser;

                    _repository.AddOrder(newOrder);

                    if (_repository.SaveAll())
                    {
                        return Created($"/api/orders/{newOrder.Id}", _mapper.Map<Order, OrderViewModel>(newOrder));
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
