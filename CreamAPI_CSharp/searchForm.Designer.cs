namespace CreamAPI_CSharp
{
    partial class searchForm
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
            this.gameNameInput = new System.Windows.Forms.TextBox();
            this.gameSearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // gameNameInput
            // 
            this.gameNameInput.Location = new System.Drawing.Point(12, 12);
            this.gameNameInput.Name = "gameNameInput";
            this.gameNameInput.Size = new System.Drawing.Size(298, 20);
            this.gameNameInput.TabIndex = 0;
            this.gameNameInput.Text = "Write name to search here...";
            this.gameNameInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gameSearch
            // 
            this.gameSearch.Location = new System.Drawing.Point(12, 38);
            this.gameSearch.Name = "gameSearch";
            this.gameSearch.Size = new System.Drawing.Size(298, 34);
            this.gameSearch.TabIndex = 1;
            this.gameSearch.Text = "Search for this game!";
            this.gameSearch.UseVisualStyleBackColor = true;
            this.gameSearch.Click += new System.EventHandler(this.gameSearch_ClickAsync);
            // 
            // searchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 84);
            this.Controls.Add(this.gameSearch);
            this.Controls.Add(this.gameNameInput);
            this.Name = "searchForm";
            this.Text = "Game name to search";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button gameSearch;
        private System.Windows.Forms.TextBox gameNameInput;
    }
}