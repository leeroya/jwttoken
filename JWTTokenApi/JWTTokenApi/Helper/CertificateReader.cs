using System.Configuration;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace JWTTokenApi.Helper
{
  public class CertificateReader
  {
    public X509Certificate2 ReadCertificate()
    {
      var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
      store.Open(OpenFlags.ReadOnly);
      var cert = new X509Certificate2();
      foreach (X509Certificate2 signingCert in store.Certificates.Cast<X509Certificate2>()
        .Where(signingCert => signingCert.Subject.Contains(ConfigurationManager.AppSettings["Issuer"])))
      {
        cert = signingCert;
      }
      store.Close();
      return cert;
    }
  }
}