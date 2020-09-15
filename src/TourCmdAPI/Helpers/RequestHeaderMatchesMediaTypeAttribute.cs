using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace TourCmdAPI.Helpers
{
    //multiple instance of this attribute Accetp header or content type header
    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = true)]
    //this is our Action Contstraint attribute
    public class RequestHeaderMatchesMediaTypeAttribute : Attribute, IActionConstraint
    {
        private readonly string[] _mediaTypes;
        private readonly string _requestHeaderToMatch;

        public RequestHeaderMatchesMediaTypeAttribute(string requestHeaderToMatch,
            string[] mediaTypes)
        {
            _requestHeaderToMatch = requestHeaderToMatch;
            _mediaTypes = mediaTypes;
        }
        //order in same stage running in same stage and that is zero
        public int Order {
            get { return 0; }
        }

        public bool Accept(ActionConstraintContext context)
        {
            var requestHeaders = context.RouteContext.HttpContext.Request.Headers;

            if(!requestHeaders.ContainsKey(_requestHeaderToMatch)){
                return false;
            }
            foreach(var mediaType in _mediaTypes){
                var headerValues = requestHeaders[_requestHeaderToMatch]
                    .ToString().Split(',').ToList();
                foreach(var headerValue in headerValues ) {
                    if(string.Equals(headerValue, mediaType,
                        StringComparison.OrdinalIgnoreCase)){
                            return true;
                        }
                }
            }
            return false;
        }
    }
}