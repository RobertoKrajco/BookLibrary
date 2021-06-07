using Library.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Library
{
    class User
    {
        private String firstname;
        private String lastname;
        private ArrayList borrowings;

        public User(string firstname, string lastname)
        {
            this.firstname = firstname;
            this.lastname = lastname;
        }

        public string Firstname { get => firstname; set => firstname = value; }
        public string Lastname { get => lastname; set => lastname = value; }
        public ArrayList Borrowings { get => borrowings; set => borrowings = value; }

        public void addBorrowing(Borrowing borrowing)
        {
            Borrowings.Add(borrowing);
        }
    }
}
