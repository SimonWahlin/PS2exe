using System;
using System.Text;
using System.IO;
using System.Management.Automation;

namespace PS2exe
{
    /// <summary>
    /// This example shows how to compile a PowerShell script 
    /// in to an executable that runs it.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            var scriptString = GetString(
                Properties.Resources.Script, Encoding.UTF8
            );

            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(scriptString);
                ps.AddCommand("Out-String");

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
