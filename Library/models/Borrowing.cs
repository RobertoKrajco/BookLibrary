using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Library.models
{
    class Borrowing
    {
        private User user;
        private DateTime from;
        private DateTime to;

        public DateTime To { get => to; set => to = value; }
        public DateTime From { get => from; set => from = value; }
        internal User User { get => user; set => user = value; }

        public Borrowing(User user, DateTime from, DateTime to)
        {
            this.user = user;
            this.from = from;
            this.to = to;
        }

        public Borrowing(User user, DateTime from)
        {
            this.user = user;
            this.from = from;
        }


        public void endBorrowing()
        {
            this.to = new DateTime();

        }
    }
}
