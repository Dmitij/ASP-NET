using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Intefaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IOrdersService _ordersService;

        // ctor
        public ProfileController(IOrdersService ordersService)
        {
            _ordersService = ordersService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Orders()
        {
            var orders = _ordersService.GetUserOders(User.Identity.Name);
            var orderModels = new List<UserOrderViewModel>(orders.Count());

            foreach (var order in orders)
            {
                orderModels.Add(new UserOrderViewModel()
                {
                    Id = order.Id,
                    Name = order.Name,
                    Address = order.Address,
                    Phone = order.Phone,
                    TotalSum = order.OrderItems.Sum(o => o.Price * o.Quantity)
                });
            }
            return View(orderModels);

        }
    }
}