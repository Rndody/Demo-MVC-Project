using MVC_Project_Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Business_Logic_Layer.Interfaces
{
    public interface IEmployeeRepository  //public as we need to use it outside BLL 
    {
        //define 5 signatures for 5 methods
        IEnumerable<Employee> GetAll();
        Employee Get(int id);
        int Add(Employee entity);
        int Update(Employee entity);
        int Delete(Employee entity);
    }
}
