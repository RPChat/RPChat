﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RPChat.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.Map("/ws", app_ =>
            {
                app_.UseWebSockets();
                var room = new GlobalChatRoom();
                app_.Use((context, n) => AcceptConnection(context, n, room));
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        private async static Task AcceptConnection(HttpContext context, Func<Task> n, GlobalChatRoom room)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                return;
            }
            var socket = await context.WebSockets.AcceptWebSocketAsync();
            var handler = new Connection(socket, listener => room.Join(listener));
            await handler.Serve();
        }
    }
}
