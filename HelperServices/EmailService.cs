using System.Net;
using System.Net.Mail;


public interface IEmailService
{
    Task sendEmail(string to, string subject, string body);
    string createEmailBody(int roleId, string email);
    string createResetPasswordEmailBody(string email, string token);
}

public class EmailService : IEmailService
{
    private readonly string fromMail = "adipathak7488@gmail.com";
    private readonly string fromPassword = "ceix yofx wlwy cnfr";

    public EmailService(IConfiguration configuration)
    {
        
    }
    public async Task sendEmail(string to, string subject, string body)
    {
        try
        {
            var client = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(fromMail, fromPassword)
            };

            var mailMessage = new MailMessage(fromMail, to, subject, body)
            {
                IsBodyHtml = true
            };

            await client.SendMailAsync(mailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending email: {ex.Message}");
            throw;
        }
    }

    public string createEmailBody(int roleId, string email)
    {
        string greeting = $"<h2>Welcome to College2Career, {email}!</h2>";

        string studentBody = @"
            <p>We’re excited to have you on board as a student.</p>
            <p>You can now explore various career opportunities and apply for internships & jobs.</p>
            <p>Start your journey today!</p>";

        string companyBody = @"
            <p>We’re happy to welcome you as a company.</p>
            <p>You can now post job opportunities and connect with talented students.</p>
            <p>Start hiring the best candidates today!</p>";

        string footer = "<br><p>Best Regards,<br><b>College2Career Team</b></p>";

        switch (roleId)
        {
            case 1:
                return greeting + studentBody + footer;
            case 2:
                return greeting + companyBody + footer;
            default:
                return greeting + "<p>Welcome to the platform!</p>" + footer;
        }
    }

    public string createResetPasswordEmailBody(string email, string token)
    {
        string resetLink = $"http://localhost:5173/reset-password?token={token}";

        string emailBody = $@"
        <h2>Password Reset Request</h2>
        <p>Dear {email},</p>
        <p>We received a request to reset your password. Click the button below to reset your password:</p>
        <a href='{resetLink}' style='background-color: #007bff; color: white; padding: 10px 20px; 
        text-decoration: none; border-radius: 5px;'>Reset Password</a>
        <p>If you didn’t request this, please ignore this email.</p>
        <p><b>Note:</b> This link will expire in 30 minutes.</p>
        <br>
        <p>Best Regards,<br><b>College2Career Team</b></p>
    ";

        return emailBody;
    }


}
