using College2Career.DTO;
using College2Career.Models;

namespace College2Career.Repository
{
    public interface IUsersRepository
    {
        Task<Users> getUserByEmail(string email);
        Task<Roles> getRolesById(int roleId);
        Task signUpUser(Users users);
        Task updateUserPassword(Users users);
        Task<Users> getUserByToken(string forgotPasswordToken);
        Task<IEnumerable<Users>> getAllUsers(AllUsersDTO allUsersDTO);
    }
}
