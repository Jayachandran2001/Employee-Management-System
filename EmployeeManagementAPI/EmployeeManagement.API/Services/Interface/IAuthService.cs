using EmployeeManagment.API.DTO;
using System.Threading.Tasks;

namespace EmployeeManagment.API.Services.Interface
{
    public interface IAuthService
    {
        Task<bool> SignupAsync(SignupDTO signupDto);
        Task<AuthResponseDTO> LoginAsync(LoginDTO loginDto);
    }
}
