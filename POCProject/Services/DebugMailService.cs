using System.Diagnostics;

namespace POCProject.Services
{

    public class DebugMailService : IMailService
    {
        public bool sendMail(string to, string from, string subject, string body)
        {
            Debug.WriteLine($"Sending Email: To {to}, Subject: {subject}");
            return true;
        }
    }

}

