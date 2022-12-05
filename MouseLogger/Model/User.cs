using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseLogger.Model
{
    public class User : INotifyPropertyChanged
    {
        string? _login;
        string? _password;
        int? _roleId;
        public int Id { get; set; }
        public string? Login
        {
            get { return _login; }
            set
            {
                _login = value;
                OnPropertyChanged("Login");
            }
        }

        public string? Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
            }
        }
        public int? RoleId 
        {
            get { return _roleId; }
            set
            {
                _roleId = value;
                OnPropertyChanged("RoleId");
            }
        }
        public Role? Role { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
