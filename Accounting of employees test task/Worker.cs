using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Accounting_of_employees_test_task
{
    public class Worker : INotifyPropertyChanged, ICloneable
    {
        private string _name;
        private string _lastName;
        private int _id;
        private string _position;
        private string _department;
        private decimal _salary;
        private double _expirience;
        private int _category;
        private decimal _pay;
        private decimal _bonus;
        private int _taskNumber;
        private int _age;
        private string _status;
        private string _color = "Black";

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public int ID
        {
            get => _id;
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        public string Position
        {
            get => _position;
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }

        public string Department
        {
            get => _department;
            set
            {
                _department = value;
                OnPropertyChanged("Department");
            }
        }

        public decimal Pay
        {
            get => _pay;
            set
            {

                _pay = value;
                RefreshSalary();
                OnPropertyChanged("Pay");
            }
        }

        public decimal Bonus
        {
            get => _bonus;
            set
            {

                _bonus = value;
                RefreshSalary();
                OnPropertyChanged("Bonus");
            }
        }

        public decimal Salary
        {
            get => _salary;
            set
            {

                _salary = _pay + _bonus;
                OnPropertyChanged("Salary");
            }
        }

        public double Expirience
        {
            get => _expirience;
            set
            {

                _expirience = value;
                OnPropertyChanged("Expirience");
            }
        }

        public int Category
        {
            get => _category;
            set
            {

                _category = value;
                OnPropertyChanged("Category");
            }
        }

        public int TaskNumber
        {
            get => _taskNumber;
            set
            {

                _taskNumber = value;
                OnPropertyChanged("TaskNumber");
            }
        }
        public int Age
        {
            get => _age;
            set
            {

                _age = value;
                OnPropertyChanged("Age");
            }
        }

        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public string Color
        {
            get => _color;
            set
            {
                _color = value;
                OnPropertyChanged("Color");
            }
        }

        private void RefreshSalary()
        {
            Salary = 1;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        public object Clone()
        {
            return (Worker)MemberwiseClone();
        }
    }
}
