using College2Career.Data;
using College2Career.DTO;
using College2Career.Models;
using Microsoft.EntityFrameworkCore;

namespace College2Career.Repository
{
    public class UsersRepository : IUsersRepository
    {
        private readonly C2CDBContext c2CDBContext;

        public UsersRepository(C2CDBContext c2CDBContext)
        {
            this.c2CDBContext = c2CDBContext;
        }

        public async Task<Roles> getRolesById(int roleId)
        {
            return await c2CDBContext.Roles.FirstOrDefaultAsync(r => r.roleId == roleId);
        }

        public async Task<Users> getUserByEmail(string email)
        {
            return await c2CDBContext.Users.FirstOrDefaultAsync(u => u.email == email);
        }

        public async Task signUpUser(Users users)
        {
            try
            {
                await c2CDBContext.Users.AddAsync(users);
                await c2CDBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR occured in users repository in signUp method: " + ex.Message);
            }
        }

        public async Task updateUserPassword(Users users)
        {
            c2CDBContext.Users.Update(users);
            await c2CDBContext.SaveChangesAsync();
        }
        public async Task<Users> getUserByToken(string forgotPasswordToken)
        {
            return await c2CDBContext.Users.FirstOrDefaultAsync(u => u.forgotPasswordToken == forgotPasswordToken);
        }

        public async Task<IEnumerable<Users>> getAllUsers(AllUsersDTO allUsersDTO)
        {
            return await c2CDBContext.Users.Select(u => new Users
            {
                usersId = u.usersId,
                email = u.email,
                roleId = u.roleId
            }).ToListAsync();
        }

    }
}
