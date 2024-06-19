﻿namespace Simplifly.Interfaces
{
    public interface IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message);
    }
}
