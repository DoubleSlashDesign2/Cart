using CartWeb.Data.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CartWeb.Data
{
    public class CartSeeder
    {
        private readonly CartContext _ctx;

        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public CartSeeder(CartContext ctx, IHostingEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task SeedAsync()
        {
            _ctx.Database.EnsureCreated();

            var user = await _userManager.FindByEmailAsync("yuvak7@gmail.com");

            if (user == null)
            {
                user = new StoreUser()
                {
                    LastName = "Kesavan",
                    FirstName = "Yuvaraj",
                    Email = "yuvak7@gmail.com",
                    UserName = "yuvak7@gmail.com"
                };

                var result = await _userManager.CreateAsync(user, "P@ssw0rd!");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Could not create user in Seeding");
                }
            }

            if (!_ctx.Products.Any())
            {
                // Need to create sample data
                var filepath = Path.Combine(_hosting.ContentRootPath, "Data/art.json");
                var json = File.ReadAllText(filepath);
                var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(json);
                _ctx.Products.AddRange(products);

                var order = _ctx.Orders.Where(o => o.Id == 1).FirstOrDefault();
                if (order != null)
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
