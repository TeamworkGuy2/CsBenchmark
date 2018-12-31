using System;
using System.Text;

namespace CsBenchmark.StringEscape
{
    /// <summary>
    /// Convert Java strings to and from JSON strings (note: this implementation does not yet support \\u four-hex-digits escape sequences, but does support \", \\, \/, \b, \f, \n, \r, \t)
    /// </summary>
    /// <author>TeamworkGuy2</author>
    /// <since>2016-2-28</since>
    public static class StringEscapeJson
    {

        /** Convert a string to a valid JSON string (i.e. '"', '\', and escape characters are escaped with '\')
         * @param str the string to convert
         * @return the resulting string, not quoted
         */
        public static string ToJsonString(string str)
        {
            var sb = new StringBuilder();
            ToJsonString(str, 0, str.Length, sb);
            return sb.ToString();
        }


        public static string ToJsonString(string str, int off, int len)
        {
            var sb = new StringBuilder();
            ToJsonString(str, off, len, sb);
            return sb.ToString();
        }


        public static void ToJsonString(String str, int off, int len, StringBuilder dst)
        {
            for (int i = off, size = off + len; i < size; i++)
            {
                char ch = str[i];
                if (ch == '"' || ch == '\\' || ch == '\b' || ch == '\f' || ch == '\n' || ch == '\r' || ch == '\t')
                {
                    dst.Append('\\');
                    switch (ch)
                    {
                        case '\b':
                            ch = 'b';
                            break;
                        case '\f':
                            ch = 'f';
                            break;
                        case '\n':
                            ch = 'n';
                            break;
                        case '\r':
                            ch = 'r';
                            break;
                        case '\t':
                            ch = 't';
                            break;
                        default:
                            break;
                    }
                }
                dst.Append(ch);
            }
        }


        public static void ToJsonString(char ch, StringBuilder dst)
        {
            if (ch == '"' || ch == '\\' || ch == '\b' || ch == '\f' || ch == '\n' || ch == '\r' || ch == '\t')
            {
                dst.Append('\\');
                switch (ch)
                {
                    case '\b':
                        ch = 'b';
                        break;
                    case '\f':
                        ch = 'f';
                        break;
                    case '\n':
                        ch = 'n';
                        break;
                    case '\r':
                        ch = 'r';
                        break;
                    case '\t':
                        ch = 't';
                        break;
                    default:
                        break;
                }
            }
            dst.Append(ch);
        }


        /** Convert a JSON string to a normal string (i.e. '\"', '\\', '\b', etc. are un-escaped)
         * @param str the string to convert
         * @return the resulting string
         */
        public static string FromJsonString(string str)
        {
            var sb = new StringBuilder();
            FromJsonString(str, 0, str.Length, sb);
            return sb.ToString();
        }


        public static string FromJsonString(string str, int off, int len)
        {
            var sb = new StringBuilder();
            FromJsonString(str, off, len, sb);
            return sb.ToString();
        }


        public static void FromJsonString(string str, int off, int len, StringBuilder dst)
        {
            char prevCh = (char)0;
            for (int i = off, size = off + len; i < size; i++)
            {
                char ch = str[i];
                bool wasEscaped = false;
                if (prevCh == '\\')
                {
                    if (ch == '"' || ch == '\\' || ch == 'b' || ch == 'f' || ch == 'n' || ch == 'r' || ch == 't')
                    {
                        char c = (ch == '"' ? '"' : (ch == '\\' ? '\\' : (ch == 'b' ? '\b' : (ch == 't' ? '\t' : (ch == 'f' ? '\f' : (ch == 'n' ? '\n' : (ch == 'r' ? '\r' : (char)0)))))));
                        dst.Append(c);
                    }
                    else
                    {
                        throw new InvalidOperationException("character after '\\' must be '\"', '\\', or a control char, found '" + ch + "' in: " + str.Substring(off, len));
                    }
                    // because the escape char has been handled, if this is not done, double backslash causes issues
                    // because if(prevCh == '\\') is true for the character after the second backslash and this code runs
                    wasEscaped = true;
                }
                if (ch != '\\' && !wasEscaped)
                {
                    dst.Append(ch);
                }
                prevCh = !wasEscaped ? ch : (char)0;
            }
        }

    }
}
