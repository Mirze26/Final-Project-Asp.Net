using Microsoft.Extensions.Options;
using Payne.Helpers.AccountSetting;
using System.Net.Mail;

namespace Payne.Services.Interfaces
{
    public interface IEmailService
    {
        void Send(string to, string subject, string html, string from = null);
    }
}
