namespace TestWork.Models
{
    /// <summary>
    /// Response model Class
    /// </summary>
    public class Response
    {
        public bool IsSuccess { get; set; } = true;
        public object Result { get; set; }
        public string DisplayMessage { get; set; } = "";
        public List<string> ErrorMessages { get; set; }
    }
}
