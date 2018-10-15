using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Automation;

namespace PS2exe
{
    class Program
    {
        static void Main(string[] args)
        {
            var unwantedChars = new Char[] { '\uFEFF', '\u200B' };
            var scriptString = Encoding.UTF8.GetString(Properties.Resources.Script).Trim(unwantedChars);
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
    }
}
