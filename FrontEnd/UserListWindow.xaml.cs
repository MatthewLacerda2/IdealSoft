// UserListWindow.xaml.cs
using System.Collections.Generic;
using System.Windows;

namespace FrontEnd {
    public partial class UserListWindow : Window {
        public List<User> Users { get; set; } = new List<User>(); // Placeholder for the list of users

        public UserListWindow() {
            InitializeComponent();
            DataContext = this; // Set the DataContext to enable data binding
        }

        private void EditButton_Click(object sender, RoutedEventArgs e) {
            // Implement logic to navigate to the EditUserWindow
            var editUserWindow = new EditUserWindow();
            editUserWindow.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e) {
            // Implement logic for deletion
            var result = MessageBox.Show("Are you sure you want to delete this item?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) {
                // Implement deletion logic (pretend HTTP call)
                MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}
