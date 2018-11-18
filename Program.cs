using System;
using System.Text;
using System.IO;
using System.Management.Automation;

namespace PS2exe
{
    class Program
    {
        static void Main(string[] args)
        {
            //var unwantedChars = new Char[] { '\uFEFF', '\u200B' };
            //var scriptString = Encoding.UTF8.GetString(Properties.Resources.Script).Trim(unwantedChars);
            var scriptString = GetString(Properties.Resources.Script, Encoding.UTF8);
            using (PowerShell ps = PowerShell.Create())
            {
                ps
                    .AddScript(scriptString)
                    .AddCommand("Out-String");

                var result = ps.Invoke();
                foreach (var line in result)
                {
                    Console.WriteLine(line);
                }
            }
        }

        private static string GetString(byte[] data, Encoding encoding)
        {
            using (var stream = new MemoryStream(data, writable:false))
            using (var reader = new StreamReader(stream, encoding))
            {
                return reader.ReadToEnd();
            }
        }

    }
}
