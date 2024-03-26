using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Project_Data_Access_Layer.Models
{
    public abstract class ModelBase //make it public
    { 
        ///only have one Property => Id
        public int Id { get; set; }

    }
}
