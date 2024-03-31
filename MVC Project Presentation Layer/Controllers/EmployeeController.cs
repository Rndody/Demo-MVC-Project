using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Business_Logic_Layer.Repositories;
using MVC_Project_Data_Access_Layer.Models;
using MVC_Project_Presentation_Layer.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace MVC_Project_Presentation_Layer.Controllers
{
    public class EmployeeController : Controller
    {
        #region Attributes
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        //  private readonly IEmployeeRepository employeeRepo;
        //private readonly IDepartmentRepository departmentRepository; 
        #endregion

        #region Constructor
        public EmployeeController(IMapper mapper, IUnitOfWork unitOfWork/*,IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository*/     )
        {
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            //employeeRepo = employeeRepository;
            //this.departmentRepository = departmentRepository;
        }

        #endregion

        #region Methods
        //-------------------------- Index ---------------------
        public IActionResult Index(string searchInput) //[HttpGet] or [HttpPost in case of Search] 
        {
            var employees = Enumerable.Empty<Employee>();
            var emploeeRepo = unitOfWork.Repository<Employee>() as EmployeeRepository;
            if (string.IsNullOrEmpty(searchInput))
                employees =/* unitOfWork.EmployeeRepository*/emploeeRepo.GetAll();
            else
                employees = /* unitOfWork.EmployeeRepository*/emploeeRepo.SearchByName(searchInput.ToLower());  //create method to search by name in the BLL 

            var mappedEmps = mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeViewModel>>(employees);

            return View(mappedEmps);

            ///1-ViewData
            ///ViewData["Message"] = "ViewData";  //sending extra info 
            /// ====== key ======== value ====

            /// 2-ViewBag
            ///  ViewBag.Message = "ViewBag";
            ///this property msg will override the ViewData msg as both of them deal with the same place and the ViewBag is written after ViewData

            ///3-TempData
            /// TempData.Keep(); //to keep msg sent from previous  action[request]

        }
        //------------------------ Create -----------------------
        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Departments"]=departmentRepository.GetAll();
            //send the departments object as extra info we can't send it as model because we are [Binding] dealing with Employee Model 
            return View();
        }
        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeVM)///Create --- 1st action [request]
        {
            if (ModelState.IsValid)
            {
                ///Manual Mapping                
                ///1st way --->
                ///var mappedEmp = new Employee()
                ///{
                ///    Name = employeeVM.Name,
                ///    Age = employeeVM.Age,
                ///    Address = employeeVM.Address,
                ///    Salary = employeeVM.Salary,
                ///    Email = employeeVM.Email,
                ///    PhoneNumber = employeeVM.PhoneNumber,
                ///    IsActive = employeeVM.IsActive,
                ///    HireDate = employeeVM.HireDate,
                ///};               
                ///2nd way ---> overload the casting operator                
                /// Employee mappedEmp = (Employee) employeeVM;
                /// 
           //------------------------------------------------------------------------
                ///AutoMapper
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                /* the automapper need to learn how to make mapping for my classes so we make a profile
                 * create Helper Folder that contains helper classes in the PL ----> create MappingProfiles class
                 */
                //  var count = employeeRepo.Add(mappedEmp);
                unitOfWork.Repository<Employee>().Add(mappedEmp);
                var count = unitOfWork.Complete(); //replaces SaveChanges(); which returns no. of rows affected
                ///TempData
                if (count > 0)
                    TempData["Message"] = "Created Successfully";//info we need to send to 2nd request
                else
                    TempData["Message"] = "Failed to create";//info we need to send to 2nd request
                return RedirectToAction(nameof(Index));/// Index --- 2nd action [request]
            }
            return View(employeeVM);
        }

        //------------------------------ Details -------------------------
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();
            var employee = unitOfWork.Repository<Employee>().Get(id.Value);

            var mappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee == null)
                return NotFound();
            return View(ViewName, mappedEmp);
        }
        //-------------------- Edit ------------------------
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
            //ViewData["Departments"] = departmentRepository.GetAll();
            return Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {

            if (id != employeeVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    unitOfWork.Repository<Employee>().Update(mappedEmp);
                    unitOfWork.Complete();//remember we removed the savechanges from the methods in the repository so we need to use it here 
                    return RedirectToAction(nameof(Index));
                }
                catch (System.Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(employeeVM);
        }

        //-------------------- Delete ------------------------
        [HttpGet]
        public IActionResult Delete(int? id)

        {
            return Details(id, "Delete");

        }

        [HttpPost]
        public IActionResult Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {

            if (id != employeeVM.Id) return BadRequest();

            try
            {
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                unitOfWork.Repository<Employee>().Delete(mappedEmp);
                unitOfWork.Complete();
                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception exception)
            {
                ModelState.AddModelError(string.Empty, exception.Message);
                return View(employeeVM);
            }
        }
        #endregion

    }
}
