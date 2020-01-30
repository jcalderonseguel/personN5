using Refit;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Super.EWalletCore.PersonDataManagement.Application.UnitTests.Common
{
    public class CustomApiException : ApiException
    {
        public CustomApiException(HttpRequestMessage message, HttpMethod httpMethod, HttpStatusCode statusCode, string reasonPhrase, HttpResponseHeaders headers, RefitSettings refitSettings = null) : base(message, httpMethod, statusCode, reasonPhrase, headers, refitSettings)
        {
        }
    }
}
