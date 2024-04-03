using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Business_Logic_Layer.Repositories;
using MVC_Project_Data_Access_Layer.Models;
using MVC_Project_Presentation_Layer.Helpers;
using MVC_Project_Presentation_Layer.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

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
        public async Task<IActionResult> Index(string searchInput) //[HttpGet] or [HttpPost in case of Search] 
        {
            var employees = Enumerable.Empty<Employee>();
            var emploeeRepo = unitOfWork.Repository<Employee>() as EmployeeRepository;
            if (string.IsNullOrEmpty(searchInput))
                employees = await /* unitOfWork.EmployeeRepository*/emploeeRepo.GetAllAsync();
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
        public async Task<IActionResult> Create(EmployeeViewModel employeeVM)///Create --- 1st action [request]
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
                employeeVM.ImageName = await DocumentSettings.UploadFile(employeeVM.Image, "Images"); //save the image before SaveChanges
                ///AutoMapper
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                /* the automapper need to learn how to make mapping for my classes so we make a profile
                 * create Helper Folder that contains helper classes in the PL ----> create MappingProfiles class
                 */
                //  var count = employeeRepo.Add(mappedEmp);

                unitOfWork.Repository<Employee>().Add(mappedEmp);
                var count = await unitOfWork.Complete(); //replaces SaveChanges(); which returns no. of rows affected
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
        public async Task<IActionResult> Details(int? id, string ViewName = "Details")
        {
            if (id == null)
                return BadRequest();
            var employee = await unitOfWork.Repository<Employee>().GetAsync(id.Value);

            var mappedEmp = mapper.Map<Employee, EmployeeViewModel>(employee);

            if (employee == null)
                return NotFound();

            if (ViewName.Equals("Delete", System.StringComparison.OrdinalIgnoreCase) || ViewName.Equals("Edit", System.StringComparison.OrdinalIgnoreCase))
                TempData["ImageName"] = employee.ImageName;

            return View(ViewName, mappedEmp);
        }
        //-------------------- Edit ------------------------
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            ///if (id == null)
            ///    return BadRequest();
            ///var employee = employeeRepo.Get(id.Value);
            ///if (employee == null)
            ///    return NotFound();
            ///return View(employee);
            ///
            //ViewData["Departments"] = departmentRepository.GetAll();
            return await Details(id, "Edit");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id) return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    employeeVM.ImageName = TempData["ImageName"] as string;
                    var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);
                    #region check if image is changed
                    if (mappedEmp.ImageName != null)
                    {
                        if (mappedEmp.ImageName != TempData["ImageName"] as string)
                        {
                            DocumentSettings.DeleteFile(TempData["ImageName"] as string, "Images");
                            mappedEmp.ImageName = await DocumentSettings.UploadFile(employeeVM.Image, "Images");
                        }
                    }
                    #endregion
                    unitOfWork.Repository<Employee>().Update(mappedEmp);
                    await unitOfWork.Complete();//remember we removed the savechanges from the methods in the repository so we need to use it here 

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
        public async Task<IActionResult> Delete(int? id)
        { return await Details(id, "Delete"); }

        [HttpPost]
        public async Task<IActionResult> Delete(EmployeeViewModel employeeVM, [FromRoute] int id)
        {
            if (id != employeeVM.Id) return BadRequest();
            try
            {
                employeeVM.ImageName = TempData["ImageName"] as string;
                var mappedEmp = mapper.Map<EmployeeViewModel, Employee>(employeeVM);

                unitOfWork.Repository<Employee>().Delete(mappedEmp);
                var count = await unitOfWork.Complete();
                if (count > 0)
                {
                    DocumentSettings.DeleteFile(employeeVM.ImageName, "Images");
                    return RedirectToAction(nameof(Index));
                }
                return View(employeeVM);
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
