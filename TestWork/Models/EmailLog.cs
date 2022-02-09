using System.ComponentModel.DataAnnotations;

namespace TestWork.Models
{
    /// <summary>
    /// EmailLog Class
    /// </summary>
    public class EmailLog
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string Recipients { get; set; }
        public string FailedMessage { get; set; }
        public DateTime EmailSent { get; set; }
        public string Result { get; set; }
    }
}
