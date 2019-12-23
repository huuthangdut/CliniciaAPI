using Clinicia.Common.Enums;
using Clinicia.Common.Exceptions;
using PhoneNumbers;
using System;

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

        public static string ToStandardFormatPhoneNumber(this string number, string countryCode = "VN")
        {
            try
            {
                var util = PhoneNumberUtil.GetInstance();
                var phoneNumber = util.Parse(number, countryCode);

                bool isValidPhoneNumber = util.IsValidNumber(phoneNumber);
                var numberType = util.GetNumberType(phoneNumber);
                bool isMobile = numberType == PhoneNumberType.MOBILE || numberType == PhoneNumberType.FIXED_LINE_OR_MOBILE;

                if (!isValidPhoneNumber || !isMobile)
                {
                    throw new BusinessException(ErrorCodes.Failed.ToString(), "Số điện thoại không hợp lệ.");
                }

                return util.Format(phoneNumber, PhoneNumberFormat.E164);
            }
            catch (NumberParseException)
            {
                throw new BusinessException(ErrorCodes.Failed.ToString(), "Số điện thoại không hợp lệ.");
            }
        }
    }
}