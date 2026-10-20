using System.Diagnostics;
using System.Text;

namespace MCPForUnity.Editor.Security
{
    /// <summary>Passes literal arguments without requiring ProcessStartInfo.ArgumentList.</summary>
    internal static class SecureKeyStoreProcess
    {
        internal static void AddArgument(ProcessStartInfo startInfo, string argument)
        {
            // Quote each argument and escape backslashes only before quotes or the closing quote.
            // No shell is involved; spaces, empty strings and metacharacters remain literal.
            var quoted = new StringBuilder("\"");
            int backslashes = 0;
            foreach (char c in argument ?? string.Empty)
            {
                if (c == '\\')
                {
                    backslashes++;
                    continue;
                }
                quoted.Append('\\', c == '"' ? backslashes * 2 + 1 : backslashes);
                quoted.Append(c);
                backslashes = 0;
            }
            quoted.Append('\\', backslashes * 2);
            quoted.Append('"');
            startInfo.Arguments += (string.IsNullOrEmpty(startInfo.Arguments) ? "" : " ") + quoted;
        }
    }
}
