using Microsoft.Extensions.DependencyInjection;
using Radiant.Domain.Exceptions;
using Radiant.Service.Attributes;
using System.Reflection;

namespace Radiant.Service.Injections
{
    /// <summary>
    /// A static helper class for service injections.
    /// </summary>
    public static class ServiceInjections
    {
        [Obsolete("AddServices with namespace is deprecated, please use AddServices or ScanServicesIn with Service Attribute instead.")]
        public static void AddServices(this IServiceCollection services, string assemblyName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            assembly.GetTypes().Where(t => $"{assembly.GetName().Name}.Service" == t.Namespace
                                        && !t.IsAbstract
                                        && !t.IsInterface
                                        && t.Name.EndsWith("Service"))
                .Select(a => new { assignedType = a })
                .ToList()
                .ForEach(typesToRegister =>
                {
                    services.AddScoped(typesToRegister.assignedType);
                });
        }

        /// <summary>
        /// Extension method for the IServiceCollection interface. Adds services to the collection.
        /// </summary>
        /// <param name="services"> The IServiceCollection to which the services need to be added. </param>
        /// <exception cref="CoreException"> Throws if there are no services with the ServiceAttribute present. </exception>
        /// <remarks>
        /// This method scans all types in all assemblies of the current application domain. It looks for types that have the ServiceAttribute.
        /// If no such types are found, an exception about no services is thrown. If there are such types, they are all added as services
        /// with scope lifetime into the passed IServiceCollection.
        /// </remarks>
        public static void AddServices(this IServiceCollection services)
        {
            var typesWithMyAttribute =
                from assembly in AppDomain.CurrentDomain.GetAssemblies().AsParallel()
                from type in assembly.GetTypes()
                let attributes = type.GetCustomAttributes(typeof(ServiceAttribute), true)
                where attributes is { Length: > 0 }
                select type;

            var types = typesWithMyAttribute.ToList();
            if (!types.Any())
            {
                throw CoreException.Format(CoreExceptionEnum.NO_SERVICE);
            }

            foreach (var type in types)
            {
                services.AddScoped(type);
            }
        }

        /// <summary>
        /// Extension method for the IServiceCollection interface. Adds services to the collection.
        /// </summary>
        /// <param name="services"> The IServiceCollection to which the services need to be added. </param>
        /// <param name="assembly"> The assembly to load</param>
        /// <exception cref="CoreException"> Throws if there are no services with the ServiceAttribute present. </exception>
        /// <remarks>
        /// This method scans all types in the specified assembly. It looks for types that have the ServiceAttribute.
        /// If no such types are found, an exception about no services is thrown. If there are such types, they are all added as services
        /// with scope lifetime into the passed IServiceCollection.
        /// </remarks>
        public static void ScanServicesIn(this IServiceCollection services, Assembly assembly)
        {
            var typesWithMyAttribute =
                from type in assembly.GetTypes()
                let attributes = type.GetCustomAttributes(typeof(ServiceAttribute), true)
                where attributes is { Length: > 0 }
                select type;

            var types = typesWithMyAttribute.ToList();
            if (!types.Any())
            {
                throw CoreException.Format(CoreExceptionEnum.NO_SERVICE);
            }

            foreach (var type in types)
            {
                services.AddScoped(type);
            }
        }

        /// <summary>
        /// Extension method for the IServiceCollection interface. Adds services to the collection.
        /// </summary>
        /// <param name="services"> The IServiceCollection to which the services need to be added. </param>
        /// <param name="assemblyName"> The name of the assembly to load</param>
        /// <exception cref="CoreException"> Throws if there are no services with the ServiceAttribute present. </exception>
        /// <remarks>
        /// This method scans all types in the specified assembly. It looks for types that have the ServiceAttribute.
        /// If no such types are found, an exception about no services is thrown. If there are such types, they are all added as services
        /// with scope lifetime into the passed IServiceCollection.
        /// </remarks>
        public static void ScanServicesIn(this IServiceCollection services, string assemblyName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            var typesWithMyAttribute =
                from type in assembly.GetTypes()
                let attributes = type.GetCustomAttributes(typeof(ServiceAttribute), true)
                where attributes is { Length: > 0 }
                select type;

            var types = typesWithMyAttribute.ToList();
            if (!types.Any())
            {
                throw CoreException.Format(CoreExceptionEnum.NO_SERVICE);
            }

            foreach (var type in types)
            {
                services.AddScoped(type);
            }
        }
    }
}