using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Library.views
{
    public partial class EditForm : Form
    {
        private string firstname;
        private string lastname;
        private DateTime from;

        public EditForm()
        {
            InitializeComponent();
        }

        public string Firstname { get => firstname; }
        public string Lastname { get => lastname;}
        public DateTime From { get => from; }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            this.firstname = this.FirstnameTextBox.Text.ToString();
            this.lastname = this.LastnameTextBox.Text.ToString();
            this.from = this.dateTimePicker1.Value;
            this.Close();
        }

        
    }
}
