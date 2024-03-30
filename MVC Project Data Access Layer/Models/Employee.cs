using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Project_Data_Access_Layer.Models
{
    public enum Gender // we will use enum as property in the Employee class which is Public , so enum must be public also
    {
        [EnumMember(Value = "Male")]
        Male = 1,
        [EnumMember(Value = "Female")]
        Femal = 2
    }
    public enum EmployeeType
    {
        [EnumMember(Value = "FullTime")]
        FullTime = 1,
        [EnumMember(Value = "PartTime")]
        PartTime = 2
    }
    public class Employee : ModelBase
    {
        #region Properties        
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Address { get; set; }
        public decimal Salary { get; set; } //by convention decimal (18, 2) or change it in Fluent API Configurations
        public bool IsActive { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime HireDate { get; set; }
        #region Properties that won't be mapped in the ViewModel will take default values in the Model
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false; // won't be displayed for end user  
        #endregion
        public Gender Gender { get; set; }
        public EmployeeType EmployeeType { get; set; }
        #endregion
        #region Foreign  Key
        public int? DepartmentId { get; set; }       
        #endregion
        #region Navigional Property [One]        
        public Department Department { get; set; }
        #endregion


        #region Before ViewModel
        //// public int Id { get; set; }
        //// by default [Convention] EF will make it PK  and use identity constrain  starts from 1 and increment by 1

        ///*Note: those validation attributes should be written in the View-Model not in the Model/Entity class
        // *next session  in Sha2 Allah */

        //#region Properties
        //[Required/*(ErrorMessage = "name is required")*/] //the default error msg is=> the field name is requierd so we can ignore writing error msg
        //[MaxLength(50, ErrorMessage = "Max Length is 50 Chars")] // also have default error msg 
        //[MinLength(5, ErrorMessage = "Min Length is 5 Chars")]// won't be mapped in database 
        //public string Name { get; set; }

        //[Range(22, 60 /*, ErrorMessage = "Age Must be in Range From 22 To 60*/)]
        //public int? Age { get; set; }

        //[RegularExpression(@"^(\d{1,3}-)[a-zA-Z]{2,15}-[a-zA-Z]{4,10}-[a-zA-Z]{2,15}$", //usually we get those RE from websites or ChatGPT
        //   ErrorMessage = "Address Must Be Like 123-Street-City-Country")]
        //public string Address { get; set; }

        //[DataType(DataType.Currency)] // just to display it with dollar sign
        //public decimal Salary { get; set; } //by convention decimal (18, 2) or change it in Fluent API Configurations

        //[Display(Name = "Is Active")] //for the name to be displayed as Is Active not IsActive
        //public bool IsActive { get; set; }

        //[EmailAddress]
        //[DataType(DataType.EmailAddress)]
        //public string Email { get; set; }

        //[Phone]
        //[Display(Name = "Phone Number")]
        //[DataType(DataType.PhoneNumber)]
        //public string PhoneNumber { get; set; }
        //[Display(Name = "Hiring Date")]
        //public DateTime HireDate { get; set; }

        //public DateTime CreationDate { get; set; } = DateTime.Now;
        ///* take the date of creation with C# in the application  => DateTime.Now
        // * or use fluent API to add the date through the SQL Server by wrinting the SQL method in the Configuration class*/
        ////this piece of info not for user to know  [won't use it in View Model ]
        //public bool IsDeleted { get; set; } = false; // won't be displayed for end user 
        ///*soft delete
        // * when some one deletes a record , the recored won't be actually deleted 
        // *but, its flag will be true 
        // *and when displaying data this we'll display records with IsDeleted==false
        // *in SQL server Service deletes the records thar are  IsDeleted==true  for 2 or 3 month
        // *That what happens on mobiles or PCs =>
        // *the files are not actually deleted they are in trach /recycle bin for a period of time and can be restored in that time*/

        //public Gender Gender { get; set; }
        //public EmployeeType EmployeeType { get; set; }

        //#endregion

        //#region Foreign  Key
        ///* [ForeignKey("Department")] *///no need to use Data-annotation for FK as the name on the property is EntityID [DepartmentId]
        //public int? DepartmentId { get; set; }
        ///*when we make the FK nullable --> the on-delete role of the relationship will be restricte
        //  restrict means when you delete department that has employees --> will get error [no-action] 
        //now we need to allow null and cascade on delete --> fluent API*/
        //#endregion

        //#region Navigional Property [One]
        ///*  [InverseProperty(nameof(Models.Department.Employees))]*/ // use it if we have more than one relationship between the same Enities
        //                                                             //we write the class with the namespace as we have a property with the same name as the class 
        //public Department Department { get; set; }
        //#endregion

        #endregion
    }
}
