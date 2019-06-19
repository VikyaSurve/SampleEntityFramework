using SampleEntityFramework.Helpers;
using SampleEntityFramework.ViewModels;
using SampleEntityFramework.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleEntityFramework
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }
        public App(IEmployeesRepository EmployeesRepository)
        {
            InitializeComponent();
            var EmployeesPage = new EmployeePage(EmployeesRepository) ; 
            MainPage = new NavigationPage(EmployeesPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
