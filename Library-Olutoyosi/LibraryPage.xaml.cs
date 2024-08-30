using System;
using System.Collections.Generic;
using Library_Olutoyosi;
using Xamarin.Forms;

namespace LibraryOlutoyosi
{
    public partial class LibraryPage : ContentPage
    {
        private List<Book> booksList = new List<Book>();
        private string Username = "";

        public LibraryPage(string username)
        {
            InitializeComponent();

            lblUserName.Text = $"Welcome {username}!";
            this.Username = username;

            // get items from database
            LoadBooks();
        }

        async void LoadBooks()
        {
            booksList = await App.MyDb.GetAllBooks();
            lvBooks.ItemsSource = null;
            lvBooks.ItemsSource = booksList;
        }

        async void CheckoutBook(System.Object sender, System.EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext as Book;
            bool result = await contextItem.Checkout(this.Username);
            if (result)
            {
                // success pop up
                await DisplayAlert("Success", "Done!", "OK");
            }
            else
            {
                // error popup
                string message = "";
                if(this.Username == contextItem.Borrower)
                {
                    message = "You already checked this book out";
                }else if (this.Username != contextItem.Borrower)
                {
                    message = $"This book is already checked out by {contextItem.Borrower}";
                }
                else
                {
                    message = "This book cannot be checked out";
                }
                await DisplayAlert("ERROR", message, "OK");
            }
        }

        async void ReturnBook(System.Object sender, System.EventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            var contextItem = menuItem.BindingContext as Book;
            bool result = await contextItem.Return(this.Username);
            if (result)
            {
                // success pop up
                await DisplayAlert("Success", "Done!", "OK");

            }
            else
            {
                //error pop up
                string message = "";
                if (this.Username != contextItem.Borrower && contextItem.Borrower != "" && contextItem.Borrower != null)
                {
                    message = $"This book needs to be returned by {contextItem.Borrower}";
                }
                else
                {
                    message = "This book cannot be returned";
                }
                await DisplayAlert("ERROR", message, "OK");
            }
        }

        void lvBooks_ItemSelected(System.Object sender, Xamarin.Forms.SelectedItemChangedEventArgs e)
        {
            lblResult.IsVisible = true;
            if (e.SelectedItem != null && e.SelectedItem is Book selectedBook)
            {
                itemInteraction(selectedBook);
            }
        }

        void lvBooks_ItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            lblResult.IsVisible = true;
            if (e.Item != null && e.Item is Book selectedBook)
            {
                itemInteraction(selectedBook);
            }
        }

        // make one function to run with both tapped and selected 
        public void itemInteraction(Book selectedBook)
        {
            if (selectedBook.BorrowingStatus == false)
            {
                lblResult.Text = $"{selectedBook.Title} is available for borrowing.";
            }
            else
            {

                if (this.Username == selectedBook.Borrower)
                {
                    lblResult.Text = $"You checked out this book!";
                }
                else
                {
                    lblResult.Text = $"The book is checked out by {selectedBook.Borrower}";

                }
            }
        }
    }
}

