using Microsoft.AspNetCore.Http;
using System.Diagnostics;

namespace API_Loan_Simulator.Telemetria
{
    public class TelemetriaMiddleware
    {
        private readonly RequestDelegate _next;

        public TelemetriaMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, TelemetriaStorage storage)
        {
            var stopwatch = Stopwatch.StartNew();
            await _next(context);
            stopwatch.Stop();

            var endpoint = context.GetEndpoint()?.DisplayName ?? context.Request.Path;
            storage.Registrar(endpoint, stopwatch.ElapsedMilliseconds);
        }
    }


}
