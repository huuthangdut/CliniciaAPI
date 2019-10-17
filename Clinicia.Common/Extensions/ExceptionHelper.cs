﻿using System;
using System.Collections;
using System.Text;

namespace Clinicia.Common.Extensions
{
    public static class ExceptionHelper
    {
        public static string GetExceptionTechnicalInfo(this Exception ex)
        {
            var sb = new StringBuilder();
            GetExceptionTechnicalInfo(ex, sb);
            return sb.ToString();
        }

        public static void GetExceptionTechnicalInfo(this Exception ex, StringBuilder sb)
        {
            sb.AppendLine();
            sb.AppendFormat("Exception Type: {0}", ex.GetType().Name);
            sb.AppendLine();

            sb.AppendFormat("Message: {0}", ex.Message);
            sb.AppendLine();

            sb.AppendFormat("Source: {0}", ex.Source);
            sb.AppendLine();

            sb.AppendFormat("Stack Trace: {0}", ex.StackTrace);
            sb.AppendLine();

            sb.AppendLine();
            sb.AppendLine("Data:");
            foreach (DictionaryEntry x in ex.Data)
            {
                sb.AppendFormat("  {0} : {1}", x.Key, x.Value);
                sb.AppendLine();
            }
            sb.AppendLine();

            if (ex.InnerException != null)
            {
                sb.AppendLine("Inner exception:");
                GetExceptionTechnicalInfo(ex.InnerException, sb);
            }
        }
    }
}