using Library.controllers;
using Library.models;
using Library.views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;

namespace Library
{
    public partial class LibraryForm : Form
    {
        private BookLibrary library;
        
        private const string BORROWED = "Borrowed";
        private const string ALL = "All";
        private const string AVAILABLE = "Available";
        private const string NOFILTER = "NoFilter";

        public LibraryForm()
        {
            InitializeComponent();
            this.library = new BookLibrary();
        }

        private void LibraryForm_Load(object sender, EventArgs e)
        {
            dataGridView1_filter(NOFILTER);
            Console.WriteLine("loaded");
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            string data = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString();
            int id = Int32.Parse(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value.ToString());
            this.library.EditXMLLibrary(id, e.ColumnIndex, data);
        }

        private void FilterButton_Click(object sender, EventArgs e)
        {

            dataGridView1_filter(comboBox1.SelectedItem == null ? NOFILTER : comboBox1.SelectedItem.ToString());
           
        }

        private void dataGridView1_filter(string filter)
        {
            //this.books = this.xmlController.LoadXMLLibrary();
            dataGridView1.Rows.Clear();
            foreach (Book book in this.library.Books)
            {
                if (filter == ALL || filter == NOFILTER || (book.Borrowed && filter == BORROWED) || (!book.Borrowed && filter == AVAILABLE))
                {
                    int row = dataGridView1.Rows.Add();
                    dataGridView1.Rows[row].Cells["ID"].Value = book.Id;
                    dataGridView1.Rows[row].Cells["BookName"].Value = book.Name;
                    dataGridView1.Rows[row].Cells["Author"].Value = book.Author;

                    if (book.Borrowed)
                    {
                        dataGridView1.Rows[row].Cells["Firstname"].Value = book.Borrowing.User.Firstname;
                        dataGridView1.Rows[row].Cells["Lastname"].Value = book.Borrowing.User.Lastname;
                        dataGridView1.Rows[row].Cells["From"].Value = book.Borrowing.From.ToString("dd.MM.yyyy");
                    }
                }
            }
        }

        private void EditButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
                MessageBox.Show("Please select 1 row with book you want to edit");
            else
            {
                Form editForm = new EditForm();
                editForm.Show();
                editForm.FormClosing += new FormClosingEventHandler(EditBook);
            }
        }

        private void EditBook(object sender, EventArgs e)
        {
            Console.WriteLine(sender);
            string firstname = ((EditForm)sender).Firstname;
            string lastname = ((EditForm)sender).Lastname;
            DateTime from = ((EditForm)sender).From;
            int bookID = Int32.Parse(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
            User newUser = new User(firstname, lastname);
            if(firstname != "" || lastname != "")
                if(this.library.EditBook(bookID, new Borrowing(newUser, from)))
                    dataGridView1_filter(comboBox1.SelectedItem == null ? NOFILTER : comboBox1.SelectedItem.ToString());
        }

        private void EndBorrowingButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
                MessageBox.Show("Please select 1 row with borrowing you want to end ");
            else
            {
                int bookID = Int32.Parse(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                this.library.EndBorrowing(bookID);
                dataGridView1_filter(comboBox1.SelectedItem == null ? NOFILTER : comboBox1.SelectedItem.ToString());
               
            }
        }

        private void RemoveButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count != 1)
                MessageBox.Show("Please select 1 row with borrowing you want to end ");
            else
            {
                int bookID = Int32.Parse(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                this.library.RemoveBook(bookID);
                dataGridView1_filter(comboBox1.SelectedItem == null ? NOFILTER : comboBox1.SelectedItem.ToString());

            }
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            Form addForm = new AddForm();
            addForm.Show();
            addForm.FormClosing += new FormClosingEventHandler(AddBook);
        }

        private void AddBook(object sender, FormClosingEventArgs e)
        {
            string name = ((AddForm)sender).BookName;
            string author = ((AddForm)sender).Author;
            this.library.AddBook(name,author);
            dataGridView1_filter(comboBox1.SelectedItem == null ? NOFILTER : comboBox1.SelectedItem.ToString());
        }
    }
}
