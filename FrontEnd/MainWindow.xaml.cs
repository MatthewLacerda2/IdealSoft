// MainWindow.xaml.cs
using System.Windows;
using System.Windows.Controls;

namespace FrontEnd {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e) {
            // Open a window for adding a user
            var addUserWindow = new AddUserWindow();
            addUserWindow.ShowDialog(); // Use ShowDialog to make it a modal window
        }

        private void ListButton_Click(object sender, RoutedEventArgs e) {
            // Open a window for listing users
            var userListWindow = new UserListWindow();
            userListWindow.ShowDialog(); // Use ShowDialog to make it a modal window
        }
    }
}
