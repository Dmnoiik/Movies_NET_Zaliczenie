﻿using Microsoft.AspNetCore.Identity.UI.Services;

public class FakeEmailSender : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        // Symulacja wysyłania e-maila
        Console.WriteLine($"Email sent to: {email}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Message: {htmlMessage}");
        return Task.CompletedTask;
    }
}
