using System.Windows;
using BackEnd.Models;
using BackEnd.Data;

namespace FrontEnd {
    public partial class AddUserWindow : Window {
        public AddUserWindow() {
            InitializeComponent();
        }

        private async void SaveButton_Click(object sender, RoutedEventArgs e) {
            
            string name = NameTextBox.Text;
            string surname = SurnameTextBox.Text;
            string telephone = TelephoneTextBox.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(surname) || string.IsNullOrWhiteSpace(telephone)) {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            var user = new User {
                name = name,
                surname = surname,
                telephone = telephone
            };

            var apiService = new ApiService();
            var isUserAdded = await apiService.AddUserAsync(user);

            if (isUserAdded) {
                MessageBox.Show("User added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            } else {
                MessageBox.Show("Failed to add user. Please try again.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Cancel_Button(object sender, RoutedEventArgs e) {
            Close();
        }
    }
}
