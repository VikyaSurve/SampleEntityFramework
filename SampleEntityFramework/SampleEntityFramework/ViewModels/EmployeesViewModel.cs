using SampleEntityFramework.Helpers;
using SampleEntityFramework.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SampleEntityFramework.ViewModels
{
    public class EmployeesViewModel : INotifyPropertyChanged
    {
        private readonly IEmployeesRepository _EmployeesRepository;
        private ObservableCollection<Employee> _Employees;
        private string _employeeEmailAddress;
        private string _employeeName;
        public Employee selectedEmployee { get; set; }
        public ObservableCollection<Employee> Employees
        {
            get
            {
                return _Employees;
            }
            set
            {
                _Employees = value;
                OnPropertyChanged();
            }
        }

        public string EmployeeEmailAddress
        {
            get
            {
                return _employeeEmailAddress;
            }
            set
            {
                _employeeEmailAddress = value;
                OnPropertyChanged();
            }
        }

        public string EmployeeName
        {
            get
            {
                return _employeeName;
            }
            set
            {
                _employeeName = value;
                OnPropertyChanged();
            }
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var data = await _EmployeesRepository.GetEmployeesAsync();
                    Employees = new ObservableCollection<Employee>(data);
                    ClearCommand.Execute(null);
                });
            }
        }
        public ICommand ClearCommand
        {
            get
            {
                return new Command(async () =>
                {
                    EmployeeEmailAddress = string.Empty;
                    EmployeeName = string.Empty;
                });
            }
        }
        public ICommand SyncCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var Employees = await _EmployeesRepository.QueryEmployeesAsync(x => x.IsSynced == false);
                    foreach (var Employee in Employees)
                    {
                        Employee.IsSynced = Connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
                        await _EmployeesRepository.AddEmployeeAsync(Employee);
                    }
                    RefreshCommand.Execute(null);

                });
            }
        }


        public ICommand AddCommand
        {
            get
            {
                return new Command(async () =>
                {
                    var Employee = new Employee
                    {
                        Name = EmployeeName,
                        EmailAddress = EmployeeEmailAddress
                    };
                    Employee.IsSynced = Connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
                    await _EmployeesRepository.AddEmployeeAsync(Employee);  
                    RefreshCommand.Execute(null);

                });
            }
        }
        public ICommand UpdateCommand
        {
            get
            {
                return new Command<Employee>(async (Param) =>
                {
                    Param.Name = EmployeeName;
                    Param.EmailAddress = EmployeeEmailAddress;
                    Param.IsSynced = Connectivity.NetworkAccess == NetworkAccess.Internet ? true : false;
                    await _EmployeesRepository.UpdateEmployeeAsync(Param); 
                    RefreshCommand.Execute(null);

                });
            }
        }
        public EmployeesViewModel(IEmployeesRepository EmployeesRepository)
        {
            _EmployeesRepository = EmployeesRepository;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}