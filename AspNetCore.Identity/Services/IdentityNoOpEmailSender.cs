using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace NewAuth.Services;

public class ConsoleEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        Console.WriteLine($"To: {email}, Subject: {subject}, Message: {htmlMessage}");
        return Task.CompletedTask;
    }
}

public class IdentityNoOpEmailSender(IEmailSender emailSender) : IEmailSender<ApplicationUser>
{
    public Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
    {
        emailSender.SendEmailAsync(email, "Confirm your email", $"Please confirm your email by clicking this link: {confirmationLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
    {
        emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password by clicking this link: {resetLink}");
        return Task.CompletedTask;
    }

    public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
    {
        emailSender.SendEmailAsync(email, "Reset your password", $"Please reset your password by using this code: {resetCode}");
        return Task.CompletedTask;
    }
}
