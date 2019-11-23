using Hangfire;
using System;

namespace Clinicia.WebApi.Schedulings
{
    public class HangfireActivator : JobActivator
    {
        private readonly IServiceProvider serviceProvider;

        public HangfireActivator(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public override object ActivateJob(Type type)
        {
            return serviceProvider.GetService(type);
        }
    }
}
