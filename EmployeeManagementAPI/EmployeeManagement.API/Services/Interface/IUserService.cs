using EmployeeManagment.API.DTO;
using System.Threading.Tasks;

public interface IUserService
{
    Task<string> LoginAsync(LoginUserDTO loginUserDto);
    Task<string> RegisterAsync(RegisterUserDTO registerUserDto);
}