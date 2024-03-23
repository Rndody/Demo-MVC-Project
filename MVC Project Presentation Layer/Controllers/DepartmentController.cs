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
        { departmentRepo = departmentRepository;        }

        ///Methods
        public IActionResult Index()
        {
            var departments = departmentRepo.GetAll();
            return View(departments);
        }
        [HttpGet]
        public IActionResult Create() { return View(); }
        [HttpPost]
        public IActionResult Create(Department  department) 
        {
            if (ModelState.IsValid) {
                departmentRepo.Add(department);
                return RedirectToAction(nameof(Index));

            }
            return View(department);
        }




    }
}
