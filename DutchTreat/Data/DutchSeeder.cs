using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using System.IO;
using Newtonsoft.Json;

using DutchTreat.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DutchTreat.Data
{
    public class DutchSeeder
    {
        private readonly DutchContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public DutchSeeder(
            DutchContext ctx,
            IHostingEnvironment hosting,
            UserManager<StoreUser> userManager)
        {
            this._ctx = ctx;
            this._hosting = hosting;
            this._userManager = userManager;
        }

        public async Task SeedAsync()
        {
            // Checking Database
            _ctx.Database.EnsureCreated();

            // Checking user exists
            StoreUser user = await _userManager.FindByEmailAsync("dwimwibowo@gmail.com");

            if(user == null)
            {
                user = new StoreUser
                {
                    FirstName = "Dwi",
                    LastName = "Wibowo",
                    Email = "dwimwibowo@gmail.com",
                    UserName = "dwimwibowo@gmail.com"
                };

                var result = await _userManager.CreateAsync(user, "dutchtreat");

                if(result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create new user in seeder");
                }
            }

            // Checking data exists
            if (!_ctx.Products.Any())
            {
                // Need to create sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);

                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();

                if(order != null)
                {
                    order.User = user;
                    order.Items = new List<OrderItem>()
                    {
                        new OrderItem()
                        {
                            Product = products.First(),
                            Quantity = 5,
                            UnitPrice = products.First().Price
                        }
                    };
                }

                _ctx.SaveChanges();
            }
        }
    }
}
