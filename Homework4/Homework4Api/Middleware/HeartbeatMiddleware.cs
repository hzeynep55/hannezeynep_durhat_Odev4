using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework4Api
{
    public class HeartbeatMiddleware
    {
        private readonly RequestDelegate _next;

        public HeartbeatMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        // request url  
       
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/heartbeat"))
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync("Hello from the server");
                return;
            }

            await _next.Invoke(context);
        }
    }
}
