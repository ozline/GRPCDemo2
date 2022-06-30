namespace CXBIM.Core.IdentityService;

public class IdentityServiceOptions
{
    public string AuthServiceName { get; set; }

    public class ApiScopes
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
    }

    public class ApiResources
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Scrops { get; set; }
    }

    public class Client
    {
        public string Enabled { get; set; }
        public string ClientID { get; set; }
        public string ClientName { get; set; }
        public string ClientSecrets { get; set; }
        public string AllowedGrantTypes { get; set; }
        public string AllowedScopes { get; set; }
        public string AccessTokenLifetime { get; set; }
    }
}