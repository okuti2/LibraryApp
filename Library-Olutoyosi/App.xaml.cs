using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LibraryOlutoyosi;
using System.Threading.Tasks;

namespace Library_Olutoyosi
{
    public partial class App : Application
    {
        // create an instance of the Database class
        // implement it as a singleton
        private static Database db;
        public static Database MyDb
        {
            get
            {
                if (db == null)
                {
                    db = new Database();
                    
                }
                return db;
            }
        }
        public App ()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());


        }

        private async Task AddInitialBooksAsync()
        {
            // Check if the database already has books
            var books = await MyDb.GetAllBooks();
            if (books.Count == 0)
            {
                await MyDb.AddBook(new Book { ISBN = "978-3-16-148410-0", Title = "The Hunger Games", Author = "Suzanne Collins" });
                await MyDb.AddBook(new Book { ISBN = "978-1-4028-9462-6", Title = "Children of Blood and Bone", Author = "Tomi Adeyemi" });
                await MyDb.AddBook(new Book { ISBN = "978-0-12-374856-0", Title = "Nearly All the Men in Lagos are Mad", Author = "Damilare Kuku" });
                await MyDb.AddBook(new Book { ISBN = "978-1-56-682051-0", Title = "Wahala", Author = "Nikki May" });
                await MyDb.AddBook(new Book { ISBN = "978-5-28-68404-0", Title = "Game of Thrones", Author = "George R.R. Martin" });

            }
        }

        protected override void OnStart ()
        {
            Task.Run(async () => await AddInitialBooksAsync()).Wait();

        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

