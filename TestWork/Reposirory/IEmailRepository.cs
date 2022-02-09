using TestWork.Models;

namespace TestWork.Reposirory
{
    /// <summary>
    /// Interface for implementing database context management
    /// </summary>
    public interface IEmailRepository
    {
        Task AddLog(EmailLog message);
        Task<IEnumerable<EmailLog>> GetAllLogs();
    }
}
