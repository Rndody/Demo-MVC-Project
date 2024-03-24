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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(Employee entity)
        {
            dbContext.Employees.Add(entity);
            return dbContext.SaveChanges();
        }

        public int Delete(Employee entity)
        {
            dbContext.Employees.Remove(entity);
            return dbContext.SaveChanges();
        }

        public Employee Get(int id)
       => dbContext.Employees.Find(id);
        // var Employee = dbContext.Employees.Where(E => E.Id == id).FirstOrDefault();        

        public IEnumerable<Employee> GetAll()
        => dbContext.Employees.AsNoTracking().ToList();


        public int Update(Employee entity)
        {
            dbContext.Employees.Update(entity);
            return dbContext.SaveChanges();
        }
    }
}
