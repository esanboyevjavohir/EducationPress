﻿namespace EduPress.Application.Services.Interface
{
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string email, string subject);
    }
}
