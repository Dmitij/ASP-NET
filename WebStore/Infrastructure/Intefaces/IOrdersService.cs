using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.DomainNew.Entities;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Intefaces
{
    public interface IOrdersService
    {
        IEnumerable<Order> GetUserOders(string userName);
        Order GetOderById(int id);
        Order CreateOrder(OrderViewModel orderViewModel, CartViewModel cartViewModel, string userName);
    }
}
