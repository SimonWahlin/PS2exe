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
