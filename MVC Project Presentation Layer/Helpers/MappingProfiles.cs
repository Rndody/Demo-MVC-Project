using AutoMapper;
using MVC_Project_Data_Access_Layer.Models;
using MVC_Project_Presentation_Layer.ViewModels;

namespace MVC_Project_Presentation_Layer.Helpers
{
    public class MappingProfiles : Profile
    {
        ///Constructor --> when create object from MappingProfiles use the function in the constructor
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap(); //method inherited from Profile class

            /*CreateMap<EmployeeViewModel, Employee>()
            //    .ForMember(D => D.Name, O => O.MapFrom(S => S.EmpName)); //in case we changed a name of the property
            //the D=> is Employee[destination I'll go to ] ,  S=>is  EmployeeViewModel [source I take data from ]*/


            CreateMap<DepartmentViewModel, Department>().ReverseMap(); //method inherited from Profile class

        }
    }
}
