using BackEnd.Models;
using System.Windows;

namespace FrontEnd;

public partial class EditUserWindow : Window {

    private readonly ApiService apiService;

    public User EditedUser { get; private set; }

    public EditUserWindow(User user) {
        InitializeComponent();
        DataContext = this;

        apiService = new ApiService();
        EditedUser = user;

        NameTextBox.Text = user.name;
        SurnameTextBox.Text = user.surname;
        TelephoneTextBox.Text = user.telephone;
    }

    private async void SaveButton_Click(object sender, RoutedEventArgs e) {
        // Update the EditedUser with the values from the TextBoxes
        EditedUser.name = NameTextBox.Text;
        EditedUser.surname = SurnameTextBox.Text;
        EditedUser.telephone = TelephoneTextBox.Text;

        // Make an HTTP PATCH call to update the user
        bool updateResult = await apiService.UpdateUser(EditedUser);

        if (updateResult) {
            MessageBox.Show("Editing saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        } else {
            MessageBox.Show("Failed to save changes.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void DoneButton_Click(object sender, RoutedEventArgs e) {
        Close();
    }
}