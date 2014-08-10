using System.Configuration;
using System.IdentityModel.Tokens;
using System.Web.Http;
using JWTTokenApi.Helper;

namespace JWTTokenApi.Controllers
{
  public class ValuesController : BaseController
  {
    private readonly CertificateReader _certificateReader;
    private readonly TokenCreator _tokenCreator;
    private readonly TokenValidator _tokenValidator;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;


    public ValuesController()
    {
      _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
      _tokenCreator = new TokenCreator();
      _certificateReader = new CertificateReader();
      _tokenValidator = new TokenValidator(_jwtSecurityTokenHandler, _certificateReader);
    }

    [ActionName("Token")]
    public string Post(string username,string password)
    {
      if (username == string.Empty)
        return string.Empty;

      if (password == string.Empty)
        return string.Empty;

      if (username != ConfigurationManager.AppSettings["ValidUser"])//this is due to authentication layer in this demo
        return string.Empty;

      var cert = _certificateReader.ReadCertificate();
      return _tokenCreator.CreateTokenWithX509SigningCredentials(cert, username);
    }

    // POST api/values
    [System.Web.Http.ActionName("Validate")]
    public bool Post([FromBody]string value)
    {
      return _tokenValidator.Validate(value) != null;
    }
  }
}
