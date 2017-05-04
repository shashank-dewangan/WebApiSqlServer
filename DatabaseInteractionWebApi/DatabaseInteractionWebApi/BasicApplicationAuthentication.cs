using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace DatabaseInteractionWebApi
{
    public class BasicApplicationAuthentication : AuthorizationFilterAttribute
    {

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if(actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,"Not Authorized");
            }
            else
            {
                string credentials = actionContext.Request.Headers.Authorization.Parameter;
                string encodeCredentials = Encoding.UTF8.GetString(Convert.FromBase64String(credentials));
                string[] usernamepassword = encodeCredentials.Split(':');
                string username = usernamepassword[0];
                string password = usernamepassword[1];
                if(ApplicationLogin.Login(username,password))
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(username),null);
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized, "Not Authorized");
                }
            }
        }
    }
}