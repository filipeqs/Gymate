﻿using Microsoft.AspNetCore.Authentication;
using System.Net.Http.Headers;

namespace Gymate.Aggregator.Infrastructure
{
    public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpClientAuthorizationDelegatingHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authorizationHeader = _httpContextAccessor.HttpContext
                .Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authorizationHeader))
                request.Headers.Add("Authorization", new List<string>() { authorizationHeader });

            var token = await GetTokenAsync();

            if (token != null)
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }

        Task<string> GetTokenAsync()
        {
            const string ACCESS_TOKEN = "access_token";

            return _httpContextAccessor.HttpContext
                .GetTokenAsync(ACCESS_TOKEN);
        }
    }
}
