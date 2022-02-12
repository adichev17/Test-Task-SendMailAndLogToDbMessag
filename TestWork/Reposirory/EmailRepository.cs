using MailKit.Net.Smtp;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using TestWork.DBContexts;
using TestWork.Models;

namespace TestWork.Reposirory
{
    /// <summary>
    /// Class for managing the context of a message database
    /// </summary>
    public class EmailRepository : IEmailRepository
    {
        public readonly ApplicationDbContext _context;
        public EmailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds EmailLog entity to the database
        /// </summary>
        /// <param name="logs">The EmailLog entity</param>
        /// <returns></returns>
        public async Task AddLog(List<EmailLog> logs)
        {
            //await _context.EmailLogs.AddAsync(logs);
            try
            {
                await _context.EmailLogs.AddRangeAsync(logs);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }

        /// <summary>
        /// Generates a list of all messages from the database
        /// </summary>
        /// <returns> List<EmailLog> list of all objects from database</returns>
        public async Task<IEnumerable<EmailLog>> GetAllLogs()
        {
            return await _context.EmailLogs.ToListAsync();
        }
    }
}
