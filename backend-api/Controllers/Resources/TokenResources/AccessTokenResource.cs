namespace backend_api.Controllers.Resources.TokenResources
{
    public class AccessTokenResource
    {
        public string Name { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public long Expiration { get; set; }
    }
}