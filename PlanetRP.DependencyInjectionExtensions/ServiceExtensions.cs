using Microsoft.Extensions.DependencyInjection;
using PlanetRP.DependencyInjectionsExtensions.PlanetRP.DependencyInjectionsExtensions;
using System.Reflection;

namespace PlanetRP.DependencyInjectionsExtensions
{
    public static class ServiceExtensions
    {
        private static readonly Dictionary<Type, object> _singletonInstances;

        static ServiceExtensions()
        {
            _singletonInstances = new Dictionary<Type, object>();
        }

        private static object GetSingletonInstance(IServiceProvider serviceProvider, Type type)
        {
            if (_singletonInstances.ContainsKey(type))
            {
                return _singletonInstances[type];
            }

            var instance = serviceProvider.GetRequiredService(type);
            _singletonInstances.Add(type, instance);

            return instance;
        }

        private static readonly List<Type> servicesToInstanciate = new List<Type>();

        public static void InstanciateStartupScripts(this ServiceProvider provider)
        {

            var typesOfInterface = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.DefinedTypes)
                .Where(x => x.IsInterface &&
                (x.GetInterfaces().Contains(typeof(IStartupSingletonScript)) || 
                x.GetInterfaces().Contains(typeof(IClientStartupSigletonScript))));


            foreach (var type in typesOfInterface)
            {
                _ = provider.GetService(type);
            }

            var typesOfClass = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.DefinedTypes)
                .Where(x => x.IsClass && 
                (x.GetInterfaces().Contains(typeof(IStartupSingletonScript)) ||
                x.GetInterfaces().Contains(typeof(IClientStartupSigletonScript))));

            foreach (var type in typesOfClass)
            {
                _ = provider.GetService(type);
            }
        }


        public static void AddAllTypes<T>(this IServiceCollection services, ServiceLifetime lifetime = ServiceLifetime.Transient)
           where T : class
        {
            #region T is interface

            var typesOfInterface = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.DefinedTypes)
                .Where(x => x.IsClass && !x.IsAbstract && x.GetInterfaces().Contains(typeof(T)));

            foreach (var type in typesOfInterface)
            {

                if (services.Any(e => e.ServiceType == type))
                {
                    continue;
                }

                if (type.ImplementedInterfaces.Contains(typeof(IStartupSingletonScript)))
                {
                    servicesToInstanciate.Add(type);
                    lifetime = ServiceLifetime.Singleton;

                }

                // add as resolvable by implementation type
                services.Add(new ServiceDescriptor(type, type, lifetime));

                if (typeof(T) != type)
                {
                    // add as resolvable by service type (forwarding)
                    services.Add(new ServiceDescriptor(typeof(T), x => x.GetRequiredService(type), lifetime));
                }
            }

            #endregion

            #region T is class

            var typesOfClasses = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .SelectMany(t => t.GetTypes())
                .Where(x => x.IsClass && !x.IsAbstract && x.IsSubclassOf(typeof(T)));

            foreach (var type in typesOfClasses)
            {

                if (services.Any(e => e.ServiceType == type))
                {
                    continue;
                }
                if (type.GetTypeInfo().ImplementedInterfaces.Contains(typeof(IStartupSingletonScript)))
                {
                    servicesToInstanciate.Add(type);
                    lifetime = ServiceLifetime.Singleton;

                }

                // add as resolvable by implementation type
                services.Add(new ServiceDescriptor(type, type, lifetime));

                if (typeof(T) != type)
                {
                    // add as resolvable by service type (forwarding)
                    services.Add(new ServiceDescriptor(typeof(T), x => x.GetRequiredService(type), lifetime));
                }
            }

            #endregion
        }
    }
}
