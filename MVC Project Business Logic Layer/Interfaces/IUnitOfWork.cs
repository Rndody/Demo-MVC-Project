using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Business_Logic_Layer.Interfaces
{
    public interface IUnitOfWork: IDisposable
    {
        public IEmployeeRepository EmployeeRepository { get; set; }
        public IDepartmentRepository DepartmentRepository { get; set; }
        ///OR 
        /// public IGenericRepository<IDepartmentRepository> DepartmentRepository { get; set; }
        ///as IDepartmentRepository don't have any special signature for the DepartmentRepository

        int Complete();// number of rows that will be affected --> SaveChanges()
    }
}
