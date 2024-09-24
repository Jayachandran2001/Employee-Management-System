using AutoMapper;
using EmployeeManagement.API.Models;
using EmployeeManagment.API.DTO;

namespace EmployeeManagment.API.Mapping
{
    public class EmployeeProfile : Profile
    {
        public EmployeeProfile()
        {
            CreateMap<EmployeeCreateDto, Employee>();
            CreateMap<EmployeeUpdateDto, Employee>();
            CreateMap<Employee, EmployeeResponseDto>()
                .ForMember(dest => dest.EmployeeImagePath, opt => opt.MapFrom(src => src.EmployeeImagePath != null ? $"{src.EmployeeImagePath}" : null));
        }
    }
}
