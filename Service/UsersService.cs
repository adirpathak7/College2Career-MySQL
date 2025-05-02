using College2Career.DTO;
using College2Career.HelperServices;
using College2Career.Models;
using College2Career.Repository;

namespace College2Career.Service
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IJWTService jWTService;
        private readonly IEmailService emailService;
        public UsersService(IUsersRepository usersRepository, IJWTService jWTService, IEmailService emailService)
        {
            this.usersRepository = usersRepository;
            this.jWTService = jWTService;
            this.emailService = emailService;
        }

        public async Task<ServiceResponse<string>> signUpUsers(UsersDTO usersDTO)
        {
            var response = new ServiceResponse<string>();


            var existUser = await usersRepository.getUserByEmail(usersDTO.email);
            if (existUser != null)
            {
                response.data = "0";
                response.message = "Email already exists.";
                response.status = false;
                return response;
            }

            var roleId = await usersRepository.getRolesById(usersDTO.roleId);

            if (roleId == null)
            {
                response.data = "0";
                response.message = "Invalid Role ID.";
                response.status = false;
                return response;
            }

            var user = new Users
            {
                email = usersDTO.email,
                password = BCrypt.Net.BCrypt.HashPassword(usersDTO.password),
                roleId = usersDTO.roleId,
                createdAt = DateTime.Now,
            };

            await usersRepository.signUpUser(user);

            response.data = "1";
            response.message = "User registered successfully.";
            response.status = true;

            string subject = "Welcome to College2Career!";
            string body = emailService.createSignUpEmailBody(usersDTO.roleId, usersDTO.email);

            await emailService.sendEmail(usersDTO.email, subject, body);

            return response;
        }

        public async Task<ServiceResponse<string>> signInUsers(UsersAuthDTO usersAuthDTO)
        {
            var response = new ServiceResponse<string>();

            //if (string.IsNullOrEmpty(usersAuthDTO.email) || string.IsNullOrEmpty(usersAuthDTO.password))
            //{
            //    response.data = "0";
            //    response.message = "Email or Password can't be empty";
            //    response.status = false;
            //    return response;
            //}

            var existUser = await usersRepository.getUserByEmail(usersAuthDTO.email);

            if (existUser == null || !BCrypt.Net.BCrypt.Verify(usersAuthDTO.password, existUser.password))
            {
                response.data = "0";
                response.message = "Invalid email or password.";
                response.status = false;
                return response;
            }

            response.data = jWTService.generateToken(existUser);
            response.message = "Login successfully";
            response.status = true;
            return response;
        }

        public async Task<ServiceResponse<string>> findUserByEmail(ForgotValidEmail forgotValidEmail)
        {
            var response = new ServiceResponse<string>();
            var existUser = await usersRepository.getUserByEmail(forgotValidEmail.email);
            if (existUser == null)
            {
                response.data = "0";
                response.message = "Email not found.";
                response.status = false;
                return response;
            }

            string token = Guid.NewGuid().ToString();
            DateTime tokenExpirationTime = DateTime.Now.AddMinutes(10);

            string hashToken = BCrypt.Net.BCrypt.HashPassword(token);

            existUser.forgotPasswordToken = hashToken;
            existUser.tokenExpirationTime = tokenExpirationTime;

            await usersRepository.updateUserPassword(existUser);

            response.data = "1";
            response.message = "Forgot password link sent to your email.";
            response.status = true;

            string subject = "Forgot Your Password - College2Career";
            string body = emailService.createResetPasswordEmailBody(forgotValidEmail.email, existUser.forgotPasswordToken);

            await emailService.sendEmail(forgotValidEmail.email, subject, body);

            return response;
        }

        public async Task<ServiceResponse<string>> forgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var response = new ServiceResponse<string>();

            var existUser = await usersRepository.getUserByToken(forgotPasswordDTO.forgotPasswordToken);
            if (existUser == null)
            {
                response.data = "0";
                response.message = "Invalid token.";
                response.status = false;
                return response;
            }

            if (DateTime.Now > existUser.tokenExpirationTime)
            {
                response.data = "0";
                response.message = "Token has expired.";
                response.status = false;
                return response;
            }

            if (forgotPasswordDTO.forgotPasswordToken != existUser.forgotPasswordToken)
            {
                response.data = "0";
                response.message = "Invalid token.";
                response.status = false;
                return response;
            }

            existUser.password = BCrypt.Net.BCrypt.HashPassword(forgotPasswordDTO.forgotPassword);
            await usersRepository.updateUserPassword(existUser);

            response.data = "1";
            response.message = "Password reset successfully.";
            response.status = true;

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<Users>>> getAllUsers(AllUsersDTO allUsersDTO)
        {
            var response = new ServiceResponse<IEnumerable<Users>>();


            var allUsers = await usersRepository.getAllUsers(allUsersDTO);

            if (allUsers == null || !allUsers.Any())
            {
                response.data = null;
                response.message = "No users found!";
                response.status = false;
                return response;
            }

            response.data = allUsers;
            response.message = "Users fetched successfully.";
            response.status = true;

            return response;
        }
    }
}
