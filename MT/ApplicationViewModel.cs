using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MT
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        private readonly string PATH = $"{Environment.CurrentDirectory}\\taskData.json";
        private FileInputOutput _fileIOService;
        public Task selectedTask;
        public Task newTask = new Task();

        public BindingList<Task> Tasks { get; set; }

        // команда добавления нового объекта
        private RelayCommand addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return addCommand ??
                  (addCommand = new RelayCommand(obj =>
                  {
                      if ((!string.IsNullOrWhiteSpace(newTask.Name) & !string.IsNullOrWhiteSpace(newTask.Description)))
                      {
                          Task task = new Task() { Name = newTask.Name, Description = newTask.Description };
                          Tasks.Add(task);
                          SelectedTask = newTask;
                          newTask.Name = null;
                          newTask.Description = null;
                      }
                      else
                      {
                          MessageBox.Show("Введите/обновите данные!");
                      }
                  }));
            }
        }

        // команда удаления
        private RelayCommand removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new RelayCommand(obj =>
                  {
                      Task task = obj as Task;
                      if (task != null)
                      {
                          Tasks.Remove(task);
                      }
                  },
                 (obj) => Tasks.Count > 0));
            }
        }

        // команда статуса
        private RelayCommand doneCommand;
        public RelayCommand DoneCommand
        {
            get
            {
                return doneCommand ??
                  (doneCommand = new RelayCommand(obj =>
                  {
                      Task task = obj as Task;
                      if (task != null)
                      {
                          task.IsDone = true;
                      }
                  },
                 (obj) => Tasks.Count > 0));
            }
        }

        public Task SelectedTask
        {
            get { return selectedTask; }
            set
            {
                selectedTask = value;
                OnPropertyChanged("SelectedTask");
            }
        }

        public string NewName
        {
            get { return newTask.Name; }
            set
            {
                newTask.Name = value;
                OnPropertyChanged("NewTask");
            }
        }
        public string NewDescription
        {
            get { return newTask.Description; }
            set
            {
                newTask.Description = value;
                OnPropertyChanged("NewDescription");
            }
        }

        public ApplicationViewModel()
        {

            _fileIOService = new FileInputOutput(PATH);

            try
            {
                Tasks = _fileIOService.LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Tasks.ListChanged += _tasks_ListChanged;

        }

        private void _tasks_ListChanged(object sender, ListChangedEventArgs e)
        {
            if (e.ListChangedType == ListChangedType.ItemAdded || e.ListChangedType == ListChangedType.ItemChanged || e.ListChangedType == ListChangedType.ItemDeleted)
            {
                _fileIOService.SaveDate(sender);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
