using MouseLogger.Model;
using MouseLogger.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MouseLogger
{
    public partial class LoginWindow : Window
    {
        public User? User { get; set; }
        public string? userLogin;
        public string? userPassword;
        public LoginWindow()
        {
            InitializeComponent();
            DataContext = new UserViewModel();
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            userLogin = l.Text.ToString();
            userPassword = p.Text.ToString();
            UserViewModel uv = new();
            uv.Log(userLogin, userPassword);
        }
    }
}
