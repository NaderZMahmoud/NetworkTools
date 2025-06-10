namespace NetworkTool;

partial class Form1
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    ///  Clean up any resources being used.
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
    ///  Required method for Designer support - do not modify
    ///  the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
        this.httpCheckerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.endpointCheckerMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        this.httpCheckerControl = new NetworkTool.HttpCheckerControl();
        this.endpointCheckerControl = new NetworkTool.EndpointCheckerControl();
        this.SuspendLayout();
        // 
        // mainMenuStrip
        // 
        this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.httpCheckerMenuItem,
            this.endpointCheckerMenuItem});
        this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
        this.mainMenuStrip.Name = "mainMenuStrip";
        this.mainMenuStrip.Size = new System.Drawing.Size(600, 24);
        this.mainMenuStrip.TabIndex = 0;
        this.mainMenuStrip.Text = "menuStrip1";
        // 
        // httpCheckerMenuItem
        // 
        this.httpCheckerMenuItem.Name = "httpCheckerMenuItem";
        this.httpCheckerMenuItem.Size = new System.Drawing.Size(97, 20);
        this.httpCheckerMenuItem.Text = "HTTP Checker";
        this.httpCheckerMenuItem.Click += new System.EventHandler(this.httpCheckerMenuItem_Click);
        // 
        // endpointCheckerMenuItem
        // 
        this.endpointCheckerMenuItem.Name = "endpointCheckerMenuItem";
        this.endpointCheckerMenuItem.Size = new System.Drawing.Size(120, 20);
        this.endpointCheckerMenuItem.Text = "Endpoint Checker";
        this.endpointCheckerMenuItem.Click += new System.EventHandler(this.endpointCheckerMenuItem_Click);
        // 
        // httpCheckerControl
        // 
        this.httpCheckerControl.Dock = System.Windows.Forms.DockStyle.Fill;
        this.httpCheckerControl.Location = new System.Drawing.Point(0, 24);
        this.httpCheckerControl.Name = "httpCheckerControl";
        this.httpCheckerControl.Size = new System.Drawing.Size(600, 376);
        this.httpCheckerControl.TabIndex = 1;
        // 
        // endpointCheckerControl
        // 
        this.endpointCheckerControl.Dock = System.Windows.Forms.DockStyle.Fill;
        this.endpointCheckerControl.Location = new System.Drawing.Point(0, 24);
        this.endpointCheckerControl.Name = "endpointCheckerControl";
        this.endpointCheckerControl.Size = new System.Drawing.Size(600, 376);
        this.endpointCheckerControl.TabIndex = 2;
        this.endpointCheckerControl.Visible = false;
        // 
        // Form1
        // 
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(600, 400);
        this.Controls.Add(this.httpCheckerControl);
        this.Controls.Add(this.endpointCheckerControl);
        this.Controls.Add(this.mainMenuStrip);
        this.MainMenuStrip = this.mainMenuStrip;
        this.Name = "Form1";
        this.Text = "Network Tool";
        this.BackColor = System.Drawing.SystemColors.Control;
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    #endregion

    private MenuStrip mainMenuStrip;
    private ToolStripMenuItem httpCheckerMenuItem;
    private ToolStripMenuItem endpointCheckerMenuItem;
    private HttpCheckerControl httpCheckerControl;
    private EndpointCheckerControl endpointCheckerControl;
}
