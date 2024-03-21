using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Data_Access_Layer.Models
{
    public class Department
    {
        public int Id { get; set; }// for us as developes won't show for end user

        public string Code { get; set; } //  end user can deal with it 

        public string Name { get; set; }

        public DateTime DateOfCreation { get; set; }




        #region for discussion

        /*  public int Id { get; set; }// for us as developes won't show for end user
          [Required(ErrorMessage = "this is requierd")] //custom msg for user =>this msg for the application only not for database 
          //so we shouldn't put it in this model as it represents the table in database
          //we should put it in the Departmet View Model 
          public string Code { get; set; } //  end user can deal with it 
          [Required] // not null constrain in the database 
          public string Name { get; set; }

          public DateTime DateOfCreation { get; set; }*/
        #endregion
    }
}
