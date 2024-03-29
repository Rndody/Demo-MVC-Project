using MVC_Project_Data_Access_Layer.Models;
using System.Collections.Generic;
using System;

namespace MVC_Project_Presentation_Layer.ViewModels
{
    public class DepartmentViewModel
    {

        #region Properties
        public string Code { get; set; } 
        public string Name { get; set; }
        public DateTime DateOfCreation { get; set; }
        #endregion

        #region Navigional Property [Many]      
        public ICollection<Employee> Employees { get; set; } = new HashSet<Employee>();        
        #endregion


    }
}
