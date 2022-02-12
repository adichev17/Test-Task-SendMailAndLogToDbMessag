using System.ComponentModel.DataAnnotations;

namespace TestWork.Models
{
    /// <summary>
    /// Post request body class. Message class
    /// </summary>
    public class Message
    {
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Body { get; set; }
        [Required]
        public IEnumerable<string> Recipients { get; set; }
    }
}
