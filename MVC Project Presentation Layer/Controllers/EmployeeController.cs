using Microsoft.AspNetCore.Mvc;
using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Business_Logic_Layer.Repositories;
using MVC_Project_Data_Access_Layer.Models;

namespace MVC_Project_Presentation_Layer.Controllers
{
    public class EmployeeController : Controller
    {

        private readonly IEmployeeRepository employeeRepo;

        ///Constructor
        public EmployeeController(IEmployeeRepository employeeRepository)
        { employeeRepo = employeeRepository; }

        ///Methods
        public IActionResult Index() //HttpGet
        {
            ///1-ViewData
            ///ViewData["Message"] = "ViewData";  //sending extra info 
            /// ====== key ======== value ====

            /// 2-ViewBag
            ///  ViewBag.Message = "ViewBag";
            ///this property msg will override the ViewData msg as both of them deal with the same place and the ViewBag is written after ViewData

            ///3-TempData
            TempData.Keep(); //to keep msg sent from previous  action[request]

            var employees = employeeRepo.GetAll();
            return View(employees); //send main info [model]
        }
        [HttpGet]
        public IActionResult Create() { return View(); }
        [HttpPost]
        public IActionResult Create(Employee employee)///Create --- 1st action [request]
        {
            if (ModelState.IsValid)
            {
                var count = employeeRepo.Add(employee);
                ///TempData
                if (count > 0)
                    TempData["Message"] = "Created Successfully";//info we need to send to 2nd request
                else
                    TempData["Message"] = "Failed to create";//info we need to send to 2nd request
                return RedirectToAction(nameof(Index));/// Index --- 2nd action [request]
            }
            return View(employee);
        }


        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();
            var employee = employeeRepo.Get(id.Value);
            if (employee == null)
                return NotFound();
            return View(ViewName, employee);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ///if (id == null)
            ///    return BadRequest();
            ///var employee = employeeRepo.Get(id.Value);
            ///if (employee == null)
            ///    return NotFound();
            ///return View(employee);
            ///

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee, [FromRoute] int id)
        {

            if (id != employee.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    employeeRepo.Update(employee);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(employee);
        }

        [HttpGet]
        public IActionResult Delete(int? id)

        {
            return Details(id, "Delete");

        }

        [HttpPost]
        public IActionResult Delete(Employee employee, [FromRoute] int id)
        {

            if (id != employee.Id) return BadRequest();

            try
            {
                employeeRepo.Delete(employee);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
                return View(employee);
            }
        }


    }
}
