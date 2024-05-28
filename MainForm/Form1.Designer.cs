namespace MainForm
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnPause = new Button();
            btnResume = new Button();
            txtOutput = new TextBox();
            lblOutput = new Label();
            btnBlock = new Button();
            SuspendLayout();
            //
            // btnPause
            //
            btnPause.Location = new Point(12, 43);
            btnPause.Name = "btnPause";
            btnPause.Size = new Size(145, 29);
            btnPause.TabIndex = 0;
            btnPause.Text = "Pause";
            btnPause.UseVisualStyleBackColor = true;
            btnPause.Click += btnPause_Click;
            //
            // btnResume
            //
            btnResume.Location = new Point(216, 43);
            btnResume.Name = "btnResume";
            btnResume.Size = new Size(94, 29);
            btnResume.TabIndex = 1;
            btnResume.Text = "Resume";
            btnResume.UseVisualStyleBackColor = true;
            btnResume.Click += btnResume_Click;
            //
            // txtOutput
            //
            txtOutput.Location = new Point(12, 117);
            txtOutput.Multiline = true;
            txtOutput.Name = "txtOutput";
            txtOutput.ScrollBars = ScrollBars.Vertical;
            txtOutput.Size = new Size(776, 293);
            txtOutput.TabIndex = 2;
            //
            // lblOutput
            //
            lblOutput.AutoSize = true;
            lblOutput.Location = new Point(12, 85);
            lblOutput.Name = "lblOutput";
            lblOutput.Size = new Size(128, 20);
            lblOutput.TabIndex = 3;
            lblOutput.Text = "Heart beat output";
            //
            // btnBlock
            //
            btnBlock.Location = new Point(368, 43);
            btnBlock.Name = "btnBlock";
            btnBlock.Size = new Size(210, 29);
            btnBlock.TabIndex = 4;
            btnBlock.Text = "Block UI for 10 Seconds";
            btnBlock.UseVisualStyleBackColor = true;
            btnBlock.Click += btnBlock_Click;
            //
            // Form1
            //
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(btnBlock);
            Controls.Add(lblOutput);
            Controls.Add(txtOutput);
            Controls.Add(btnResume);
            Controls.Add(btnPause);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }
        #endregion
        private Button btnPause;
        private Button btnResume;
        private TextBox txtOutput;
        private Label lblOutput;
        private Button btnBlock;
    }
}