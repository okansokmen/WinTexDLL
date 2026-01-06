using System.Net.Http.Headers;

namespace YKKEntegrasyon
{
    public partial class Client
    {
        public string Username { get; set; }
        public string Password { get; set; }

        partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request,
            string url)
        {
            if (request.RequestUri.AbsolutePath.Contains("Authentication")) return;
            var token = this.LoginAsync(new LoginModel
            {
                Password = this.Password,
                Username = this.Username
            }).Result;
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
