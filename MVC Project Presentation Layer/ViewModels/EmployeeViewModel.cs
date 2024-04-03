using MVC_Project_Data_Access_Layer.Models;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace MVC_Project_Presentation_Layer.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; } //class view model doesn't inherit from class ModelBase so we'll create the Id property as it is not inherited
       
        #region Properties
        [Required(ErrorMessage = "name is required")] 
        [MaxLength(50, ErrorMessage = "Max Length is 50 Chars")] 
        [MinLength(5, ErrorMessage = "Min Length is 5 Chars")]
        public string Name { get; set; }

        [Range(22, 60 , ErrorMessage = "Age Must be in Range From 22 To 60")]
        public int? Age { get; set; }

        [RegularExpression(@"^(\d{1,3}-)[a-zA-Z]{2,15}-[a-zA-Z]{4,10}-[a-zA-Z]{2,15}$", 
           ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)] 
        public decimal Salary { get; set; } 

        [Display(Name = "Is Active")] 
        public bool IsActive { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Phone]
        [Display(Name = "Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
        [Display(Name = "Hiring Date")]
        public DateTime HireDate { get; set; }
        //public DateTime CreationDate { get; set; } = DateTime.Now;       
        //public bool IsDeleted { get; set; } = false; 
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        #endregion

        #region Foreign  Key    
        public int? DepartmentId { get; set; }       
        #endregion
        #region Navigional Property [One]       
        public Department Department { get; set; }
        #endregion
        ///adding property to display the image 
        public IFormFile Image { get; set; }
        public string ImageName { get; set; }




    }
}
