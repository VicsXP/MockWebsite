using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Web.WebView2.Core;

namespace MockWebsite
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.Resize += Form_Resize;
            addressBar.KeyDown += AddressBar_KeyDown;

            webView.NavigationStarting += EnsureHttps;
            InitializeAsync();
        }
        async void InitializeAsync()
        {
            try
            {
                await webView.EnsureCoreWebView2Async(null);
                webView.CoreWebView2.WebMessageReceived += UpdateAddressBar;

                await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(
                    @"window.chrome.webview.postMessage(window.document.URL);");
                await webView.CoreWebView2.AddScriptToExecuteOnDocumentCreatedAsync(
                    @"window.chrome.webview.addEventListener('message', event => alert(event.data));");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error initializing WebView: " + ex.Message, "Initialization Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void UpdateAddressBar(object sender, CoreWebView2WebMessageReceivedEventArgs args)
        {
            var uri = args.TryGetWebMessageAsString();
            addressBar.Text = uri;
            webView.CoreWebView2.PostWebMessageAsString(uri);
        }

        void EnsureHttps(object sender, CoreWebView2NavigationStartingEventArgs args)
        {
            var uri = args.Uri;

            if (!uri.StartsWith("https://"))
            {
                var result = MessageBox.Show($"{uri} no es seguro. ¿Deseas continuar?", "Advertencia de seguridad",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    args.Cancel = true;
                }
            }
        }


        private void Form_Resize(object sender, EventArgs e)
        {
            webView.Size = new Size(this.ClientSize.Width, this.ClientSize.Height - webView.Top);
            goButton.Left = Math.Max(this.ClientSize.Width - goButton.Width, addressBar.Right + 5);
            addressBar.Width = goButton.Left - addressBar.Left - 5;
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            if (webView?.CoreWebView2 != null)
            {
                var url = GetFormattedUrl(addressBar.Text);
                webView.CoreWebView2.Navigate(url);
            }
        }
        private string GetFormattedUrl(string input)
        {
            input = input.Trim();

            if (!input.Contains(".")) return $"https://www.google.com/search?q={Uri.EscapeDataString(input)}";
            if (!input.StartsWith("http://") && !input.StartsWith("https://")) return "https://" + input;

            return input;
        }
        private void AddressBar_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                goButton.PerformClick();
                e.SuppressKeyPress = true;
            }
        }
    }
}
