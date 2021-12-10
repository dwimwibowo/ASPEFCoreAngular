using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DutchTreat.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DutchTreat.Data
{
    public class DutchRepository : IDutchRepository
    {
        private readonly DutchContext _ctx;
        private readonly ILogger<DutchRepository> _logger;

        public DutchRepository(DutchContext ctx, ILogger<DutchRepository> logger)
        {
            this._ctx = ctx;
            this._logger = logger;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            try
            {
                _logger.LogInformation("GetAllProducts was called");

                return _ctx.Products
                        .OrderBy(p => p.Title)
                        .ToList();
            }
            catch(Exception ex)
            {
                _logger.LogInformation($"Failed to get all products: {ex}");
                return null;
            }
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _ctx.Products
                    .Where(p => p.Category == category)
                    .OrderBy(p => p.Title)
                    .ToList();
        }

        public IEnumerable<Order> GetAllOrders(bool includeItems)
        {
            try
            {
                _logger.LogInformation("GetAllOrders was called");

                if (includeItems)
                {
                    return _ctx.Orders
                        .Include(o => o.Items)
                        .ThenInclude(p => p.Product)
                        .OrderByDescending(o => o.OrderDate)
                        .ToList();
                }
                else
                {
                    return _ctx.Orders
                        .OrderByDescending(o => o.OrderDate)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get all orders: {ex}");
                return null;
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                _logger.LogInformation("GetOrderById was called");

                return _ctx.Orders
                        .Include(o => o.Items)
                        .ThenInclude(p => p.Product)
                        .Where(o => o.Id == id)
                        .OrderByDescending(o => o.OrderDate)
                        .FirstOrDefault();
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Failed to get order by id: {ex}");
                return null;
            }
        }

        public bool SaveAll()
        {
            return _ctx.SaveChanges() > 0;
        }

        public void AddEntity(object model)
        {
            _ctx.Add(model);
        }
    }
}
