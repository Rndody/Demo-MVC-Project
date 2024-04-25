using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Business_Logic_Layer.Repositories;
using MVC_Project_Data_Access_Layer.Data;
using MVC_Project_Data_Access_Layer.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Business_Logic_Layer
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext dbContext;
        private Hashtable repositories;
        #region wrong implemntation
        //public IEmployeeRepository EmployeeRepository { get; set; } = null;
        //public IDepartmentRepository DepartmentRepository { get; set; } = null; 
        #endregion
        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            repositories = new Hashtable();
            #region wrong implemntation 
            ///  EmployeeRepository = new EmployeeRepository(dbContext);
            //  DepartmentRepository = new DepartmentRepository(dbContext);
            /*we are creating the objects here not the CLR who create them ---> those objects depends on creating objects from ApplicationDbContext
            we don't ask CLR to create it as we won't ask for that object outside this class
            [we used to ask CLR to create object as we may need to use the same object so CLR sends it to me as it keeps its address]*/

            /*now ask CLR to create the DbContext object for me in the constructor --> ctrl + .  create and assign field
             then send the dbContxt object to the objects we create */
            #endregion
        }
        public IGenericRepository<T> Repository<T>() where T : ModelBase
        {
            var key = typeof(T).Name;
            #region temporary till we discuss specificatio design pattern
            if (!repositories.ContainsKey(key))
            //if doesn't contain the key create object as if the key is included means we already have object and no need to create new one
            {
                if (key == nameof(Employee))
                {
                    var repo = new EmployeeRepository(dbContext);
                    repositories.Add(key, repo);
                }
                else
                {
                    var repo = new GenericRepository<T>(dbContext);
                    repositories.Add(key, repo);
                }
            }
            #endregion
            //  return new GenericRepository<T>(dbContext) as IGenericRepository<T>;
            return repositories[key] as IGenericRepository<T>;
        }
        public async Task<int> Complete() => await dbContext.SaveChangesAsync();
        public async ValueTask DisposeAsync() => await dbContext.DisposeAsync(); //to close the connection


    }
}
