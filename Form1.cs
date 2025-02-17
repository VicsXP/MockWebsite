using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace MockWebsite
{
    public partial class Form1 : Form
    {
        private string historyFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "History.txt");

        public Form1()
        {
            InitializeComponent();
            this.Resize += new System.EventHandler(this.Form_Resize);
            this.Load += new System.EventHandler(this.Form1_Load);

            webView.NavigationStarting += EnsureHttps;
            InitializeAsync();

            comboBoxHistory.Visible = false;
            comboBoxHistory.SelectedIndexChanged += ComboBoxHistory_SelectedIndexChanged;
            addressBar.KeyDown += AddressBar_KeyDown;

        }
        async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.WebMessageReceived += UpdateAddressBar;

            await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.postMessage(window.document.URL);");
            await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync("window.chrome.webview.addEventListener(\'message\', event => alert(event.data));");
        }
        void UpdateAddressBar(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            var uri = args.TryGetWebMessageAsString();
            addressBar.Text = uri;
            webView.CoreWebView2.PostWebMessageAsString(uri);
        }

        void EnsureHttps(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            string uri = args.Uri;
            if (!uri.StartsWith("https://"))
            {
                webView.CoreWebView2.ExecuteScriptAsync($"alert('{uri} no es seguro, intenta otra https link')");
                args.Cancel = true;
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            int padding = 10;

            btnHistory.Left = this.ClientSize.Width - btnHistory.Width - padding;
            btnHistory.Top = padding;

            goButton.Left = btnHistory.Left - goButton.Width - padding;
            goButton.Top = padding;

            addressBar.Left = padding;
            addressBar.Top = padding;
            addressBar.Width = goButton.Left - addressBar.Left - padding;

            webView.Location = new Point(0, addressBar.Bottom + padding);
            webView.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - addressBar.Height - padding);
        }


        private void goButton_Click(object sender, EventArgs e)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                string input = addressBar.Text.Trim();

                if (!input.Contains("."))
                {
                    input = $"https://www.google.com/search?q={Uri.EscapeDataString(input)}";
                }
                else if (!input.StartsWith("http://") && !input.StartsWith("https://"))
                {
                    input = "https://" + input;
                }

                webView.CoreWebView2.Navigate(input);
                Guardar(input);
                comboBoxHistory.Visible = false;
            }
        }
        private void Guardar(string uri)
        {
            try
            {
                using (StreamWriter writer = File.AppendText(historyFile))
                {
                    writer.WriteLine(uri);
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error de acceso al archivo. Asegúrese de que el archivo no está abierto en otro programa.\n" + ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                MessageBox.Show("Error de permisos. Ejecute la aplicación como administrador.\n" + ex.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar historial: " + ex.Message);
            }
        }

        private void Navegar(string input)
        {
            if (webView != null && webView.CoreWebView2 != null)
            {
                if (!input.Contains("."))
                {
                    input = $"https://www.google.com/search?q={Uri.EscapeDataString(input)}";
                }
                else if (!input.StartsWith("http://") && !input.StartsWith("https://"))
                {
                    input = "https://" + input;
                }

                webView.CoreWebView2.Navigate(input);
                Guardar(input);
                comboBoxHistory.Visible = false;
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadHistory();
        }
        private void LoadHistory()
        {
            try
            {
                if (File.Exists(historyFile))
                {
                    string[] history = File.ReadAllLines(historyFile);
                    comboBoxHistory.Items.Clear();
                    comboBoxHistory.Items.AddRange(history);

                    if (comboBoxHistory.Items.Count > 0)
                    {
                        comboBoxHistory.SelectedIndex = comboBoxHistory.Items.Count - 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar historial: " + ex.Message);
            }
        }

        private void ComboBoxHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxHistory.SelectedItem != null)
            {
                Navegar(comboBoxHistory.SelectedItem.ToString());
            }
        }
        private void AddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                goButton.PerformClick(); 
                comboBoxHistory.Visible = false;
                e.SuppressKeyPress = true; 
            }
        }

        private void comboBoxHistory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (comboBoxHistory.SelectedItem != null)
            {
                string selectedUrl = comboBoxHistory.SelectedItem.ToString();
                webView.CoreWebView2.Navigate(selectedUrl);
                comboBoxHistory.Visible = false; 
            }
        }

        private void btnHistory_Click_1(object sender, EventArgs e)
        {
            if (comboBoxHistory.Items.Count == 0)
            {
                LoadHistory();
            }

            comboBoxHistory.Visible = !comboBoxHistory.Visible;
        }
    }
}
