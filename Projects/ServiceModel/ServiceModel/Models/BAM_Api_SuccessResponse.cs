namespace ServiceModel.Models
{
    public class BAM_Api_SuccessResponse
    {
        public string Message { get; set; }
        public bool Success { get; set; }
        public string Data { get; set; }
        public string Exception { get; set; }
        public string BaseId { get; set; }
    }
}
