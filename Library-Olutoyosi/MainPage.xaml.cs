using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using LibraryOlutoyosi;

namespace Library_Olutoyosi
{
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            string enteredUser = username.Text;
            string enteredPassword = password.Text;
            if (!string.IsNullOrWhiteSpace(enteredUser) && !string.IsNullOrWhiteSpace(enteredPassword))
            {
                var userDictionary = new Dictionary<string, string>
                {
                    {"peter", "1234" },
                    {"mary", "0000" }
                };

                if(userDictionary.TryGetValue(enteredUser, out string storedPassword) && storedPassword == enteredPassword)
                {
                    lblError.IsVisible = false;
                    await Navigation.PushAsync(new LibraryPage(username.Text));
                    Navigation.PushAsync(new LibraryPage(username.Text));
                    username.Text = "";
                    password.Text = "";
                }
                else
                {
                    lblError.Text = "ERROR: Wrong username or password";
                    lblError.BackgroundColor = Color.FromHex("#F8D7DA");
                    lblError.IsVisible = true;
                }
            }
            else
            {
                lblError.Text = "ERROR: Username or password cannot be empty.";
                lblError.BackgroundColor = Color.FromHex("#F8D7DA");
                lblError.IsVisible = true;
            }
        }
    }
}

