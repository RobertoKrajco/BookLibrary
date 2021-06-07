using Library.models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Library.controllers
{
    class XMLController
    {
      
        private IEnumerable<XElement> XMLLibrary;
        private XDocument xmlFile;
        private static readonly log4net.ILog _log = log4net.LogManager.
            GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public XMLController()
        {
            LoadXMLLibrary();
        }


        /// <summary>
        /// Loads books from xml and add them to ArrayList.
        /// </summary>
        /// <returns>ArrayList of books</returns>
        public ArrayList LoadXMLLibrary()
        {
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configLocation = ConfigurationManager.AppSettings["XML Library Source"].ToString();
            string file = Path.Combine(appData, configLocation);

            System.Text.EncodingProvider ppp = System.Text.CodePagesEncodingProvider.Instance;
            Encoding.RegisterProvider(ppp);
            this.xmlFile = XDocument.Load(file);
            this.XMLLibrary = from c in xmlFile.Elements("Library").Elements("Book")
                        select c;
            ArrayList books = new ArrayList();
            foreach (XElement book in this.XMLLibrary)
            {
                Book objBook = new Book(book.Element("Name").Value, book.Element("Author").Value, 
                    book.Element("Borrowed"), Int32.Parse(book.Attribute("id").Value));
                books.Add(objBook);
                //book.Attribute("Name").Value = "MyNewValue";
            }
            return books;
        }

        /// <summary>
        /// save xml library of given books
        /// </summary>
        /// <param name="books"></param>
        public void SaveXMLLibrary(ArrayList books)
        {
            MapBooksToLibrary(books);
            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configLocation = ConfigurationManager.AppSettings["XML Library Source"].ToString();
            string file = Path.Combine(appData, configLocation);
            this.xmlFile.Save(file);
        }

        /// <summary>
        /// maps ArrayList of books to xml library
        /// </summary>
        /// <param name="books"></param>
        public void MapBooksToLibrary(ArrayList books)
        {
            foreach (Book book in books)
            {
                foreach(XElement xmlBook in this.XMLLibrary)
                {
                    if(book.Id == Int32.Parse(xmlBook.Attribute("id").Value))
                    {
                        Book objBook = new Book(xmlBook.Element("Name").Value, xmlBook.Element("Author").Value,
                         xmlBook.Element("Borrowed"), Int32.Parse(xmlBook.Attribute("id").Value));
                        if (objBook != book)
                        {
                            if(objBook.Name != book.Name)
                            {
                                xmlBook.Element("Name").Value = book.Name;
                            }
                            if (objBook.Author != book.Author)
                            {
                                xmlBook.Element("Author").Value = book.Author;
                            }
                            if (objBook.Borrowing != book.Borrowing)
                            {
                                xmlBook.Element("Borrowed").Element("FirstName").Value = book.Borrowing.User.Firstname;
                                xmlBook.Element("Borrowed").Element("LastName").Value = book.Borrowing.User.Lastname;
                                xmlBook.Element("Borrowed").Element("From").Value = book.Borrowing.From.Date.ToString("dd.MM.yyyy");
                            }
                        }
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// end borrowing in xml library
        /// </summary>
        /// <param name="bookID"></param>
        public void EndBorrowing(int bookID)
        {
            foreach (XElement xmlBook in this.XMLLibrary)
            {
                if (bookID == Int32.Parse(xmlBook.Attribute("id").Value))
                {
                    xmlBook.Element("Borrowed").Element("FirstName").Value = "";
                    xmlBook.Element("Borrowed").Element("LastName").Value ="";
                    xmlBook.Element("Borrowed").Element("From").Value ="";
                    string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string configLocation = ConfigurationManager.AppSettings["XML Library Source"].ToString();
                    string file = Path.Combine(appData, configLocation);
                    this.xmlFile.Save(file);
                    break;
                }
            }
        }

        /// <summary>
        /// remove book from xml library
        /// </summary>
        /// <param name="bookID"></param>
        public void RemoveBookFromLibrary(int bookID)
        {
            foreach (XElement xmlBook in this.XMLLibrary)
            {
                if (bookID == Int32.Parse(xmlBook.Attribute("id").Value))
                {
                    xmlBook.Remove();
                    string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    string configLocation = ConfigurationManager.AppSettings["XML Library Source"].ToString();
                    string file = Path.Combine(appData, configLocation);
                    this.xmlFile.Save(file);
                    break;
                }
            }


        }

        /// <summary>
        /// addd book to xml library
        /// </summary>
        /// <param name="book"></param>
        public void AddBookToLibrary(Book book)
        {
            XElement root = new XElement("Book");
            root.Add(new XAttribute("id", book.Id));
            root.Add(new XElement("Name", book.Name));
            root.Add(new XElement("Author", book.Author));
            XElement borrowed = new XElement("Borrowed");
            borrowed.Add(new XElement("FirstName", ""));
            borrowed.Add(new XElement("LastName", ""));
            borrowed.Add(new XElement("From", ""));
            root.Add(borrowed);
            this.xmlFile.Element("Library").Add(root);

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string configLocation = ConfigurationManager.AppSettings["XML Library Source"].ToString();
            string file = Path.Combine(appData, configLocation);
            this.xmlFile.Save(file);

        }
       
    }
}
