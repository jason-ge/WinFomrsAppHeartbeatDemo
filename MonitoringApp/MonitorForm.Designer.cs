namespace MonitoringApp
{
    partial class MonitorForm
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            lblOutput = new Label();
            txtOutput = new TextBox();
            SuspendLayout();
            //
            // lblOutput
            //
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(12, 18);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(128, 20);
            lblOutput.TabIndex = 0;
            lblOutput.Text = "Monitoring status:";
            //
            // txtOutput
            //
            txtOutput.Location = new Point(12, 51);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(776, 361);
            txtOutput.TabIndex = 1;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(txtOutput);
            Controls.Add(lblOutput);
            Name = "Form1";
            Text = "Form1";
            FormClosing += Form1_FormClosing;
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
        private Label lblOutput;
        private TextBox txtOutput;
    }
}