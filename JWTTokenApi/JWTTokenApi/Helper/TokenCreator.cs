using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IdentityModel.Claims;
using System.IdentityModel.Protocols.WSTrust;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace JWTTokenApi.Helper
{
  public class TokenCreator
  {
    internal string CreateTokenWithX509SigningCredentials(X509Certificate2 signingCert, string username)
    {
      var now = DateTime.UtcNow;
      var tokenHandler = new JwtSecurityTokenHandler();
      NameValueCollection properties = new NameValueCollection();
      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new System.Security.Claims.Claim[]
                    {
                        new System.Security.Claims.Claim(System.IdentityModel.Claims.ClaimTypes.Name, username,Rights.Identity),
                        new System.Security.Claims.Claim("Role", "Developer","True"), 
                    }),
        TokenIssuerName = ConfigurationManager.AppSettings["Issuer"],
        AppliesToAddress = ConfigurationManager.AppSettings["Audience"],
        Lifetime = new Lifetime(now, now.AddDays(30)),
        SigningCredentials = new X509SigningCredentials(signingCert)
      };

      SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
      string tokenString = tokenHandler.WriteToken(token);

      return tokenString;
    }
  }
}