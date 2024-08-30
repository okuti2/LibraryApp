using System;
using SQLite;
using Library_Olutoyosi;
using System.Threading.Tasks;


namespace LibraryOlutoyosi
{
	public class Book
	{
        // define a primary key
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        // properties
        public string ISBN { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool BorrowingStatus { get; set; }
        public string Borrower { get; set; }

        public Book()
		{

        }

        public Book(string isbn, string title, string author)
        {
            this.ISBN = isbn;
            this.Title = title;
            this.Author = author;
            this.BorrowingStatus = false;
            this.Borrower = "";
        }

        public override string ToString()
        {
            return $"The Author of {this.Title} is {this.Author}";
        }

        public async Task<bool> Checkout(string borrower)
        {
            if(this.BorrowingStatus == false)
            {
                this.BorrowingStatus = true;
                this.Borrower = borrower;
                await App.MyDb.UpdateItem(this);
                return true;
            }
            return false;
        }

        public async  Task<bool> Return(string username)
        {
            if(this.BorrowingStatus == true && username == this.Borrower)
            {
                this.BorrowingStatus = false;
                this.Borrower = "";
                await App.MyDb.UpdateItem(this);
                return true;
            }
            return false;
        }


    }
}

