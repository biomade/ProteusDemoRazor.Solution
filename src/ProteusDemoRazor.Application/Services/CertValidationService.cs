using Microsoft.AspNetCore.Hosting;
using Proteus.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Proteus.Application.Services
{
    public class CertValidationService:ICertValidationService
    {
        ////TODO CAC Authentication 2b: add certificate validation
        //public bool ValidateCertificate(X509Certificate2 clientCertificate, IWebHostEnvironment env)
        //{
        //    //here would be where we check against the thumbprint in the database
        //    ;
        //    var cert = new X509Certificate2(Path.Combine(env.ContentRootPath, "root_localhost.pfx"), "1234");
        //    if (clientCertificate.Thumbprint == cert.Thumbprint)
        //    {
        //        return true;
        //    }

        //    return false;
        //}

        #region CAC


        public List<Tuple<string, string>> GetCertificateInfo(X509Certificate2 x509)
        {
            //now go and find user
            List<Tuple<string, string>> certInfo = new List<Tuple<string, string>>();
            //LJK CAN WE GRAB THE CERTIFICATE AND LOG IN  WITH IT
            string userCert = x509.Subject;
            
            Tuple<string, string> Subject = Tuple.Create<string, string>("Subject", userCert);
            certInfo.Add(Subject);

            int idx = userCert.IndexOf(",") + 1;
            var CN = userCert.Substring(3, idx - 4);
            idx = CN.LastIndexOf(".") ;
            Tuple<string, string> UserName = Tuple.Create<string, string>("userName", CN.Substring(0, idx));
            certInfo.Add(UserName);

            idx = CN.LastIndexOf(".") + 1;
            Tuple<string, string> EID = Tuple.Create<string, string>("eid",CN.Substring(idx));
            certInfo.Add(EID);

            certInfo.Add(EID);

            return certInfo;
        }

        #endregion CAC


    }
}
