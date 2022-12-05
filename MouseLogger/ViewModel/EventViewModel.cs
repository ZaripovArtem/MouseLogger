using MouseLogger.Model;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Windows;
using System.Linq;
using MouseLogger.Services;

namespace MouseLogger.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {
        ApplicationContext db = new();
        private ICommand? _startLogging;
        private ICommand? _stopLogging;
        private ICommand? _mouseCommand;
        private bool _btnStart = true;
        private bool _btnStop = false;
        private int messageCount;
        private string? _filter;

        public ObservableCollection<Event> Events { get; set; }

        public EventViewModel()
        {
            //db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            messageCount = db.Events.Count();
            db.Events.Load();
            Events = db.Events.Local.ToObservableCollection();
        }

        public ICommand StartLogging
        {
            get
            {
                if (_startLogging == null)
                {
                    _startLogging = new RelayCommand((param) => Start());
                }
                return _startLogging;                  
            }
        }

        public ICommand StopLogging
        {
            get
            {
                if(_stopLogging == null)
                {
                    _stopLogging = new RelayCommand((param) => Stop());
                }
                return _stopLogging;
            }
        }

        public ICommand MouseCommand
        {
            get
            {
                if(_mouseCommand == null)
                {
                    _mouseCommand = new RelayCommand((param) => MouseLog(param?.ToString()));
                }
                return _mouseCommand;
            }
        }

        private void MouseLog(string? param)
        {
            if(_btnStart == false)
            {
                Event ev = new() { Name = param, Date = DateTime.Now.ToString() };
                db.Events.Add(ev);
                db.SaveChanges();
                db.Events.Load();
                messageCount++;

                if(messageCount%50 == 0)
                {
                    MessageBoxResult result = MessageBox.Show(
                        "В базе данных набрано следующие 50 записей. Подтвердите отправку.", "Внимание",  
                        MessageBoxButton.YesNo);
                    if(result == MessageBoxResult.Yes)
                    {
                        string message = $"В базе данных {messageCount} записей";
                        var mail = db.Emails.Where(e => e.Id == 1);
                        string? email = (from e in db.Emails select e.EmailAddress).ToList().First();
                        SendEmail sendEmail = new();
                        SendWhattsAPP sendWhattsAPP = new();
                        sendEmail.Send(message, email);
                        sendWhattsAPP.Send(message);
                    }
                }
            }
            
        }

        public bool btnStart
        {
            get { return _btnStart; }
            set
            {
                if (_btnStart != value)
                {
                    _btnStart = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool btnStop
        {
            get { return _btnStop; }
            set
            {
                _btnStop = value;
                OnPropertyChanged();
            }
        }

        public void Start()
        {
            btnStop = true;
            btnStart = false;
        }

        public void Stop()
        {
            btnStop = false;
            btnStart = true;
        }

        public void Filtering(string filter)
        {
            _filter = filter;
            var filterEvent = Events.Where(e => e.Name == _filter);
            if (_filter == "Все" || _filter == "")
            {
                filterEvent = Events;
            }
            FilterWindow fw = new();
            fw.Show();
            foreach(var fe in filterEvent)
            {
                
                fw.ViewList.Items.Add($"{fe.Name} - {fe.Date}");
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
