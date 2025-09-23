using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WorkingWomenApp.BLL.Interfaces;
using WorkingWomenApp.Database.Models.config;

namespace WorkingWomenApp.BLL.Implementation
{
    public class Emailservice: IEmailService
    {
        public EmailSettings _emailSettings { get; }

    public Emailservice(IOptions<EmailSettings> emailSettings)
    {
        _emailSettings = emailSettings.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string message)
    {
        await Execute(email, subject, message, null);

        
    }

    public async Task SendEmailWithAttachmentsAsync(string email, string subject, string message, List<Attachment> attachments)
    {
        await Execute(email, subject, message, attachments);

        
    }

    public async Task Execute(string email, string subject, string message, List<Attachment> attachments)
    {
        try
        {
            var toEmail = string.IsNullOrEmpty(email) ? _emailSettings.ToEmail : email;

            MailMessage mail = new MailMessage()
            {
                From = new MailAddress(_emailSettings.FromAddress, _emailSettings.FromName)
            };

            mail.To.Add(new MailAddress(toEmail));

            if (!string.IsNullOrEmpty(_emailSettings.CcEmail))
                mail.CC.Add(new MailAddress(_emailSettings.CcEmail));

            if (!string.IsNullOrEmpty(_emailSettings.BccEmail))
                mail.Bcc.Add(new MailAddress(_emailSettings.BccEmail));

            if (attachments != null)
            {
                foreach (var item in attachments)
                {
                    mail.Attachments.Add(item);
                }
            }

            mail.Subject = subject;
            mail.Body = message;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.Normal;

            using (SmtpClient smtp = new SmtpClient(_emailSettings.ServerAddress, _emailSettings.ServerPort))
            {
                smtp.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);
                smtp.EnableSsl = _emailSettings.ServerUseSsl;

                await smtp.SendMailAsync(mail);
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
} }

