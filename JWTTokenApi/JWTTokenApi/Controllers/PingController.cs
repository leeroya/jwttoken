using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JWTTokenApi.Controllers
{
    public class PingController : ApiController
    {

      public String Get()
      {
        return DateTime.UtcNow.ToShortDateString();
      }

    }
}
