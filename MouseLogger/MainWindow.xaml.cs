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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;
using MouseLogger.Model;
using MouseLogger.ViewModel;

namespace MouseLogger
{
    public partial class MainWindow : Window
    {
        public string? filter;
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new EventViewModel();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            filter = Event.Text;
            EventViewModel ewm = new();
            ewm.Filtering(filter);
        }
    }
}
