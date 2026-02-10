using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerCare.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "ManageProducts")]
public class ProductsController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
