using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TODOAPP.Domain.Interfaces.IServices;
using TODOAPP.Services;

namespace TODOAPP.Utilies
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next= null!;
        private const string _correlationIdHeader="X-Correlation-ID";
        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, ICorrelationIdGenerator correlatlionIdGenerator)
        {
            var correlationId= await getCorrelationIdTrace(context, correlatlionIdGenerator);
            await AddCorrelationIdToResponse(context, correlationId);
            await _next(context);
        }

        private static async Task<string> getCorrelationIdTrace(HttpContext context, ICorrelationIdGenerator correlationIdGenerator)
        {
            if(context.Request.Headers.TryGetValue(_correlationIdHeader, out var correlationId))
            {
                await correlationIdGenerator.Set(correlationId!);
                return correlationId!;
            }

            return await correlationIdGenerator.Get();
        }
        private static Task AddCorrelationIdToResponse(HttpContext _context, string _correlationId)
        {
            _context.Response.OnStarting(async () =>
            {
                _context.Response.Headers[_correlationIdHeader] = _correlationId;
            });
            return Task.CompletedTask;
        }
    }
}