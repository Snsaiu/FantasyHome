﻿using Furion.DependencyInjection;
using Furion.DynamicApiController;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace FantasyHomeCenter.Application
{

    [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme)]
    public class SystemService : ISystemService, IDynamicApiController, ITransient
    {


        
        public string GetDescription()
        {
            return "让 .NET 开发更简单，更通用，更流行。";
        }
    }
}