using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOAPP.Utilies;

namespace TODOAPP.ExtentionMethods
{
    public static class ApplicationBuilderExtention
    {
        public static Task<IApplicationBuilder> AddCorrelationIdMiddlewareAsync(this IApplicationBuilder app)
        {
            app.UseMiddleware<CorrelationIdMiddleware>();
            return Task.FromResult(app);
        }
    }
}