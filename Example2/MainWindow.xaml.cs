using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace Example2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private Collection<PSObject> RunPS(String Command, IDictionary<String, Object> Parameters)
        {
            var ps = PowerShell.Create();

            // Start-Sleep -Seconds 5
            ps.AddCommand("Start-Sleep").AddParameter("Seconds", 5).AddStatement();

            // . $Command @Parameters | Out-String
            ps.AddCommand(Command);
            foreach (var key in Parameters.Keys)
            {
                ps.AddParameter(key, Parameters[key]);
            }
            ps.AddCommand("Out-String");

            return ps.Invoke();
        }

        private void GetService_Click(object sender, RoutedEventArgs e)
        {
            GetService.IsEnabled = false;

            var Parameters = new Dictionary<string, object>();
            Parameters.Add("Name", ServiceName.Text);

            var result = RunPS("Get-Service", Parameters);

            foreach (var r in result)
            {
                Output.Text += r.ToString();
            }

            Scroll.ScrollToBottom();
            GetService.IsEnabled = true;
        }

    }
}
