using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redfish_nas.BLL
{
    public interface IUpdateService
    {
        public void SslCert(HttpClient httpClient);
        void SslCertUpload();
    }
}
