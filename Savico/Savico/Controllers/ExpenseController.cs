using Microsoft.AspNetCore.Mvc;

namespace Savico.Controllers
{
    public class ExpenseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
