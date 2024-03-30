using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Business_Logic_Layer.Repositories;
using MVC_Project_Data_Access_Layer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Business_Logic_Layer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;

        public IEmployeeRepository EmployeeRepository { get; set; } = null;
        public IDepartmentRepository DepartmentRepository { get; set; } = null;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;

            EmployeeRepository = new EmployeeRepository(dbContext);
            DepartmentRepository = new DepartmentRepository(dbContext);
            /*we are creating the objects here not the CLR who create them ---> those objects depends on creating objects from ApplicationDbContext
            we don't ask CLR to create it as we won't ask for that object outside this class
            [we used to ask CLR to create object as we may need to use the same object so CLR sends it to me as it keeps its address]*/

            /*now ask CLR to create the DbContext object for me in the constructor --> ctrl + .  create and assign field
             then send the dbContxt object to the objects we create */
        }
        public int Complete()        
        =>    dbContext.SaveChanges();

        public void Dispose()
            => dbContext.Dispose(); //to close the connection
        
    }
}
