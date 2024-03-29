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
    public class GenericRepository<T> : IGenericRepository<T> where T : ModelBase
    {
        private protected readonly ApplicationDbContext dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Add(T entity)
        {
            // dbContext.Set<T>().Add(entity);
            dbContext.Add(entity);
            return dbContext.SaveChanges();
        }
        public int Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            // dbContext.Update(entity);//EF core 3.1 new feature
            return dbContext.SaveChanges();
        }

        public int Delete(T entity)
        {
            //dbContext.Set<T>().Remove(entity);
            dbContext.Remove(entity);
            return dbContext.SaveChanges();
        }

        public T Get(int id)
       => dbContext.Find<T>(id);
        // var Employee = dbContext.Employees.Where(E => E.Id == id).FirstOrDefault();        

        public IEnumerable<T> GetAll()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>)  dbContext.Employees.Include(E => E.Department).AsNoTracking();
            //note: casting to IEnumerable<T> from  IEnumerable<Employee>
            else
                return dbContext.Set<T>().AsNoTracking().ToList();
        }
    }
}
