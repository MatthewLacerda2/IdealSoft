using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using BackEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd;

public partial class UserListWindow : Window {

    public ObservableCollection<User> Users { get; set; } = new ObservableCollection<User>();

    public UserListWindow() {
        InitializeComponent();
        DataContext = this;

        PopulateUserList();
    }

    private async void PopulateUserList() {
        try {
            var apiService = new ApiService();
            var apiUrl = "https://localhost:5293/api/v1/users/";

            var response = await apiService.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode) {
                var users = JsonConvert.DeserializeObject<List<User>>(await response.Content.ReadAsStringAsync());

                Users.Clear();
                foreach (var user in users) {
                    Users.Add(user);
                }
            } else {
                MessageBox.Show($"Failed to retrieve users. Status code: {response.StatusCode}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        } catch (Exception ex) {
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void EditButton_Click(object sender, RoutedEventArgs e) {
        var selectedUser = (User)UserListDataGrid.SelectedItem;

        if (selectedUser != null) {
            var editUserWindow = new EditUserWindow(selectedUser);
            editUserWindow.Show();
        } else {
            MessageBox.Show("Please select a user to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private async void DeleteButton_Click(object sender, RoutedEventArgs e) {
        var selectedUser = (User)UserListDataGrid.SelectedItem;

        if (selectedUser != null) {
            var result = MessageBox.Show("Are you sure you want to delete this user?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes) {
                var apiService = new ApiService();
                bool deleteResult = await apiService.DeleteUser(selectedUser.Id);

                if (deleteResult) {
                    MessageBox.Show("User deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Refresh the user list (replace this with actual refresh logic)
                    UserListWindow newListWindow = new UserListWindow();
                    newListWindow.Show();
                    Close();
                } else {
                    MessageBox.Show("Failed to delete user.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        } else {
            MessageBox.Show("Please select a user to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}