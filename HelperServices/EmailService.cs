using System.Net;
using System.Net.Mail;


public interface IEmailService
{
    Task sendEmail(string to, string subject, string body);
    string createSignUpEmailBody(int roleId, string email);
    string createResetPasswordEmailBody(string email, string token);
    string createActivetedEmailBody(string name, int roleId);
    string createRejectedEmailBody(string name, string rejectReason, int roleId);
    string createDeactivatedEmailBody(string name, string deactivateReason, int roleId);
    public string createApplicationStatusEmailBody(string studentName, string status, string companyName, string title, string reason = null);
    string createInterviewStatusEmailBody(string studentName, string status, string companyName, string title, string dateTime, string reason = null);
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

    public string createSignUpEmailBody(int roleId, string email)
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

        string footer = "<br><p>Best Regards,<br><b>Team College2Career</b></p>";

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
        <p>Best Regards,<br><b>Team College2Career</b></p>
    ";

        return emailBody;
    }

    public string createActivetedEmailBody(string name, int roleId)
    {
        string roleLabel = roleId == 1 ? "student" : "company";

        string body = $@"
        <p>Dear {name} Team,</p>

        <p>We are pleased to inform you that your {roleLabel} profile has been successfully reviewed and approved by our admin team.</p>
        <p>You can now fully access all features and start using the platform to its fullest potential.</p>
        <p>Welcome aboard, and we look forward to your valuable contribution to our community!</p>
        <br>
        <p>Best Regards,<br><b>Team College2Career</b></p>
    ";

        return body;
    }

    public string createRejectedEmailBody(string name, string rejectReason, int roleId)
    {
        string roleLabel = roleId == 1 ? "student" : "company";

        string body = $@"
        <p>Dear {name},</p>

        <p>Thank you for submitting your {roleLabel} profile for review. After careful consideration, we regret to inform you that your profile has been rejected for the following reason:</p>
        <p>Reason for Rejection: <b>{rejectReason}</b></p>
        <p>Please review the above reason, make the necessary updates or corrections, and feel free to resubmit your profile for approval.</p>
        <p>If you have any questions or need assistance, don’t hesitate to contact our support team.</p>
        <br>
        <p>Sincerely,<br><b>Team College2Career</b></p>
    ";

        return body;
    }

    public string createDeactivatedEmailBody(string name, string deactivateReason, int roleId)
    {
        string roleLabel = roleId == 1 ? "student" : "company";

        string body = $@"
        <p>Dear {name},</p><br/><br/>

        We hope this message finds you well.<br/><br/>
        We would like to inform you that your {roleLabel} profile on <strong>College2Career</strong> has been <strong>deactivated</strong> by our administrative team due to the following reason:<br/><br/>
        🔹 <strong>Reason for Deactivation:</strong> {deactivateReason}<br/><br/>
        As a result, your account will not be able to access or perform actions on the platform until this issue is resolved.<br/><br/>
        If you believe this action was taken in error, or if you have any questions or concerns, please feel free to reach out to our support team at <a href='mailto:support@college2career.com'>support@college2career.com</a>.<br/><br/>
        We value your presence on our platform and look forward to helping you get reactivated soon.<br/><br/>
        Warm regards,<br/>
        <strong>Team College2Career</strong>
    ";
        return body;
    }

    public string createApplicationStatusEmailBody(string studentName, string status, string companyName, string title, string reason = null)
    {
        string statusMessage = "";

        switch (status.ToLower())
        {
            case "rejected":
                statusMessage = $@"
                <p>We regret to inform you that your application for the position of <strong>{title}</strong> at <strong>{companyName}</strong> has been <strong>rejected</strong>.</p>
                <p><strong>Reason:</strong> {reason}</p>";
                break;

            case "shortlisted":
                statusMessage = $@"
                <p>Good news! You have been <strong>shortlisted</strong> for the position of <strong>{title}</strong> at <strong>{companyName}</strong>.</p>
                <p>The company will reach out to you soon for the next steps.</p>";
                break;

            case "interviewscheduled":
                statusMessage = $@"
                <p>You have been <strong>scheduled for an interview</strong> for the role of <strong>{title}</strong> at <strong>{companyName}</strong>.</p>
                <p>Please check your email or dashboard for interview details.</p>";
                break;

            case "offered":
                statusMessage = $@"
                <p>Congratulations! You have received an <strong>offer</strong> for the position of <strong>{title}</strong> at <strong>{companyName}</strong>.</p>
                <p>Check your profile for more details and next steps.</p>";
                break;

            default:
                statusMessage = "<p>Status update received.</p>";
                break;
        }

        return $@"
        <p>Dear {studentName},</p>
        {statusMessage}
        <br/>
        <p>Best Regards,<br/><b>Team College2Career</b></p>";
    }

    public string createInterviewStatusEmailBody(string studentName, string status, string companyName, string title, string dateTime, string reason = null)
    {
        string statusMessage = "";

        switch (status.ToLower())
        {
            case "rescheduled":
                statusMessage = $@"
                <p>Your interview for the position of <strong>{title}</strong> at <strong>{companyName}</strong> has been <strong>rescheduled</strong>.</p>
                <p><strong>New Schedule:</strong> {dateTime}</p>
                <p><strong>Reason:</strong> {reason}</p>";
                break;

            case "cancelled":
                statusMessage = $@"
                <p>We regret to inform you that your interview for the position of <strong>{title}</strong> at <strong>{companyName}</strong> has been <strong>cancelled</strong>.</p>
                <p><strong>Reason:</strong> {reason}</p>";
                break;

            default:
                statusMessage = "<p>Interview update received.</p>";
                break;
        }

        return $@"
    <p>Dear {studentName},</p>
    {statusMessage}
    <br/>
    <p>Best Regards,<br/><b>Team College2Career</b></p>";
    }

}
