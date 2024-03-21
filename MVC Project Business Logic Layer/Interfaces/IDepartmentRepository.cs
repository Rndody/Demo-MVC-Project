using MVC_Project_Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Business_Logic_Layer.Interfaces
{
    internal interface IDepartmentRepository
    {
        //define 5 signatures for 5 methods
        IEnumerable<Department> GetAll();
        Department Get(int id);
        int Add(Department entity);
        int Update(Department entity);
        int Delete(Department entity);


    }
}
