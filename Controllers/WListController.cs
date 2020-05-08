using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;

namespace w_list.Controllers
{
    public class WListController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult W(string forum)
        {
            ViewData["Forum"] = forum;
            return View();
        }
    }
}