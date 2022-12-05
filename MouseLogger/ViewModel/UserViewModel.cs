using MouseLogger.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MouseLogger.ViewModel
{
    public class UserViewModel : INotifyPropertyChanged
    {
        ApplicationContext db = new();
        
        public ObservableCollection<User>? Users { get; set; }

        public UserViewModel()
        {
            db.Database.EnsureCreated();
        }

        public void Log(string log, string pass)
        {
            IQueryable<User> user = db.Users;
            IQueryable<Role> role = db.Roles;
            var loginUser = user.Where(u => u.Login == log && u.Password == pass);

            int count = loginUser.Count();
            if (count > 0)
            {
                foreach (var u in loginUser)
                {
                    var loginRole = role.Where(r => r.Id == u.RoleId);
                    foreach (var r in loginRole)
                    {
                        MessageBox.Show($"Вы авторизовались под ролью {r.Name}");
                    }
                }
                MainWindow mainWindow = new();
                mainWindow.Show();
            }
            else
                MessageBox.Show("Неверный логин или пароль");
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
