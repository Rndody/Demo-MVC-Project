using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Business_Logic_Layer.Repositories;
using MVC_Project_Data_Access_Layer.Models;
using MVC_Project_Presentation_Layer.ViewModels;
using System.Collections.Generic;

namespace MVC_Project_Presentation_Layer.Controllers
{
    public class DepartmentController : Controller
    {

        //   private readonly IDepartmentRepository departmentRepo;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        ///Constructor
        public DepartmentController(IMapper mapper, IUnitOfWork unitOfWork/*IDepartmentRepository departmentRepository*/)
        {
            //departmentRepo = departmentRepository;
          }

        ///Methods
        public IActionResult Index()
        {
            //------------ NullReferenceException: Object reference not set to an instance of an object in line 27!!!!
            var departments = unitOfWork.DepartmentRepository.GetAll();

            var mappedDeps = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

            return View(mappedDeps);

        
        }
        [HttpGet]
        public IActionResult Create() { 
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
            var mappedDep = mapper.Map<DepartmentViewModel,Department>(departmentVM);
                unitOfWork.DepartmentRepository.Add(mappedDep);
                var count = unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }

            return View(departmentVM);
        }


        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();
            var department = unitOfWork.DepartmentRepository.Get(id.Value);
            var mappedDep = mapper.Map<Department,DepartmentViewModel>(department);
            if (department == null)
                return NotFound();
            return View(ViewName, mappedDep);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
           ///if (id == null)
           ///    return BadRequest();
           ///var department = departmentRepo.Get(id.Value);
           ///if (department == null)
           ///    return NotFound();
           ///return View(department);
           ///

            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentViewModel departmentVM, [FromRoute] int id)
        {

            if (id != departmentVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                    unitOfWork.DepartmentRepository.Update(mappedDep);
                    unitOfWork.Complete();
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(departmentVM);
        }

        [HttpGet]
        public IActionResult Delete(int? id)

        {
            return Details(id, "Delete");

        }

        [HttpPost]
        public IActionResult Delete(DepartmentViewModel departmentVM, [FromRoute] int id)
        {

            if (id != departmentVM.Id) return BadRequest();

            try
            {
                var mappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                unitOfWork.DepartmentRepository.Delete(mappedDep);
                unitOfWork.Complete();  
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception exception)
            {
                ModelState.AddModelError(string.Empty , exception.Message);
                return View(departmentVM);
            }
        }
    }
}
