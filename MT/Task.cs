using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MT
{
    public class Task : INotifyPropertyChanged
    {
        public DateTime CreationDate { get; set; } = DateTime.Now;

        private string _name;
        private string _description;
        private bool _isDone = false;

        public string Name
        {
            get { return _name; }
            set
            {
                if (_name == value)
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description == value)
                {
                    return;
                }
                _description = value;
                OnPropertyChanged("Description");
            }
        }
        public bool IsDone
        {
            get { return _isDone; }
            set
            {
                if (_isDone == value)
                    return;
                _isDone = value;
                OnPropertyChanged("IsDone");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
