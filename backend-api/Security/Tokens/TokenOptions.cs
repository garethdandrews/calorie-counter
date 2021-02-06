namespace backend_api.Security.Tokens
{
	/// <summary>
	/// The options are loaded into this class from the appsettings by dependency injection
	/// </summary>
    public class TokenOptions
	{
		public string Audience { get; set; }
		public string Issuer { get; set; }
		public long AccessTokenExpiration { get; set; }
		public long RefreshTokenExpiration { get; set; }
		public string Secret { get; set; }
	}
}