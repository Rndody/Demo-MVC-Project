using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Data_Access_Layer.Models
{

   public enum Gender {
        [EnumMember(Value ="Male")]
        Male=1, 
        [EnumMember(Value ="Female")]
        Femal = 2 }
   public enum EmployeeType { FullTime=1, PartTime=2 }
    public class Employee
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "name is required")]
        [MaxLength(50, ErrorMessage = "Max Length is 50 Chars")]
        [MinLength(5, ErrorMessage = "Min Length is 5 Chars")]
        public string Name { get; set; }

        [Range(22, 30 /*, ErrorMessage = "Age Must be in Range From 22 To 35"*/)]
        public int? Age { get; set; }



        [RegularExpression(@"^(\d{1,3}-){2}[a-zA-Z]{2,15}-[0-9]{2}-[a-zA-Z]{4,10}-[a-zA-Z]{2,15}$",
           ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        public string Address { get; set; }

        [DataType(DataType.Currency)]
        public decimal Salary { get; set; }

        [Display(Name ="Is Active")]
        public bool IsActive { get; set; }

        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Phone]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name ="Hiring Date")]
        public DateTime HireDate { get; set; }

        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }


        public DateTime CreationDate { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;//soft delete
    }
}
