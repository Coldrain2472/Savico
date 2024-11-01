using Microsoft.AspNetCore.Mvc;

namespace Savico.Controllers
{
    public class BudgetController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
