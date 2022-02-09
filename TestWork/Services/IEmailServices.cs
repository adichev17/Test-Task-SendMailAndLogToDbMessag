using MimeKit;
using TestWork.Models;

namespace TestWork.Services
{
    /// <summary>
    /// Interface for implementing send message methods services 
    /// </summary>
    public interface IEmailServices
    {
        Task SendAndLogEmail(Message message);
    }
}
