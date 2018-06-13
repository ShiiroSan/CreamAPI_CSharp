namespace CreamAPI_CSharp
{
    partial class searchGameForm
    {
        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(searchGameForm));
            this.searchGameInputBox = new System.Windows.Forms.TextBox();
            this.searchGameBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // searchGameInputBox
            // 
            resources.ApplyResources(this.searchGameInputBox, "searchGameInputBox");
            this.searchGameInputBox.Name = "searchGameInputBox";
            // 
            // searchGameBtn
            // 
            resources.ApplyResources(this.searchGameBtn, "searchGameBtn");
            this.searchGameBtn.Name = "searchGameBtn";
            this.searchGameBtn.UseVisualStyleBackColor = true;
            this.searchGameBtn.Click += new System.EventHandler(this.searchGameBtn_Click);
            // 
            // searchGameForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.searchGameBtn);
            this.Controls.Add(this.searchGameInputBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "searchGameForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox searchGameInputBox;
        private System.Windows.Forms.Button searchGameBtn;
    }
}

