namespace ServiceModel.Models
{
    public class AuthorizationModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LanguageCode { get; set; }
        public string AuthToken { get; set; }
    }
}
