using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerCare.Web.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Policy = "ManageUsers")]
public class UsersController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
