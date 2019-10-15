using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL;
using WebStore.DomainNew.Entities;
using WebStore.Infrastructure.Intefaces;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Services
{
    public class SqlOrdersService : IOrdersService
    {
        private readonly WebStoreContext _context;
        private readonly UserManager<User> _userManager;

        public SqlOrdersService(WebStoreContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IEnumerable<Order> GetUserOders(string userName)
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .Where(o => o.User.UserName == userName)
                .ToList();
        }

        public Order GetOderById(int id)
        {
            return _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .FirstOrDefault(o => o.Id == id);
        }

        public Order CreateOrder(OrderViewModel orderViewModel, CartViewModel cartViewModel, string userName)
        {
            var user = _userManager.FindByNameAsync(userName).Result;

            using (var trans = _context.Database.BeginTransaction())
            {
                var order = new Order()
                {
                    Address = orderViewModel.Address,
                    Name = orderViewModel.Name,
                    Date = DateTime.Now,
                    Phone = orderViewModel.Phone,
                    User = user

                };

                _context.Orders.Add(order);

                foreach (var item in cartViewModel.Items)
                {
                    var productVm = item.Key;
                    var product = _context.Products.FirstOrDefault(p => p.Id == productVm.Id);

                    if(product == null)
                        throw new InvalidOperationException("Товар не найден в БД");

                    var orderItem = new OrderItem()
                    {
                        Price = product.Price,
                        Quantity = item.Value,

                        Order = order,
                        Product = product
                    };

                    _context.OrderItems.Add(orderItem);
                }

                _context.SaveChanges();
                trans.Commit();

                return order;
            }
        }
    }
}
