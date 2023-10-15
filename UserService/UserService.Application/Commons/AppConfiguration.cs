namespace UserService.Application.Commons
{
    public class AppConfiguration
    {
        public string DatabaseConnection { get; set; } = default!;
        public string ClientToken { get; set; } = default!;
        public string JWTSecretKey { get;set; }=default!;
        public string ProjectId { get; set; } = default!;
        public string PrivateKeyId { get; set; } = default!;
        public string PrivateKey { get; set; } = default!;
        public string ClientEmail { get; set; } = default!;
    }
}