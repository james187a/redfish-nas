using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redfish_nas.BLL
{
    public class UpdateService : IUpdateService
    {
        public void SslCert(HttpClient httpClient)
        {
            var logInRequestBody = new LogInRequestBody("ADMIN", "TOVNWONXNE");
            var json = JsonConvert.SerializeObject(logInRequestBody);
            var requestBody = new StringContent(json, Encoding.UTF8, "application/json");

            var login = httpClient.PostAsync("SessionService/Sessions/", requestBody).GetAwaiter().GetResult();
            login.Headers.TryGetValues("X-Auth-Token", out var token);

            var getSslCertInfo = httpClient.GetAsync("UpdateService/SSLCert/").GetAwaiter().GetResult();
        }

        public void SslCertUpload()
        {
            throw new NotImplementedException();
        }
    }
}
