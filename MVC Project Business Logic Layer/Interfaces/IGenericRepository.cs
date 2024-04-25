using MVC_Project_Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Business_Logic_Layer.Interfaces
{
    public interface IGenericRepository<T> where T : ModelBase   //public => use it outside BLL
    {
        //define 5 signatures for 5 methods
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
