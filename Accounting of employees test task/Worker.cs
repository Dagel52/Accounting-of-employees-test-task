using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

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
        private string _color="Black";

        public string Name
        {
            get { return _name; }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("LastName");
            }
        }
        public int ID
        {
            get { return _id; }
            set
            {
                _id = value;
                OnPropertyChanged("ID");
            }
        }

        public string Position
        {
            get { return _position; }
            set
            {
                _position = value;
                OnPropertyChanged("Position");
            }
        }

        public string Department
        {
            get { return _department; }
            set
            {
                _department = value;
                OnPropertyChanged("Department");
            }
        }

        public decimal Pay
        {
            get { return _pay; }
            set
            {

                _pay = value;
                RefreshSalary();
                OnPropertyChanged("Pay");
            }
        }

        public decimal Bonus
        {
            get { return _bonus; }
            set
            {

                _bonus = value;
                RefreshSalary();
                OnPropertyChanged("Bonus");
            }
        }

        public decimal Salary
        {
            get { return _salary; }
            set
            {

                _salary = _pay + _bonus;
                OnPropertyChanged("Salary");
            }
        }

        public double Expirience
        {
            get { return _expirience; }
            set
            {

                _expirience = value;
                OnPropertyChanged("Expirience");
            }
        }

        public int Category
        {
            get { return _category; }
            set
            {

                _category = value;
                OnPropertyChanged("Category");
            }
        }
        
        public int TaskNumber   
        {
            get { return _taskNumber; }
            set
            {

                _taskNumber = value;
                OnPropertyChanged("TaskNumber");
            }
        }
        public int Age
        {
            get { return _age; }
            set
            {

                _age = value;
                OnPropertyChanged("Age");
            }
        }

        public string Status
        {
            get { return _status; }
            set
            {
                _status = value;
                OnPropertyChanged("Status");
            }
        }

        public string Color
        {
            get { return _color; }
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
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        public object Clone()
        {
            return (Worker)this.MemberwiseClone();
        }
    }
}
