using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Proteus.Application.Services
{
    public class CertValidationService
    {
        //TODO CAC Authentication 2b: add certificate validation
        public bool ValidateCertificate(X509Certificate2 clientCertificate, IWebHostEnvironment env)
        {
            //here would be where we check against the thumbprint in the database
            ;
            var cert = new X509Certificate2(Path.Combine(env.ContentRootPath, "root_localhost.pfx"), "1234");
            if (clientCertificate.Thumbprint == cert.Thumbprint)
            {
                return true;
            }

            return false;
        }

    }
}
