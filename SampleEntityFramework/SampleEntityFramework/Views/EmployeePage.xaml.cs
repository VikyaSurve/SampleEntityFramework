using SampleEntityFramework.Helpers;
using SampleEntityFramework.Models;
using SampleEntityFramework.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleEntityFramework.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmployeePage : ContentPage
    {
        EmployeesViewModel employeeVM;
        public EmployeePage(IEmployeesRepository EmployeesRepository)
        {
            InitializeComponent();
            this.BindingContext = employeeVM = new EmployeesViewModel(EmployeesRepository);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            employeeVM.RefreshCommand.Execute(null);
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var data = e.Item as Employee;
            employeeVM.selectedEmployee = data;
            employeeVM.EmployeeName = data.Name;
            employeeVM.EmployeeEmailAddress = data.EmailAddress;
            saveUpdateButton.Text = "Update";
        }

        private void SaveUpdateButton_Clicked(object sender, EventArgs e)
        {
            if (saveUpdateButton.Text == "Save")
            {
                employeeVM.AddCommand.Execute(null);
            }
            else
            {
                employeeVM.UpdateCommand.Execute(employeeVM.selectedEmployee);
                saveUpdateButton.Text = "Save";
            }
        }
    }
}