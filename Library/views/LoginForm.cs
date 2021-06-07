using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public partial class LoginForm : Form
    {
        private string username;
        private string password;

        public string Password { get => password; set => password = value; }
        public string Username { get => username; set => username = value; }

        public LoginForm()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            this.username = UsernameTextBox.Text;
            this.password = PasswordTextBox.Text;
            Login(this.username, this.password);
            
        }

        public bool Login(string username,string password)
        {
            if ((username == "") || (password == ""))
            {
                MessageBox.Show("login credentials are empty");
            }
            else if (password == ConfigurationManager.AppSettings[username])
            {
                //MessageBox.Show("you are granted with access");
                this.Hide();
                Form libraryForm = new LibraryForm();
                libraryForm.Show();
                libraryForm.FormClosed += new FormClosedEventHandler(FormOpen);
                return true;
            }
            else
            {
                MessageBox.Show("you are not granted with access");
            }
            return false;
        }

        void FormOpen(object sender, EventArgs e)
        {
            this.Show();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }
    }
}
