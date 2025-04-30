using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;

namespace College2Career.Service
{
    public interface IUsersService
    {
        Task<ServiceResponse<string>> signUpUsers(UsersDTO usersDTO);
        Task<ServiceResponse<string>> signInUsers(UsersAuthDTO usersAuthDTO);
        Task<ServiceResponse<string>> findUserByEmail(ForgotValidEmail forgotValidEmail);
        Task<ServiceResponse<string>> forgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<ServiceResponse<IEnumerable<Users>>> getAllUsers(AllUsersDTO allUsersDTO);
    }
}
