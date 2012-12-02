using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MovieOrganizer
{
    public partial class HomeForm : Form
    {
        Form login;
        public HomeForm(object sender)
        {
            login = (Form)sender;
            InitializeComponent();
        }

        private void HomeForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            login.Close();
        }
    }
}
