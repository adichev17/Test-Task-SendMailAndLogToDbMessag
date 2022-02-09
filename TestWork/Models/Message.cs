namespace TestWork.Models
{
    /// <summary>
    /// Post request body class. Message class
    /// </summary>
    public class Message
    {
        public string Subject { get; set; }
        public string Body { get; set; }
        public IEnumerable<string> Recipients { get; set; }
    }
}
