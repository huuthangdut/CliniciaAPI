using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Clinicia.Common.Helpers
{
    public static class AutoRegisterHelpers
    {
        public static AutoRegisterData RegisterAssemblyTypes(this IServiceCollection services, params Assembly[] assemblies)
        {
            var allPublicTypes = assemblies.SelectMany(x => x.GetExportedTypes()
                .Where(y => y.IsClass && !y.IsAbstract && !y.IsGenericType && !y.IsNested));
            return new AutoRegisterData(services, allPublicTypes);
        }

        public static AutoRegisterData Where(this AutoRegisterData autoRegData, Func<Type, bool> predicate)
        {
            if (autoRegData == null)
            {
                throw new ArgumentNullException(nameof(autoRegData));
            }

            autoRegData.TypeFilter = predicate;
            return new AutoRegisterData(autoRegData.Services, autoRegData.TypesToConsider.Where(predicate));
        }

        public static IServiceCollection AsImplementedInterfaces(
            this AutoRegisterData autoRegData,
            ServiceLifetime lifetime = ServiceLifetime.Transient)
        {
            if (autoRegData == null)
            {
                throw new ArgumentNullException(nameof(autoRegData));
            }

            foreach (var classType in autoRegData.TypeFilter == null
                ? autoRegData.TypesToConsider
                : autoRegData.TypesToConsider.Where(autoRegData.TypeFilter))
            {
                var interfaces = classType.GetTypeInfo().ImplementedInterfaces
                    .Where(i => i != typeof(IDisposable) && i.IsPublic);
                foreach (var infc in interfaces)
                {
                    autoRegData.Services.Add(new ServiceDescriptor(infc, classType, lifetime));
                }
            }

            return autoRegData.Services;
        }
    }

    public class AutoRegisterData
    {
        public AutoRegisterData(IServiceCollection services, IEnumerable<Type> typesToConsider)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
            TypesToConsider = typesToConsider ?? throw new ArgumentNullException(nameof(typesToConsider));
        }

        public IServiceCollection Services { get; }

        public IEnumerable<Type> TypesToConsider { get; }

        public Func<Type, bool> TypeFilter { get; set; }
    }
}