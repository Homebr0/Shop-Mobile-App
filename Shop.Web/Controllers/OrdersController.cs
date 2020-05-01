using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Web.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Web.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var model = await orderRepository.GetOrdersAsync(this.User.Identity.Name);
            return View(model);
        }


    }
}
