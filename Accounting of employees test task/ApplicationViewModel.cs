using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Linq;
using System.Windows;
using System;
using System.Collections.Generic;
using System.Windows.Data;
using System.Windows.Controls;

namespace Accounting_of_employees_test_task
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Worker _selectedWorker;

        IFileService _fileService;
        IDialogService _dialogService;
       
        public ObservableCollection<Worker> Workers { get; set; }

        [Bindable(true)]
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand
        {
            get
            {
                return _saveCommand ??
                  (_saveCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (_dialogService.SaveFileDialog() == true)
                          {
                              _fileService.Save(_dialogService.FilePath, Workers.ToList());
                              _dialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          _dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }
               
        private RelayCommand _openCommand;
        public RelayCommand OpenCommand
        {
            get
            {
                return _openCommand ??
                  (_openCommand = new RelayCommand(obj =>
                  {
                      try
                      {
                          if (_dialogService.OpenFileDialog() == true)
                          {
                              var workers = _fileService.Open(_dialogService.FilePath);
                              Workers.Clear();
                              foreach (var p in workers)
                                  Workers.Add(p);
                              _dialogService.ShowMessage("Файл загружен");
                          }
                      }
                      catch (Exception ex)
                      {
                          _dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }


        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(obj =>
                  {
                      Worker worker = new Worker();
                      Workers.Insert(0, worker);
                      SelectedWorker = worker;
   
                  }));
            }
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??
                  (_removeCommand = new RelayCommand(obj =>
                  {
                      Worker worker = obj as Worker;
                      if (worker != null)
                      {
                          Workers.Remove(worker);
                      }
                  },
                 (obj) => SelectedWorker != null));
            }
        }

        private RelayCommand _fireCommand;
        public RelayCommand FireCommand
        {
            get
            {
                return _fireCommand ??
                  (_fireCommand = new RelayCommand(obj =>
                  {
                      Worker worker = obj as Worker;
                      if (worker != null)
                      {
                          worker.Status = "Уволен";
                          worker.Color = "Red";
                      }
                  },
                 (obj) => SelectedWorker != null));
            }
        }

        private RelayCommand _restoreCommand;
        public RelayCommand RestoreCommand
        {
            get
            {
                return _restoreCommand ??
                  (_restoreCommand = new RelayCommand(obj =>
                  {
                      Worker worker = obj as Worker;
                      if (worker != null)
                      {
                          worker.Status = "Работает";
                          worker.Color = "Black";
                      }
                  },
                 (obj) => SelectedWorker != null));
            }
        }

        public Worker SelectedWorker
        {
            get { return _selectedWorker; }
            set
            {
                _selectedWorker = value;
                OnPropertyChanged("SelectedWorker");
            }
        }

        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            _dialogService = dialogService;
            _fileService = fileService;

        Workers = new ObservableCollection<Worker>
            {
                new Worker { Name="Serge", LastName="Kor", ID=1, Position="Programmer", Age=30, Bonus=34, Category=1, Department="ASU", Pay=45000, Expirience=5, TaskNumber=2, Salary=1, Status="Работает"},
                new Worker { Name="Dmit", LastName="Mir", ID=2, Position="Programmer", Age=30, Bonus=51, Category=1, Department="ASU", Pay=47000, Expirience=7, TaskNumber=3, Salary=1, Status="Работает"},
                new Worker { Name="Ivan", LastName="Ant", ID=3, Position="Engineer", Age=28, Bonus=18, Category=3, Department="Nalad", Pay=21000, Expirience=2, TaskNumber=1, Salary=1, Status="Работает"}
            };

             

            FilteredItems = CollectionViewSource.GetDefaultView(Workers);

            FilteredItems.Filter = i =>
            {
                if (string.IsNullOrEmpty(FilterString)) return true;
                Worker w = i as Worker;
                switch (SelectedIndex)
                {
                    case (int)WorkerFilterValue.Name:
                        return w.Name.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.LastName:
                        return w.LastName.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.ID:
                        return w.ID.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.Position:
                        return w.Position.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.Department:
                        return w.Department.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.Salary:
                        return w.Salary.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.Expirience:
                        return w.Expirience.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.Category:
                        return w.Category.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.Pay:
                        return w.Pay.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.Bonus:
                        return w.Bonus.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.TaskNumber:
                        return w.TaskNumber.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.Age:
                        return w.Age.ToString().StartsWith(FilterString);
                    case (int)WorkerFilterValue.Status:
                        return w.Status.ToString().StartsWith(FilterString);
                    default:
                        return w.Name.ToString().StartsWith(FilterString);
                }
            };

    }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private string filterString;
        public string FilterString
        {
            get { return filterString; }
            set
            {
                if (Equals(value, filterString)) return;
                filterString = value;
                FilteredItems.Refresh(); // tirggers filtering logic
                OnPropertyChanged("FilterString");
            }
        }

        //test
        private int _selectedIndex;

        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }
        enum WorkerFilterValue
        {
            Name ,
            LastName,
            ID,
            Position,
            Department,
            Salary,
            Expirience,
            Category,
            Pay,
            Bonus,
            TaskNumber,
            Age,
            Status
        }
        public ICollectionView FilteredItems { get; set; }
    }
}
