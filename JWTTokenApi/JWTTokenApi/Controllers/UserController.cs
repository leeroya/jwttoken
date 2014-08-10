using System;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Web.Http;
using JWTTokenApi.Helper;

namespace JWTTokenApi.Controllers
{
    public class UserController : ApiController
    {
      private readonly CertificateReader _certificateReader;
      private readonly TokenValidator _tokenValidator;
      private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

      public UserController()
      {
        _certificateReader = new CertificateReader();
        _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        _tokenValidator = new TokenValidator(_jwtSecurityTokenHandler, _certificateReader);
      }

      public string Post([FromBody]string value)
      {
        var claim = _tokenValidator.Validate(value);
        if (claim != null)
        {
          return claim.Identities.FirstOrDefault().Name;
        }
        throw new Exception("Unautherised.");
      }
    }
}
