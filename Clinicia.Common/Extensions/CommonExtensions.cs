﻿using System;

namespace Clinicia.Common.Extensions
{
    public static class CommonExtensions
    {
        public static TOut IfNotNull<TIn, TOut>(this TIn value, Func<TIn, TOut> innerProperty)
            where TIn : class
            where TOut : class
        {
            return value == null ? null : innerProperty(value);
        }

        public static TOut IfNotNull<TIn, TOut>(this TIn? value, Func<TIn, TOut> innerProperty)
            where TIn : struct
            where TOut : class
        {
            return value.HasValue ? innerProperty(value.Value) : null;
        }

        public static TOut? IfNotNull<TIn, TOut>(this TIn value, Func<TIn, TOut?> innerProperty)
            where TIn : class
            where TOut : struct
        {
            return value == null ? null : innerProperty(value);
        }

        public static TOut IfNotNull<TIn, TOut>(this TIn value, Func<TIn, TOut> innerProperty, TOut otherwise)
            where TIn : class
        {
            return value == null ? otherwise : innerProperty(value);
        }

        public static TOut IfNotNull<TIn, TOut>(this TIn? value, Func<TIn, TOut> innerProperty, TOut otherwise)
            where TIn : struct
        {
            return value.HasValue ? innerProperty(value.Value) : otherwise;
        }

        public static TOut? IfNotNull<TIn, TOut>(this TIn? value, Func<TIn, TOut?> innerProperty)
            where TIn : struct
            where TOut : struct
        {
            return value.HasValue ? innerProperty(value.Value) : null;
        }
    }
}