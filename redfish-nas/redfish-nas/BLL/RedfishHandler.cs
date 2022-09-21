using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace redfish_nas.BLL
{
    internal class RedfishHandler : IRedfishHandler
    {
        public void LogIn()
        {
            using var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) =>
            {
                if (sslPolicyErrors == System.Net.Security.SslPolicyErrors.None)
                {
                    return true;
                }

                if (cert.GetCertHashString().ToLower() == "5598bab7f83ff5a9e2f9bb4e518d8848f2ceeefc")
                {
                    return true;
                }

                return false;
            };

            var baseUrl = "https://192.168.1.10";

            using var client = new HttpClient(httpClientHandler);
            var logInRequestBody = new LogInRequestBody("ADMIN", "TOVNWONXNE");
            var json = JsonConvert.SerializeObject(logInRequestBody);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");

            var login = client.PostAsync($"{baseUrl}/redfish/v1/SessionService/Sessions/",
                requestBody).GetAwaiter().GetResult();

            var tokenLocation = login.Headers.Location;
            login.Headers.TryGetValues("X-Auth-Token", out var token);

            var getSession = client.GetAsync($"{baseUrl}{tokenLocation}").GetAwaiter().GetResult();

            var moo = "moo";
        }
    }

    public class LogInRequestBody
    {
        public string UserName { get; }
        public string Password { get; }

        public LogInRequestBody(string username, string password)
        {
            UserName = username;
            Password = password;
        }
    }
}
