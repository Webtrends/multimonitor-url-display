namespace MultiMonitorURLDisplayTool
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
            this.btnKillThreads = new System.Windows.Forms.Button();
            this.Button2 = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtNumMon = new System.Windows.Forms.TextBox();
            this.btnGo = new System.Windows.Forms.Button();
            this.Button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnKillThreads
            // 
            this.btnKillThreads.Location = new System.Drawing.Point(127, 302);
            this.btnKillThreads.Name = "btnKillThreads";
            this.btnKillThreads.Size = new System.Drawing.Size(75, 23);
            this.btnKillThreads.TabIndex = 11;
            this.btnKillThreads.Text = "Kill Threads";
            this.btnKillThreads.UseVisualStyleBackColor = true;
            this.btnKillThreads.Click += new System.EventHandler(this.btnKillThreads_Click);
            // 
            // Button2
            // 
            this.Button2.Location = new System.Drawing.Point(672, 12);
            this.Button2.Name = "Button2";
            this.Button2.Size = new System.Drawing.Size(75, 23);
            this.Button2.TabIndex = 10;
            this.Button2.Text = "Exit App";
            this.Button2.UseVisualStyleBackColor = true;
            this.Button2.Click += new System.EventHandler(this.Button2_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Location = new System.Drawing.Point(8, 15);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(122, 13);
            this.Label1.TabIndex = 9;
            this.Label1.Text = "Num Monitors Detected:";
            // 
            // txtNumMon
            // 
            this.txtNumMon.Location = new System.Drawing.Point(127, 12);
            this.txtNumMon.Name = "txtNumMon";
            this.txtNumMon.ReadOnly = true;
            this.txtNumMon.Size = new System.Drawing.Size(20, 20);
            this.txtNumMon.TabIndex = 8;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(11, 302);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(75, 23);
            this.btnGo.TabIndex = 7;
            this.btnGo.Text = "Go";
            this.btnGo.UseVisualStyleBackColor = true;
            this.btnGo.Click += new System.EventHandler(this.btnGo_Click);
            // 
            // Button1
            // 
            this.Button1.Location = new System.Drawing.Point(672, 302);
            this.Button1.Name = "Button1";
            this.Button1.Size = new System.Drawing.Size(75, 23);
            this.Button1.TabIndex = 6;
            this.Button1.Text = "Configure";
            this.Button1.UseVisualStyleBackColor = true;
            this.Button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(756, 338);
            this.Controls.Add(this.btnKillThreads);
            this.Controls.Add(this.Button2);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtNumMon);
            this.Controls.Add(this.btnGo);
            this.Controls.Add(this.Button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnKillThreads;
        internal System.Windows.Forms.Button Button2;
        internal System.Windows.Forms.Label Label1;
        internal System.Windows.Forms.TextBox txtNumMon;
        internal System.Windows.Forms.Button btnGo;
        internal System.Windows.Forms.Button Button1;
    }
}

