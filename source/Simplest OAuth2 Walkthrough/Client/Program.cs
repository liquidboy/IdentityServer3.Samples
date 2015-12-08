using IdentityModel.Client;
using System;
using System.Net.Http;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var response = GetClientToken();
            CallApi(response);

            response = GetUserToken();
            CallApi(response);
        }

        static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            Console.WriteLine(client.GetStringAsync("http://localhost:14869/test").Result);
        }

        static TokenResponse GetClientToken()
        {
            var client = new TokenClient(
                "https://lobbyidp.azurewebsites.net/core/token",
                "client",
                "secret");

            return client.RequestClientCredentialsAsync("api1").Result;
        }

        static TokenResponse GetUserToken()
        {
            var client = new TokenClient(
                "https://lobbyidp.azurewebsites.net/core/token",
                "roclient",
                "secret");

            return client.RequestResourceOwnerPasswordAsync("bob", "secret", "api1").Result;
        }
    }
}