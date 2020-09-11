using Proteus.Application.Services;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Proteus.Application.Tests.Services
{
    public class CertValidationServiceTests
    {
        [Trait("Application", "CertService")]
        [Fact]
        public void CertValidationService_CreateCertService()
        {
            //act only
            var result = new CertValidationService();

            //assert
            Assert.IsType<CertValidationService>(result);
        }

        [Trait("Application", "CertService")]
        [Fact]
        public async Task CertValidationService_ReturnCertDataAsync()
        {
            var certService = new CertValidationService();

            //get a random cert from the certStore
            X509Store st = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            X509Certificate2 cert = new X509Certificate2();
            st.Open(OpenFlags.ReadOnly);
            var certCollection = st.Certificates;
            cert = certCollection.Find(X509FindType.FindByIssuerName,"Proteus", false)[0];
           

            //act
            var result = certService.GetCertificateInfo(cert);

            //assert
            Assert.IsType<List<Tuple<string, string>>>(result);
        }
    }
}

