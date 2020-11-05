using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TourCmdAPI.TokenAuthentication;

namespace TourCmdAPI.Filter
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        //context to be used to pulled out the token
        //a filter cannot regular DI.  Get it from the context
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenManager = (ITokenManager)context.HttpContext.RequestServices.GetService(typeof(ITokenManager));
            var verified = true;
            if(!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                verified = false;
            
            string token = string.Empty;
            if(verified){
                token = context.HttpContext.Request.Headers.First(h => h.Key == "Authorization").Value;
                if(!tokenManager.VerifyToken(token))
                    verified = false;
            }
            if(verified){
                context.ModelState.AddModelError("Unauthorized","Transaction not Authorized.");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
                
        }
    }
}