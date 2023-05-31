using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinkoffMapper.Extensions
{
    public static class UriExtensions
    {
        public static bool IsPredefinedScheme(this string value)
        {
            if (value == null || value.Length < 2)
                return false;
            char ch = value[0];
            switch (ch)
            {
                case 'f':
                    return value == "file" || value == "ftp";
                case 'h':
                    return value == "http" || value == "https";
                case 'n':
                    return value[1] == 'e' ? value == "news" || value == "net.pipe" || value == "net.tcp" : value == "nntp";
                case 'w':
                    return value == "ws" || value == "wss";
                default:
                    return ch == 'g' && value == "gopher" || ch == 'm' && value == "mailto";
            }
        }

        public static bool MaybeUri(this string value)
        {
            if (value == null || value.Length == 0)
                return false;
            int length = value.IndexOf(':');
            return length != -1 && length < 10 && value.Substring(0, length).IsPredefinedScheme();
        }

        public static Uri ToUri(this string uriString)
        {
            Uri result;
            Uri.TryCreate(uriString, uriString.MaybeUri() ? UriKind.Absolute : UriKind.Relative, out result);
            return result;
        }

        internal static bool TryCreateUri(
            this string uriString,
            out Uri result,
            out string message)
        {
            result = (Uri)null;
            Uri uri1 = uriString.ToUri();
            if (uri1 == (Uri)null)
            {
                message = "An invalid URI string: " + uriString;
                return false;
            }
            if (!uri1.IsAbsoluteUri)
            {
                message = "Not an absolute URI: " + uriString;
                return false;
            }
            string scheme = uri1.Scheme;
            if (!(scheme == "ws") && !(scheme == "wss"))
            {
                message = "The scheme part isn't 'ws' or 'wss': " + uriString;
                return false;
            }
            if (uri1.Fragment.Length > 0)
            {
                message = "Includes the fragment component: " + uriString;
                return false;
            }
            int port = uri1.Port;
            if (port == 0)
            {
                message = "The port part is zero: " + uriString;
                return false;
            }
            ref Uri local = ref result;
            Uri uri2;
            if (port == -1)
                uri2 = new Uri(string.Format("{0}://{1}:{2}{3}", (object)scheme, (object)uri1.Host, (object)(scheme == "ws" ? 80 : 443), (object)uri1.PathAndQuery));
            else
                uri2 = uri1;
            local = uri2;
            message = string.Empty;
            return true;
        }

    }
}
