using System.Windows;
using BackEnd.Models;

namespace FrontEnd {
    public partial class UserListWindow : Window {

        public List<User> Users { get; set; } = new List<User>();

        public UserListWindow() {
            InitializeComponent();
            DataContext = this;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {
            var editUserWindow = new EditUserWindow();
            editUserWindow.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            var result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) {
                MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
