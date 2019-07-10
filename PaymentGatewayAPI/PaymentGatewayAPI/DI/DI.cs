using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using PaymentGatewayAPI.Logger;

namespace PaymentGatewayAPI.DI
{
    public class DI
    {
        public static IContainer Container { get; set; }

        public static void InitializeContainer()
        {
            // Building dependencies 
            var builder = new ContainerBuilder();
            // Configure builder to use SQL logger implementation
            builder.RegisterType<SQLLogger>().As<ILoggerInterface>();
            Container = builder.Build();
        }
    }
}