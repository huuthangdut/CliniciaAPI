using System;
using System.Collections.Generic;
using System.Linq;

namespace Clinicia.Common.Extensions
{
    public static class EnumExtensions
    {
        public static Dictionary<int, string> ToDictionary<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>().ToDictionary(e => (int)(object)e, e => e.ToString());
        }
    }
}