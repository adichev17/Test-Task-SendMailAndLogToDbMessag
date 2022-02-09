using Microsoft.EntityFrameworkCore;
using TestWork.Models;

namespace TestWork.DBContexts
{
    /// <summary>
    /// database contexts
    /// </summary>
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }
        /// <summary>
        /// EmailLogs Database table
        /// </summary>
        public DbSet<EmailLog> EmailLogs { get; set; }
    }
}
