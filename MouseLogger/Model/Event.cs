using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MouseLogger.Model
{
    public class Event : INotifyPropertyChanged
    {
        string? _name;
        string? _date;

        public int Id { get; set; }
        public string? Name
        {
            get { return _name; }
            set 
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        public string? Date
        {
            get { return _date; }
            set 
            {
                _date = value;
                OnPropertyChanged("Date");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
