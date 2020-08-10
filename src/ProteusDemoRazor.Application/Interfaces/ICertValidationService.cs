using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Proteus.Application.Interfaces
{
    public interface ICertValidationService
    {
        List<Tuple<string, string>> GetCertificateInfo(X509Certificate2 x509);
    }
}
