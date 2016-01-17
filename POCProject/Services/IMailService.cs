using System;

namespace POCProject.Services
{
    public interface IMailService
    {
        bool sendMail(string to, string from, string subject, string body);
    }
}

