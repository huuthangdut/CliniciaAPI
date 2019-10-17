using System;
using System.Text.RegularExpressions;

namespace Clinicia.Common.Helpers
{
    public static class GuidHelper
    {
        private static readonly Regex GuidRegEx =
            new Regex(
                @"^(\{){0,1}[0-9a-fA-F]{8}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{4}\-[0-9a-fA-F]{12}(\}){0,1}$",
                RegexOptions.Compiled);

        public static bool TryParse(string candidate, out Guid output)
        {
            bool isValid = false;
            output = Guid.Empty;
            if (candidate != null)
            {
                if (GuidRegEx.IsMatch(candidate))
                {
                    try
                    {
                        output = new Guid(candidate);
                        isValid = true;
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }
            return isValid;
        }

        public static Guid? Parse(string s)
        {
            Guid guid;
            if (TryParse(s, out guid))
                return guid;
            return null;
        }

        public static Guid Create()
        {
            return Guid.NewGuid();
        }
    }
}