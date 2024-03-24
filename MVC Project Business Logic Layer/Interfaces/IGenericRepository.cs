using MVC_Project_Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Business_Logic_Layer.Interfaces
{
    public interface IGenericRepository <T>  where T : ModelBase   //public => use it outside BLL
    {
        //define 5 signatures for 5 methods
        IEnumerable<T> GetAll();
        T Get(int id);
        int Add(T entity);
        int Update(T entity);
        int Delete(T entity);
    }
}
