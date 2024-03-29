using MVC_Project_Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Business_Logic_Layer.Interfaces
{

    public interface IEmployeeRepository : IGenericRepository<Employee>   //public as we need to use it outside BLL 
    {
        /*IEnumerable*/  IQueryable<Employee> GetEmployeesByAddress(string address);
        /*Note: not to use IEnumerable
         * when implementing the method => write LINQ Query  [employees with that address]
         * which will lead to retriving all data and filtering in the application not in the database
         * Why?   =>  the LINQ Query run against Sequence whci may be Local/Remote
         * in case of Local Sequence the LINQ won't be converted to SQL whcih means the Where will work as normal C# function
         * that's what happen when using the IEnumerable
         * instead use IQueryble**/

        IQueryable<Employee> SearchByName(string name);
        //specif method for Employees
        #region Deleted Code
        //define 5 signatures for 5 methods
        //IEnumerable<Employee> GetAll();
        //Employee Get(int id);
        //int Add(Employee entity);
        //int Update(Employee entity);
        //int Delete(Employee entity); 
        #endregion
    }
   
}
