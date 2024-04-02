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

        #region Attributes
        //   private readonly IDepartmentRepository departmentRepo;
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        #endregion

        #region Constructor
        public DepartmentController(IMapper mapper, IUnitOfWork unitOfWork/*IDepartmentRepository departmentRepository*/)
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            //departmentRepo = departmentRepository;
        }

        #endregion

        #region Methods
        //-------------------------- Index ---------------------
        public IActionResult Index()
        {           
            var departments = unitOfWork.Repository<Department>().GetAll();

            var mappedDeps = mapper.Map<IEnumerable<Department>, IEnumerable<DepartmentViewModel>>(departments);

            return View(mappedDeps);
        }
        [HttpGet]
        //------------------------ Create -----------------------
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (ModelState.IsValid)
            {
                var mappedDep = mapper.Map<DepartmentViewModel, Department>(departmentVM);
                unitOfWork.Repository<Department>().Add(mappedDep);
                var count = unitOfWork.Complete();
                return RedirectToAction(nameof(Index));

            }

            return View(departmentVM);
        }

        //------------------------------ Details -------------------------
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();
            var department = unitOfWork.Repository<Department>().Get(id.Value);
            var mappedDep = mapper.Map<Department, DepartmentViewModel>(department);
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
        //-------------------- Edit ------------------------
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
                    unitOfWork.Repository<Department>().Update(mappedDep);
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
        //-------------------- Delete ------------------------
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
                unitOfWork.Repository<Department>().Delete(mappedDep);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
                return View(departmentVM);
            }
        }
        #endregion

    }
}
