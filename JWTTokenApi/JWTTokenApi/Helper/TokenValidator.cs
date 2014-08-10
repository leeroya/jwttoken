using System.Configuration;
using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;

namespace JWTTokenApi.Helper
{
  public class TokenValidator
  {
    private JwtSecurityTokenHandler _tokenHandler;
    private CertificateReader _certificateReader;

    public TokenValidator(JwtSecurityTokenHandler tokenHandler, CertificateReader certificateReader)
    {
      _certificateReader = certificateReader;
      _tokenHandler = tokenHandler;
    }

    internal TokenValidationParameters TokenValidationParameters(X509Certificate2 cert)
    {
      var validationParameters = new TokenValidationParameters
      {
        AllowedAudience = ConfigurationManager.AppSettings["Audience"],
        ValidIssuer = ConfigurationManager.AppSettings["Issuer"],
        SigningToken = new X509SecurityToken(cert)
      };
     
      return validationParameters;
    }

    internal ClaimsPrincipal Validate(string token)
    {
      var cert = _certificateReader.ReadCertificate();
      var validationParameters = TokenValidationParameters(cert);
      return _tokenHandler.ValidateToken(token, validationParameters);
    }
  }
}