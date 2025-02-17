namespace MockWebsite
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.webView = new Microsoft.Web.WebView2.WinForms.WebView2();
            this.addressBar = new System.Windows.Forms.TextBox();
            this.goButton = new System.Windows.Forms.Button();
            this.btnHistory = new System.Windows.Forms.Button();
            this.comboBoxHistory = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.webView)).BeginInit();
            this.SuspendLayout();
            // 
            // webView
            // 
            this.webView.AllowExternalDrop = true;
            this.webView.CreationProperties = null;
            this.webView.DefaultBackgroundColor = System.Drawing.Color.White;
            this.webView.Location = new System.Drawing.Point(12, 40);
            this.webView.Name = "webView";
            this.webView.Size = new System.Drawing.Size(792, 385);
            this.webView.Source = new System.Uri("https://www.microsoft.com", System.UriKind.Absolute);
            this.webView.TabIndex = 0;
            this.webView.ZoomFactor = 1D;
            // 
            // addressBar
            // 
            this.addressBar.Location = new System.Drawing.Point(12, 12);
            this.addressBar.Name = "addressBar";
            this.addressBar.Size = new System.Drawing.Size(593, 22);
            this.addressBar.TabIndex = 1;
            // 
            // goButton
            // 
            this.goButton.Location = new System.Drawing.Point(611, 11);
            this.goButton.Name = "goButton";
            this.goButton.Size = new System.Drawing.Size(95, 23);
            this.goButton.TabIndex = 2;
            this.goButton.Text = "Go!";
            this.goButton.UseVisualStyleBackColor = true;
            this.goButton.Click += new System.EventHandler(this.goButton_Click);
            // 
            // btnHistory
            // 
            this.btnHistory.Location = new System.Drawing.Point(712, 11);
            this.btnHistory.Name = "btnHistory";
            this.btnHistory.Size = new System.Drawing.Size(95, 23);
            this.btnHistory.TabIndex = 3;
            this.btnHistory.Text = "Historial";
            this.btnHistory.UseVisualStyleBackColor = true;
            this.btnHistory.Click += new System.EventHandler(this.btnHistory_Click_1);
            // 
            // comboBoxHistory
            // 
            this.comboBoxHistory.FormattingEnabled = true;
            this.comboBoxHistory.Location = new System.Drawing.Point(12, 40);
            this.comboBoxHistory.Name = "comboBoxHistory";
            this.comboBoxHistory.Size = new System.Drawing.Size(792, 24);
            this.comboBoxHistory.TabIndex = 4;
            this.comboBoxHistory.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.comboBoxHistory_MouseDoubleClick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 437);
            this.Controls.Add(this.comboBoxHistory);
            this.Controls.Add(this.btnHistory);
            this.Controls.Add(this.goButton);
            this.Controls.Add(this.addressBar);
            this.Controls.Add(this.webView);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.webView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Microsoft.Web.WebView2.WinForms.WebView2 webView;
        private System.Windows.Forms.TextBox addressBar;
        private System.Windows.Forms.Button goButton;
        private System.Windows.Forms.Button btnHistory;
        private System.Windows.Forms.ComboBox comboBoxHistory;
    }
}

