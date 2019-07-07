using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace CreamAPI_CSharp
{
    public partial class searchForm : Form
    {
        public const string urlRoot = "https://steamdb.info";

        public searchForm()
        {
            InitializeComponent();
        }

        public void setGameNameInputText(string str)
        {
            gameNameInput.Text = str;
        }

        private async void gameSearch_ClickAsync(object sender, EventArgs e)
        {
            if (gameNameInput.Text == "" || gameNameInput.Text == "Write name to search here...")
            {
                MessageBox.Show("You must enter a game name before searching!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                string gameName = gameNameInput.Text;
                string pattern = @"\s";
                string substitution = @"+";
                Regex regex = new Regex(pattern);
                gameName = regex.Replace(gameName, substitution);
                string url = $"{urlRoot}/search/?a=app&q={gameName}&type=1&category=0";
                List<List<string>> table = WebGestion.GetTableFromPages(url);
                if (table == null)
                {
                    MessageBox.Show("This game wasn't found... ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
                this.Hide();
                (new GameSelecForm(table)).ShowDialog();
            }
        }
    }
}