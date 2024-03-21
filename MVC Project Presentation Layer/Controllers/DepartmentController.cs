using Microsoft.AspNetCore.Mvc;
using MVC_Project_Business_Logic_Layer.Repositories;

namespace MVC_Project_Presentation_Layer.Controllers
{
    public class DepartmentController : Controller
    {

        private DepartmentRepository departmentRepo;
        public IActionResult Index()
        {
            return View();
        }
    }
}
