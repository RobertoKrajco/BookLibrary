using Library.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace Library
{
    class Book
    {
        private String name;
        private String author;
        // this is an actual borrowing
        private Borrowing borrowing;
        private bool borrowed =false;
        private int id;
        private ArrayList borrowings;

        public string Name { get => name; set => name = value; }
        public string Author { get => author; set => author = value; }
        public int Id { get => id; set => id = value; }
        public bool Borrowed { get => borrowed; set => borrowed = value; }
        internal Borrowing Borrowing { get => borrowing; }

        /// <summary>
        /// create book from given parameters
        /// </summary>
        /// <param name="name">Book name</param>
        /// <param name="author">Book author</param>
        /// <param name="borrowed">XElement describes borrowing</param>
        /// <param name="id">book ID</param>
        public Book(string name, string author, XElement borrowed, int id)
        {
            this.name = name;
            this.author = author;
            this.borrowings = new ArrayList();
            if (borrowed.Element("From").Value != "" && borrowed.Element("FirstName").Value != "")
            {
                User user = new User(borrowed.Element("FirstName").Value, borrowed.Element("LastName").Value);
                this.borrowing = new Borrowing(user, DateTime.Parse(borrowed.Element("From").Value));
                this.borrowed = true;
                this.borrowings.Add(this.borrowing);
            }
            this.Id = id;
            
        }

        /// <summary>
        /// create book from given parameters without Borrowing
        /// </summary>
        /// <param name="name"></param>
        /// <param name="author"></param>
        /// <param name="id"></param>
        public Book(string name, string author, int id)
        {
            this.name = name;
            this.author = author;
            this.borrowings = new ArrayList();
            this.borrowed = false;
            this.Id = id;

        }

        /// <summary>
        /// create new borrowing and end actual borrowing
        /// </summary>
        /// <param name="borrowing"></param>
        public void AddBorrowing(Borrowing borrowing)
        {
            if (this.borrowing != null)
            this.borrowing.endBorrowing();
            this.borrowing = borrowing;
            this.borrowings.Add(this.borrowing);
            this.borrowed = true;
        }

        /// <summary>
        /// end of actual Borrowing
        /// </summary>
        public void EndActualBorrowing()
        {
            if (this.borrowing != null)
                this.borrowing.endBorrowing();
            this.borrowed = false;

        }

        
    }
}
