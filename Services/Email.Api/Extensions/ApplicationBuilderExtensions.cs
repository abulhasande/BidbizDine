﻿using Email.Api.Messaging;
using System.Runtime.CompilerServices;

namespace Email.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        private static IAzureServiceBusConsumer serviceBusConsumer {  get; set; }
        public static IApplicationBuilder UseAzureServiceBusConsumer(this IApplicationBuilder app)
        {
            serviceBusConsumer = app.ApplicationServices.GetService<IAzureServiceBusConsumer>();
            var hostApplicationLife = app.ApplicationServices.GetService<IHostApplicationLifetime>();


            hostApplicationLife.ApplicationStarted.Register(OnStart);
            hostApplicationLife.ApplicationStopping.Register(OnStop);

            return app; 
        }

        private static void OnStop()
        {
            serviceBusConsumer.Stop();
        }

        private static void OnStart()
        {
            serviceBusConsumer.Start();
        }
    }
}
