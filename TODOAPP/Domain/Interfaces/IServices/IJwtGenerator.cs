using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TODOAPP.Models.DTOs.Responses;

namespace TODOAPP.Data.Services
{
    public interface IJwtGenerator
    {
        public Task<AuthResult> GenerateToken(IdentityUser user);
    }
}