using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Windows.Data;

namespace Accounting_of_employees_test_task
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private Worker _selectedWorker;
        private readonly IFileService FileService;
        private readonly IDialogService DialogService;
        public ObservableCollection<Worker> Workers { get; set; }

        [Bindable(true)]
        private RelayCommand _saveCommand;
        public RelayCommand SaveCommand => _saveCommand ??= new RelayCommand(obj =>
                  {
                      try
                      {
                          if (DialogService.SaveFileDialog())
                          {
                              FileService.Save(DialogService.FilePath, Workers.ToList());
                              DialogService.ShowMessage("Файл сохранен");
                          }
                      }
                      catch (Exception ex)
                      {
                          DialogService.ShowMessage(ex.Message);
                      }
                  });

        private RelayCommand _openCommand;
        public RelayCommand OpenCommand => _openCommand ??= new RelayCommand(obj =>
                  {
                      try
                      {
                          if (DialogService.OpenFileDialog())
                          {
                              var workers = FileService.Open(DialogService.FilePath);
                              Workers.Clear();
                              foreach (var p in workers)
                                  Workers.Add(p);
                              DialogService.ShowMessage("Файл загружен");
                          }
                      }
                      catch (Exception ex)
                      {
                          DialogService.ShowMessage(ex.Message);
                      }
                  });


        private RelayCommand _addCommand;
        public RelayCommand AddCommand => _addCommand ??= new RelayCommand(obj =>
                  {
                      Worker worker = new Worker();
                      Workers.Insert(0, worker);
                      SelectedWorker = worker;

                  });

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand => _removeCommand ??= new RelayCommand(obj =>
                  {
                      if (obj is Worker worker)
                      {
                          Workers.Remove(worker);
                      }
                  },
                 (obj) => SelectedWorker != null);

        private RelayCommand _fireCommand;
        public RelayCommand FireCommand => _fireCommand ??= new RelayCommand(obj =>
                  {
                      if (obj is Worker worker)
                      {
                          worker.Status = "Уволен";
                          worker.Color = "Red";
                      }
                  },
                 (obj) => SelectedWorker != null);

        private RelayCommand _restoreCommand;
        public RelayCommand RestoreCommand => _restoreCommand ??= new RelayCommand(obj =>
                  {
                      if (obj is Worker worker)
                      {
                          worker.Status = "Работает";
                          worker.Color = "Black";
                      }
                  },
                 (obj) => SelectedWorker != null);

        public Worker SelectedWorker
        {
            get => _selectedWorker;
            set
            {
                _selectedWorker = value;
                OnPropertyChanged("SelectedWorker");
            }
        }

        public ApplicationViewModel(IDialogService dialogService, IFileService fileService)
        {
            DialogService = dialogService;
            FileService = fileService;

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
                return SelectedIndex switch
                {
                    (int)WorkerFilterValue.Name => w.Name.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.LastName => w.LastName.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.ID => w.ID.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.Position => w.Position.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.Department => w.Department.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.Salary => w.Salary.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.Expirience => w.Expirience.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.Category => w.Category.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.Pay => w.Pay.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.Bonus => w.Bonus.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.TaskNumber => w.TaskNumber.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.Age => w.Age.ToString().StartsWith(FilterString),
                    (int)WorkerFilterValue.Status => w.Status.ToString().StartsWith(FilterString),
                    _ => w.Name.ToString().StartsWith(FilterString),
                };
            };

    }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }

        private string filterString;
        public string FilterString
        {
            get => filterString;
            set
            {
                if (Equals(value, filterString)) return;
                filterString = value;
                FilteredItems.Refresh();
                OnPropertyChanged("FilterString");
            }
        }

        private int _selectedIndex;

        public int SelectedIndex
        {
            get => _selectedIndex;
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }
        private enum WorkerFilterValue
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
