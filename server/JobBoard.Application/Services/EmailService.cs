﻿using System.Net.Mail;
using System.Net;
using JobBoard.Core.Interfaces;
using Microsoft.Extensions.Configuration;

namespace JobBoard.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var smtpClient = new SmtpClient
        {
            Host = _configuration["Smtp:Host"],
            Port = int.Parse(_configuration["Smtp:Port"]),
            EnableSsl = true,
            Credentials = new NetworkCredential(
                _configuration["Smtp:Username"],
                _configuration["Smtp:Password"])
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(_configuration["Smtp:FromEmail"]),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        mailMessage.To.Add(to);

        await smtpClient.SendMailAsync(mailMessage);
    }
}