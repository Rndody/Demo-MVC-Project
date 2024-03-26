using Microsoft.EntityFrameworkCore;
using MVC_Project_Business_Logic_Layer.Interfaces;
using MVC_Project_Data_Access_Layer.Data;
using MVC_Project_Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Business_Logic_Layer.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>,  IDepartmentRepository
    {
        //all methods are object members
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            
        }

        #region Deleted Code
        // private  readonly ApplicationDbContext dbContext;


        // public DepartmentRepository( ApplicationDbContext dbContext)
        // {
        //    this. dbContext = dbContext;            
        // }

        // public int Add(Department entity)
        // {
        //     dbContext.Departments.Add(entity);
        //     return dbContext.SaveChanges();
        // }

        // public int Delete(Department entity)
        // {
        //     dbContext.Departments.Remove(entity);
        //     return dbContext.SaveChanges();
        // }

        // public Department Get(int id)
        //=> dbContext.Departments.Find(id);
        // // var department = dbContext.Departments.Where(D => D.Id == id).FirstOrDefault();        

        // public IEnumerable<Department> GetAll()
        // => dbContext.Departments.AsNoTracking().ToList();


        // public int Update(Department entity)
        // {
        //     dbContext.Departments.Update(entity);
        //     return dbContext.SaveChanges();
        // } 
        #endregion
    }
}
