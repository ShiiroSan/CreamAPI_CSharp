using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;

namespace CreamAPI_CSharp
{
    public partial class GameSelecForm : Form
    {
        public static string gameName = "";

        private ArrayList ListOfListTOArrayList(List<List<string>> toConvert)
        {
            ArrayList gameList = new ArrayList();
            for (int i = 0; i < toConvert.Count; i++)
            {
                gameList.Add(new gameList(toConvert[i][0], toConvert[i][2]));
            }
            return gameList;
        }

        public GameSelecForm(List<List<string>> vs)
        {
            if (vs.Count == 1)
            {
                int iAppID = int.Parse(vs[0][0]); //Add tryParse, prevent error
                gameName = vs[0][2];
                this.Hide();
                (new DLCLister(iAppID)).ShowDialog();
            }
            else
            {
                InitializeComponent();
                gameSelecListBox.DataSource = ListOfListTOArrayList(vs);

                gameSelecListBox.DisplayMember = "GameName";
                gameSelecListBox.ValueMember = "GameAppID";
            }
        }

        private void quitSelecGameBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void selecGameBtn_Click(object sender, EventArgs e)
        {
            if (gameSelecListBox.SelectedItem == null)
            {
                MessageBox.Show("Error!", "You should select a game before proceeding.", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (!int.TryParse(gameSelecListBox.SelectedValue.ToString(), out int iAppID))
                {
                    MessageBox.Show("Cannot get the game appid. This means that an issue happened. Try again, or verify that you have a correct internet connection.",
                        "Error!",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                this.Hide();
                gameName = gameSelecListBox.Text;
                (new DLCLister(iAppID)).ShowDialog();
            }
        }
    }
}