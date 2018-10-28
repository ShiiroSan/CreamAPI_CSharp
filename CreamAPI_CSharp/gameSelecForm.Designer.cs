namespace CreamAPI_CSharp
{
    partial class GameSelecForm
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
            this.gameSelecListBox = new System.Windows.Forms.ListBox();
            this.selecGameBtn = new System.Windows.Forms.Button();
            this.quitSelecGameBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameSelecListBox
            // 
            this.gameSelecListBox.FormattingEnabled = true;
            this.gameSelecListBox.Location = new System.Drawing.Point(13, 13);
            this.gameSelecListBox.Name = "gameSelecListBox";
            this.gameSelecListBox.Size = new System.Drawing.Size(311, 316);
            this.gameSelecListBox.TabIndex = 0;
            // 
            // selecGameBtn
            // 
            this.selecGameBtn.Location = new System.Drawing.Point(13, 336);
            this.selecGameBtn.Name = "selecGameBtn";
            this.selecGameBtn.Size = new System.Drawing.Size(156, 33);
            this.selecGameBtn.TabIndex = 1;
            this.selecGameBtn.Text = "Select game";
            this.selecGameBtn.UseVisualStyleBackColor = true;
            this.selecGameBtn.Click += new System.EventHandler(this.selecGameBtn_Click);
            // 
            // quitSelecGameBtn
            // 
            this.quitSelecGameBtn.Location = new System.Drawing.Point(175, 336);
            this.quitSelecGameBtn.Name = "quitSelecGameBtn";
            this.quitSelecGameBtn.Size = new System.Drawing.Size(149, 33);
            this.quitSelecGameBtn.TabIndex = 2;
            this.quitSelecGameBtn.Text = "Quit";
            this.quitSelecGameBtn.UseVisualStyleBackColor = true;
            this.quitSelecGameBtn.Click += new System.EventHandler(this.quitSelecGameBtn_Click);
            // 
            // gameSelecForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(336, 374);
            this.Controls.Add(this.quitSelecGameBtn);
            this.Controls.Add(this.selecGameBtn);
            this.Controls.Add(this.gameSelecListBox);
            this.Name = "gameSelecForm";
            this.Text = "gameSelecForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox gameSelecListBox;
        private System.Windows.Forms.Button selecGameBtn;
        private System.Windows.Forms.Button quitSelecGameBtn;
    }
}