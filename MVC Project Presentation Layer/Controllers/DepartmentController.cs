using Microsoft.AspNetCore.Mvc;
using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Business_Logic_Layer.Repositories;
using MVC_Project_Data_Access_Layer.Models;

namespace MVC_Project_Presentation_Layer.Controllers
{
    public class DepartmentController : Controller
    {

        private readonly IDepartmentRepository departmentRepo;

        ///Constructor
        public DepartmentController(IDepartmentRepository departmentRepository)
        { departmentRepo = departmentRepository; }

        ///Methods
        public IActionResult Index()
        {
            var departments = departmentRepo.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create() { return View(); }
        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid)
            {
                departmentRepo.Add(department);
                return RedirectToAction(nameof(Index));

            }
            return View(department);
        }


        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();
            var department = departmentRepo.Get(id.Value);
            if (department == null)
                return NotFound();
            return View(ViewName, department);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            //if (id == null)
            //    return BadRequest();
            //var department = departmentRepo.Get(id.Value);
            //if (department == null)
            //    return NotFound();
            //return View(department);
            return Details(id, "Edit");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Department department, [FromRoute] int id)
        {

            if (id != department.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    departmentRepo.Update(department);
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(department);
        }

        [HttpGet]
        public IActionResult Delete(int? id)

        {
            return Details(id, "Delete");

        }

        [HttpPost]
        public IActionResult Delete(Department department, [FromRoute] int id)
        {

            if (id != department.Id) return BadRequest();

            try
            {
                departmentRepo.Delete(department);
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception exception)
            {
                ModelState.AddModelError(string.Empty , exception.Message);
                return View(department);
            }
        }
    }
}
