// AddUserWindow.xaml.cs
using System.Windows;

namespace FrontEnd {
    public partial class AddUserWindow : Window {
        public AddUserWindow() {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e) {
            // Implement logic to save the user
            // You'll need to call the API or perform any other necessary actions
            // You can access the textbox values like this: NameTextBox.Text, SurnameTextBox.Text, TelephoneTextBox.Text
            // Example: var user = new User { Name = NameTextBox.Text, Surname = SurnameTextBox.Text, Telephone = TelephoneTextBox.Text };
            // Then, call the API to save the user
        }

        private void Cancel_Button(object sender, RoutedEventArgs e) {
            // Implement logic to close the window without saving
            Close(); // Close the window
        }
    }
}
