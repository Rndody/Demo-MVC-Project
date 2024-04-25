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

        public void Add(T entity)
        {
            // dbContext.Set<T>().Add(entity);
            dbContext.Add(entity);
            // return dbContext.SaveChanges();
        }
        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
            // dbContext.Update(entity);//EF core 3.1 new feature
            //return dbContext.SaveChanges();
        }

        public void Delete(T entity)
        {
            //dbContext.Set<T>().Remove(entity);
            dbContext.Remove(entity);
            //  return dbContext.SaveChanges();
        }

        public async Task<T> GetAsync(int id)
       => await dbContext.FindAsync<T>(id);
        // var Employee = dbContext.Employees.Where(E => E.Id == id).FirstOrDefault();        

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T) == typeof(Employee))
                return (IEnumerable<T>)await dbContext.Employees.Include(E => E.Department).AsNoTracking().ToListAsync();
            // return (IEnumerable<T>) await dbContext.Set<Employee>().Include(E => E.Department).AsNoTracking().ToListAsync();
            //note: casting to IEnumerable<T> from  IEnumerable<Employee>

            return await dbContext.Set<T>().AsNoTracking().ToListAsync();
        }
    }
}
