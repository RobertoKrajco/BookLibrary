using Library.controllers;
using Library.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Library.models
{
    internal class BookLibrary
    {
        private ArrayList books;
        private XMLController xMLController;
        private static readonly log4net.ILog _log = log4net.LogManager.
            GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public ArrayList Books { get => this.books;  }

        /// <summary>
        /// this class store books from library
        /// </summary>
        public BookLibrary()
        {
            this.xMLController = new XMLController();
            this.books = this.xMLController.LoadXMLLibrary();
        }

        /// <summary>
        /// method edits book directly in xml library
        /// </summary>
        /// <param name="id">book ID</param>
        /// <param name="col">which column from datagridview is edited (starts from 0)</param>
        /// <param name="data">edited data</param>
        public void EditXMLLibrary(int id, int col, object data)
        {
            foreach (Book book in this.books)
            {
                if (book.Id == id)
                    switch (col)
                    {

                        case 1:
                            book.Name = data.ToString();
                            this.xMLController.SaveXMLLibrary(this.books);
                            break;
                        case 2:
                            book.Author = data.ToString();
                            this.xMLController.SaveXMLLibrary(this.books);
                            break;
                        case 3:
                            book.AddBorrowing((Borrowing)data);
                            this.xMLController.SaveXMLLibrary(this.books);
                            break;
                        default:
                            return;
                    }


            }

        }

        /// <summary>
        /// editing book stored as object
        /// </summary>
        /// <param name="bookID">book ID</param>
        /// <param name="newBorrowing">new Borrowing</param>
        /// <returns></returns>
        internal bool EditBook(int bookID,Borrowing newBorrowing)
        {
            foreach (Book book in this.books)
            {
                if (book.Id == bookID)
                {
                    book.AddBorrowing(newBorrowing);
                    this.EditXMLLibrary(bookID, 3, newBorrowing);
                    
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// edns borrowing of the book at the current Datetime
        /// </summary>
        /// <param name="bookID">book ID</param>
        internal void EndBorrowing(int bookID)
        {
            foreach (Book book in books)
            {
                if (book.Id == bookID)
                {
                    book.EndActualBorrowing();
                    this.xMLController.EndBorrowing(bookID);
                    _log.Debug("end borrowing of book: " + bookID);
                    return;
                }
            }
        }

        /// <summary>
        /// remove book from library
        /// </summary>
        /// <param name="bookID">book ID</param>
        internal void RemoveBook(int bookID)
        {
            foreach (Book book in books)
            {
                if (book.Id == bookID)
                {
                    this.xMLController.RemoveBookFromLibrary(bookID);
                    books.Remove(book);
                    _log.Debug("removed book: " + bookID);
                    return;
                }
            }
        }

        /// <summary>
        /// add book to library
        /// </summary>
        /// <param name="name">name</param>
        /// <param name="author">author</param>
        internal void AddBook(string name, string author)
        {
            int maxId = 0;
            foreach (Book book in books)
            {
                if (book.Id > maxId)
                {
                    maxId = book.Id;
                }
            }

            Book newBook = new Book(name, author, maxId + 1);
            this.books.Add(newBook);
            this.xMLController.AddBookToLibrary(newBook);
            _log.Debug("book added: " + name +" " + author);
        }
    }
}
