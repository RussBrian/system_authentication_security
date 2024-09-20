using authentication_security.Core.Application.Dtos;
using authentication_security.Core.Domain.Settings;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;
using authentication_security.Core.Application.Interfaces;
using RazorLight;

namespace authentication_security.Infra.Shared.Services;

public class EmailService : IEmailService
{
    public EmailSettings EmailSettings { get; }
    private readonly RazorLightEngine _razorEngine;

    public EmailService(IOptions<EmailSettings> emailSettings)
    {
        EmailSettings = emailSettings.Value;

        _razorEngine = new RazorLightEngineBuilder()
            .UseFileSystemProject(Path.Combine(Directory.GetCurrentDirectory(), "Templates/EmailVerification"))
            .UseMemoryCachingProvider()
            .Build();
    }

    public async Task SendAsync(EmailRequest emailRequest, string? templateName = null, object? templateModel = null)
    {
        try
        {
            MailAddress fromAddress = new(emailRequest.From ?? EmailSettings.EmailFrom);
            MailAddress toAddress = new(emailRequest.To);

            using (SmtpClient smtp = new(EmailSettings.SmtpHost, EmailSettings.SmtpPort))
            {
                smtp.Credentials = new NetworkCredential(EmailSettings.SmtpUser, EmailSettings.SmtpPass);
                smtp.EnableSsl = true;

                string body = templateName != null && templateModel != null
                      ? await GenerateEmailBodyAsync(templateName, templateModel)
                      : emailRequest.Body;

                MailMessage mail = new(fromAddress, toAddress)
                {
                    Subject = emailRequest.Subject,
                    Body = body,
                    IsBodyHtml = true
                };

                await smtp.SendMailAsync(mail);
            }
        }
        catch (AggregateException ex)
        {
            throw new AggregateException("Error tratando de enviar el email: " + ex.Message);
        }
    }

    private async Task<string> GenerateEmailBodyAsync<T>(string templateName, T model)
    {
        string templatePath = $"{templateName}.cshtml";
        return await _razorEngine.CompileRenderAsync(templatePath, model);
    }

}
