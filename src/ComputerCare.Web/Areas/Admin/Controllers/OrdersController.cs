using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerCare.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "ManageOrders")]
public class OrdersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
