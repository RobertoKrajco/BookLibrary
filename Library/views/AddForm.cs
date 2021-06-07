using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Library.views
{

    public partial class AddForm : Form
    {

        private string name;
        private string author;
        public string BookName { get => name; }
        public string Author { get => author; }

        public AddForm()
        {
            InitializeComponent();
        }

        private void AddBookButton_Click(object sender, EventArgs e)
        {
            this.name = this.BookNameTextBox.Text.ToString();
            this.author = this.AuthorTextBox.Text.ToString();
            this.Close();
        }
    }
}
