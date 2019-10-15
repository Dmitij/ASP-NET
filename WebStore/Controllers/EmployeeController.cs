using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Intefaces;
using WebStore.ViewModels;

namespace WebStore.Controllers
{
    [Route("users")]
    [Authorize]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // GET: Home
        [Route("all")]
        [AllowAnonymous]
        public ActionResult Index()
        {
            //return Content("Привет. Я твой первый контроллер");
            return View(_employeeService.GetAll());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult Details(int id)
        {
            //return Content("Привет. Я твой первый контроллер");
            return View(_employeeService.GetById(id));
        }

        [HttpGet]
        [Route("edit/{id?}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return View(new EmployeeView());

            EmployeeView model = _employeeService.GetById(id.Value);
            if (model == null)
                return NotFound();// возвращаем результат 404 Not Found

            return View(model);
        }

        [HttpPost]
        [Route("edit/{id?}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Edit(EmployeeView model)
        {
            if (model.Age < 18 || model.Age > 100)
            {
                ModelState.AddModelError("Age", "Несовершеннолетним вход воспрещен");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (model.Id > 0) // если есть Id, то редактируем модель
            {
                var dbItem = _employeeService.GetById(model.Id);

                if (ReferenceEquals(dbItem, null))
                    return NotFound();// возвращаем результат 404 Not Found

                dbItem.FirstName = model.FirstName;
                dbItem.LastName = model.LastName;
                dbItem.Age = model.Age;
                dbItem.Patronymic = model.Patronymic;
                //dbItem.Position = model.Position;
            }
            else // иначе добавляем модель в список
            {
                _employeeService.AddNew(model);
            }
            _employeeService.Commit(); // станет актуальным позднее (когда добавим БД)

            return RedirectToAction(nameof(Index));
        }

        [Route("delete/{id}")]
        [Authorize(Roles = "Administrator")]
        public IActionResult Delete(int id)
        {
            _employeeService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}