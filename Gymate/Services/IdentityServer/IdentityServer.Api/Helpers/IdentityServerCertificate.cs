using System.Security.Cryptography.X509Certificates;

namespace IdentityServer.Api.Helpers
{
    public class IdentityServerCertificate
    {
        public static X509Certificate2? GetCertificate()
        {
            X509Certificate2? cert = null;
            using (X509Store certStore = new(StoreName.My, StoreLocation.CurrentUser))
            {
                certStore.Open(OpenFlags.ReadOnly);
                var certCollection = certStore.Certificates.Find(
                    X509FindType.FindByThumbprint,
                    "a7b6ccfeea52e73dad150a8604fdfcaed43019e6",
                    false);

                if (certCollection.Count > 0)
                    cert = certCollection[0];
            }

            return cert;
        }
    }
}
