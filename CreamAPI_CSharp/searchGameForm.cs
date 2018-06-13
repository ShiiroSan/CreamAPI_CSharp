using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CreamAPI_CSharp
{
    public partial class searchGameForm : Form
    {
        public searchGameForm()
        {
            InitializeComponent();
        }

        private void searchGameBtn_Click(object sender, EventArgs e)
        {
            if (searchGameInputBox.Text == "" || searchGameInputBox.Text == "Write game name to search here...")
            {
                MessageBox.Show("You must specify a game name!", "Error!");
                return;
            }

        }
    }
}
