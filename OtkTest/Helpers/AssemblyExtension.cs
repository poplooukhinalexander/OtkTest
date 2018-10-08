using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;

using Microsoft.Extensions.DependencyInjection;

namespace OtkTest.Helpers
{
    public static class AssemblyExtension
    {
        public static IEnumerable<Type> GetTypeAssignableFrom<TService>(this Assembly assembly)
        {
            Contract.Requires(assembly != null);
            var serviceType = typeof(TService);
            return assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && serviceType.IsAssignableFrom(t));
        }

        public static void RegisterAllInstancesOfInterface<TService>(this Assembly assembly, IServiceCollection services, ServiceLifetime serviceLifetime = ServiceLifetime.Scoped)
        {
            Contract.Requires(assembly != null);
            Contract.Requires(services != null);

            foreach (var implementationType in assembly.GetTypeAssignableFrom<TService>())
            {
                if (serviceLifetime == ServiceLifetime.Transient)
                    services.AddTransient(typeof(TService), implementationType);
                else if (serviceLifetime == ServiceLifetime.Singleton)
                    services.AddSingleton(typeof(TService), implementationType);
                else
                    services.AddScoped(typeof(TService), implementationType);
            }
        }
    }
}
