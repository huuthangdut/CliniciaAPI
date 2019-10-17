using System;
using System.Threading.Tasks;

namespace Clinicia.Common.Helpers
{
    // TODO: Using Nito.AsyncEx to run async method, waiting for support .NET Core 2.0
    public static class AsyncHelper
    {
        public static TResult RunSync<TResult>(Func<Task<TResult>> func)
        {
            return func.Invoke().Result;
        }

        public static void RunSync(Func<Task> action)
        {
            action.Invoke();
        }
    }
}