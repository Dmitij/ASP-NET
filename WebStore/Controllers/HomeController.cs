using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            
        }

        //[SimpleActionFilter]
        public IActionResult Index()
        {
            //throw new Exception("Упс! Что-то пошло не так");

            //return StatusCode(403);
            //return new RedirectResult("https://google.com");
            //return new RedirectToActionResult("Blog", "home", null);
            //return new JsonResult("{'key': 'value'}");
            //return NotFound(); // return new NotFoundResult();
            //return new EmptyResult();
            //return new ContentResult();
            
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }
    }
}