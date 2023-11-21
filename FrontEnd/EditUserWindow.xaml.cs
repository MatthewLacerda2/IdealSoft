// EditUserWindow.xaml.cs
using BackEnd.Models;
using System.Windows;

namespace FrontEnd;

public partial class EditUserWindow : Window {
    public User EditedUser { get; private set; }

    public EditUserWindow(User user) {
        InitializeComponent();
        DataContext = this;

        EditedUser = user;

        NameTextBox.Text = user.name;
        SurnameTextBox.Text = user.surname;
        TelephoneTextBox.Text = user.telephone;
    }

    private void SaveButton_Click(object sender, RoutedEventArgs e) {
        MessageBox.Show("Editing saved!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void DoneButton_Click(object sender, RoutedEventArgs e) {
        Close();
    }
}