﻿using Microsoft.IdentityModel.Tokens;
using OrderService.Application.Interfaces;
using System.Security.Claims;

namespace OrderService.WebApi.Services
{
    public class ClaimService:IClaimService
    {
        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            // todo implementation to get the current userId
            var Id = httpContextAccessor.HttpContext?.User?.FindFirstValue("UserId");
            GetCurrentUser = string.IsNullOrEmpty(Id) ? Guid.Empty : Guid.Parse(Id);

            var email = httpContextAccessor.HttpContext?.User.FindFirstValue("Email");
            GetEmail = email.IsNullOrEmpty() ? "" : email!.ToString();

        }
        public string GetEmail { get; }

        public Guid GetCurrentUser { get; }
    }
}
