namespace EBP.API.Models
{
    public class ErrorDetailsResponse
    {
        public string Type { get; set; } = null!;
        public int Code { get; set; }
        public string Message { get; set; } = null!;
    }
}
