// MainWindow.xaml.cs
using System.Windows;
using System.Windows.Controls;

namespace FrontEnd {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            var addUserWindow = new AddUserWindow();
            addUserWindow.ShowDialog();
        }

        private void ListButton_Click(object sender, RoutedEventArgs e) {
            var userListWindow = new UserListWindow();
            userListWindow.ShowDialog();
        }
    }
}
