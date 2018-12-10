using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Management.Automation;
using System.Threading.Tasks;
using System.Windows;

namespace Example3
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

        private Collection<PSObject> RunAsyncPS(String Command, IDictionary<String, Object> Parameters)
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

            IAsyncResult async = ps.BeginInvoke();
            var returnCollection = new Collection<PSObject>();
            foreach (PSObject obj in ps.EndInvoke(async))
            {
                returnCollection.Add(obj);
            }
            return returnCollection;
        }

        private async void GetService_Click(object sender, RoutedEventArgs e)
        {
            GetService.IsEnabled = false;

            var Parameters = new Dictionary<string, object>();
            Parameters.Add("Name", ServiceName.Text);

            var result = await Task.Run(() => RunAsyncPS("Get-Service", Parameters));

            foreach (var r in result)
            {
                Output.Text += r.ToString();
            }

            Scroll.ScrollToBottom();
            GetService.IsEnabled = true;
        }

    }
}
